// <copyright file="NugetVersionsModelTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using FluentAssertions;
using NugetExistsChecker.Models;

// ReSharper disable UseObjectOrCollectionInitializer
namespace NugetExistsCheckerTests.Models;

/// <summary>
/// Tests the <see cref="NugetVersionsModel"/> class.
/// </summary>
public class NugetVersionsModelTests
{
    #region Prop Tests
    [Fact]
    public void Version_WhenSettingValue_ReturnsCorrectResult()
    {
        // Arrange
        var model = new NugetVersionsModel();

        // Act
        model.Versions = new[] { "1.2.3", "4.5.6" };

        // Assert
        model.Versions.Should()
            .HaveCount(2)
            .And.Contain("1.2.3")
            .And.Contain("4.5.6")
            .And.HaveElementPreceding("4.5.6", "1.2.3");
    }
    #endregion
}
