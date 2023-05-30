// <copyright file="DataService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Net;
using Newtonsoft.Json.Linq;
using PackageMonster.Repositories;
using RestSharp;

namespace PackageMonster.Services;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public sealed class DataService : IDataService
{
    private readonly RestClient client;
    private bool isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataService"/> class.
    /// </summary>
    public DataService() => this.client = new RestClient();

    /// <inheritdoc />
    /// <remarks>
    ///     The param <paramref name="packageName"/> is not case sensitive.  The API
    ///     requires that it is in lowercase.  This is taken care of for you.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the <paramref name="packageName"/> param is null or empty.
    /// </exception>
    /// <exception cref="HttpRequestException">
    ///     Thrown if any HTTP based error occurs.
    /// </exception>
    public async Task<string[]> GetVersions(string packageName, string source, string versionsJsonPath)
    {
        if (string.IsNullOrEmpty(packageName))
        {
            throw new ArgumentNullException(nameof(packageName), $"Must provide a package name.");
        }

        if (string.IsNullOrWhiteSpace(source))
        {
            source = "nuget";
        }

        IPackageRepository packageRepository;

        switch (source.ToLowerInvariant())
        {
            case "nuget":
                packageRepository = new NugetPackageRepository();
                break;
            case "npm":
                packageRepository = new NpmPackageRepository();
                break;
            default:
                if (!Uri.IsWellFormedUriString(source, UriKind.Absolute))
                {
                    throw new ArgumentException(nameof(source), $"Must provide a well-formed source URI.");
                }

                if (!Uri.TryCreate(source, UriKind.Absolute, out _))
                {
                    throw new ArgumentException(nameof(source), $"Must provide an absolute source URI.");
                }

                if (string.IsNullOrWhiteSpace(versionsJsonPath))
                {
                    throw new ArgumentException(nameof(versionsJsonPath), $"Must provide a json path for a custom source. Make sure the variable `PACKAGE-NAME` is in the url.");
                }

                packageRepository = new CustomPackageRepository { Url = source, JsonPath = versionsJsonPath };
                break;
        }

        this.client.AcceptedContentTypes = new[] { "application/json" };
        
        var resolvedUrl = packageRepository.Url.Replace("PACKAGE-NAME", packageName);
        var request = new RestRequest(resolvedUrl);

        var response = await this.client.ExecuteAsync(request, Method.Get);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            if (string.IsNullOrWhiteSpace(response.Content) || response.ContentLength == 0)
            {
                return Array.Empty<string>();
            }

            var json = JObject.Parse(response.Content);
            return json.SelectTokens(packageRepository.JsonPath).Select(t => t.Value<string>()).Cast<string>().ToArray();
        }

        var exception = response.ErrorException ?? new Exception($"There was an issue getting data from '{source}'.");

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
