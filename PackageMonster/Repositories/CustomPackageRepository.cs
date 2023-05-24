namespace PackageMonster.Repositories;

internal class CustomPackageRepository : IPackageRepository
{
    public string Url { get; set; }
    public string JsonPath { get; set; }
}
