namespace GotNuget.Services;

/// <summary>
/// Provides access to nuget.org data.
/// </summary>
public interface INugetDataService : IDisposable
{
    /// <summary>
    /// Gets all of the versions of a nuget package that has been published
    /// to nuget.org using the given <paramref name="packageName"/>.
    /// </summary>
    /// <param name="packageName">The name of the package.</param>
    /// <returns>The list of versions that exist for the package.</returns>
    Task<string[]> GetNugetVersions(string packageName);
}
