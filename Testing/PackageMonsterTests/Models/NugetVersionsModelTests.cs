// <copyright file="NugetVersionsModelTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

// ReSharper disable UseObjectOrCollectionInitializer

using Newtonsoft.Json.Linq;
using PackageMonster.Services;

namespace PackageMonsterTests.Models;

using FluentAssertions;

/// <summary>
/// Tests the versions Json Path functionality.
/// </summary>
public class NugetVersionsModelTests
{
    #region Prop Tests
    [Fact]
    public void Version_WhenSettingValue_ReturnsCorrectResult()
    {
        // Arrange
        var model = JObject.Parse(@"{ 'versions': [""1.2.3"", ""4.5.6""] }");

        // Act
        var actual = model.SelectTokens(NugetDataService.PublicNugetVersionsJsonPath).Select(v => v.Value<string>()).ToArray();

        // Assert
        actual.Should()
            .HaveCount(2)
            .And.Contain("1.2.3")
            .And.Contain("4.5.6")
            .And.HaveElementPreceding("4.5.6", "1.2.3");
    }
    #endregion
}
