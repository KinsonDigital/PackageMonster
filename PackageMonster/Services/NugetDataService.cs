// <copyright file="NugetDataService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace PackageMonster.Services;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public sealed class NugetDataService : INugetDataService
{
    /* Resources:
     * These links refer to the documentation for the NuGet API
     * 1. Package Content: https://docs.microsoft.com/en-us/nuget/api/package-base-address-resource
     * 2.Nuget Server API: https://docs.microsoft.com/en-us/nuget/api/overview
     */
    internal const string PublicNugetApiUrl = "https://api.nuget.org/v3-flatcontainer/PACKAGE-NAME/index.json";
    internal const string PublicNugetVersionsJsonPath = "$.versions[*]";
    private readonly RestClient client;
    private bool isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="NugetDataService"/> class.
    /// </summary>
    public NugetDataService() => this.client = new RestClient(PublicNugetApiUrl);

    /// <inheritdoc />
    /// <remarks>
    ///     The param <paramref name="packageName"/> is not case sensitive.  The NuGet API
    ///     requires that it is in lowercase.  This is taken care of for you.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the <paramref name="packageName"/> param is null or empty.
    /// </exception>
    /// <exception cref="HttpRequestException">
    ///     Thrown if any HTTP based error occurs.
    /// </exception>
    public async Task<string[]> GetNugetVersions(string packageName, string source, string versionsJsonPath)
    {
        if (string.IsNullOrEmpty(packageName))
        {
            throw new ArgumentNullException(nameof(packageName), $"Must provide a NuGet package name.");
        }

        if (string.IsNullOrWhiteSpace(source))
        {
            source = PublicNugetApiUrl;
        }

        if (!Uri.IsWellFormedUriString(source, UriKind.Absolute))
        {
            throw new ArgumentException(nameof(source), $"Must provide a well-formed NuGet source URI.");
        }

        if (!Uri.TryCreate(source, UriKind.Absolute, out _))
        {
            throw new ArgumentException(nameof(source), $"Must provide an absolute NuGet source URI.");
        }

        if (string.IsNullOrWhiteSpace(versionsJsonPath))
        {
            versionsJsonPath = PublicNugetVersionsJsonPath;
        }

        this.client.AcceptedContentTypes = new[] { "application/vnd.github.v3+json" };
        
        var resolvedUrl = source.Replace("PACKAGE-NAME", packageName);
        var request = new RestRequest(resolvedUrl);

        var response = await this.client.ExecuteAsync(request, Method.Get);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            if (string.IsNullOrWhiteSpace(response.Content) || response.ContentLength == 0)
            {
                return Array.Empty<string>();
            }

            var json = JObject.Parse(response.Content);
            return json.SelectTokens(versionsJsonPath).Select(t => t.Value<string>()).ToArray();
        }

        var exception = response.ErrorException ?? new Exception("There was an issue getting data from NuGet.");

        throw new HttpRequestException(
            exception.Message,
            inner: null,
            statusCode: response.StatusCode);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (this.isDisposed)
        {
            return;
        }

        this.client.Dispose();

        this.isDisposed = true;
    }
}
