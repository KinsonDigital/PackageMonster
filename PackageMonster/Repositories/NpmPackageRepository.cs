namespace PackageMonster.Repositories;

internal class NpmPackageRepository : IPackageRepository
{
    public string Url => "https://registry.npmjs.org/PACKAGE-NAME";
    public string JsonPath => "$.versions.*.version";
}
