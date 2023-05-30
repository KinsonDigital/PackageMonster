// <copyright file="PackageVersionsModelTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

// ReSharper disable UseObjectOrCollectionInitializer

using Newtonsoft.Json.Linq;
using PackageMonster.Repositories;
using PackageMonster.Services;

namespace PackageMonsterTests.Models;

using FluentAssertions;

/// <summary>
/// Tests the versions Json Path functionality.
/// </summary>
public class PackageVersionsModelTests
{
    #region Prop Tests
    [Fact]
    public void Version_WhenUsingNugetJsonPath_ReturnsCorrectResult()
    {
        // Arrange
        var packageRepository = new NugetPackageRepository();
        var model = JObject.Parse(@"
{ 
    ""versions"": [
        ""1.2.3"", 
        ""4.5.6""
    ]
}
");

        // Act
        var actual = model.SelectTokens(packageRepository.JsonPath).Select(v => v.Value<string>()).ToArray();

        // Assert
        actual.Should()
            .HaveCount(2)
            .And.Contain("1.2.3")
            .And.Contain("4.5.6")
            .And.HaveElementPreceding("4.5.6", "1.2.3");
    }
    #endregion


    #region Prop Tests
    [Fact]
    public void Version_WhenUsingNpmJsonPath_ReturnsCorrectResult()
    {
        // Arrange
        var packageRepository = new NpmPackageRepository();
        var model = JObject.Parse(@"
{
  ""versions"": {
    ""1.2.3"": {
      ""version"": ""1.2.3""
    },
    ""4.5.6"": {
      ""version"": ""4.5.6""
    }
  }
}
");

        // Act
        var actual = model.SelectTokens(packageRepository.JsonPath).Select(v => v.Value<string>()).ToArray();

        // Assert
        actual.Should()
            .HaveCount(2)
            .And.Contain("1.2.3")
            .And.Contain("4.5.6")
            .And.HaveElementPreceding("4.5.6", "1.2.3");
    }
    #endregion
}
