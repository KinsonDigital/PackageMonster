namespace PackageMonster.Repositories;

/* Resources:
 * These links refer to the documentation for the Nuget API
 * 1. Package Content: https://docs.microsoft.com/en-us/nuget/api/package-base-address-resource
 * 2. Server API: https://docs.microsoft.com/en-us/nuget/api/overview
 */
internal class NugetPackageRepository : IPackageRepository
{
    public string Url => "https://api.nuget.org/v3-flatcontainer/PACKAGE-NAME/index.json";
    public string JsonPath => "$.versions[*]";
}
