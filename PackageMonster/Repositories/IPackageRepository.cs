namespace PackageMonster.Repositories;

internal interface IPackageRepository
{
    string Url { get; }
    string JsonPath { get; }
}
