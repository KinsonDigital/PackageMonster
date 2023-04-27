// <copyright file="INugetDataService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace PackageMonster.Services;

/// <summary>
/// Provides access to data from nuget.org marketplace.
/// </summary>
public interface INugetDataService : IDisposable
{
    /// <summary>
    /// Gets all of the versions of a NuGet package that have been published
    /// to nuget.org using the given <paramref name="packageName"/>.
    /// </summary>
    /// <param name="packageName">The name of the package.</param>
    /// <returns>The list of versions that exist for the package.</returns>
    Task<string[]> GetNugetVersions(string packageName);
}
