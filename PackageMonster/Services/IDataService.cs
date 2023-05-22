// <copyright file="IDataService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace PackageMonster.Services;

/// <summary>
/// Provides access to data from a package marketplace.
/// </summary>
public interface IDataService : IDisposable
{
    /// <summary>
    /// Gets all of the versions of a package that have been published
    /// using the given <paramref name="packageName"/>.
    /// </summary>
    /// <param name="packageName">The name of the package.</param>
    /// <param name="source">The source.</param>
    /// <param name="versionsJsonPath">The versions json path.</param>
    /// <returns>The list of versions that exist for the package.</returns>
    Task<string[]> GetVersions(string packageName, string source, string versionsJsonPath);
}
