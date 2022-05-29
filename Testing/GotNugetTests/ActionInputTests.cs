// <copyright file="ActionInputTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using CommandLine;
using FluentAssertions;
using GotNuget;
using GotNugetTests.Helpers;

// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable PossibleMultipleEnumeration
namespace GotNugetTests;

/// <summary>
/// Tests the <see cref="ActionInputs"/> class.
/// </summary>
public class ActionInputTests
{
    #region Prop Tests
    [Fact]
    public void Ctor_WhenConstructed_PropsHaveCorrectDefaultValuesAndDecoratedWithAttributes()
    {
        // Arrange & Act
        var inputs = new ActionInputs();

        // Assert
        inputs.PackageName.Should().BeEmpty();
        typeof(ActionInputs).GetProperty(nameof(ActionInputs.PackageName)).Should().BeDecoratedWith<OptionAttribute>();
        inputs.GetAttrFromProp<OptionAttribute>(nameof(ActionInputs.PackageName))
            .AssertOptionAttrProps("package-name", true, "The name of the package.  This is not case-sensitive.");

        inputs.PackageName.Should().BeEmpty();
        typeof(ActionInputs).GetProperty(nameof(ActionInputs.Version)).Should().BeDecoratedWith<OptionAttribute>();
        inputs.GetAttrFromProp<OptionAttribute>(nameof(ActionInputs.Version))
            .AssertOptionAttrProps("version", true, "The version of the nuget package to check for.  This is not case-sensitive.");

        inputs.PackageName.Should().BeEmpty();
        typeof(ActionInputs).GetProperty(nameof(ActionInputs.FailWhenNotFound)).Should().BeDecoratedWith<OptionAttribute>();
        inputs.GetAttrFromProp<OptionAttribute>(nameof(ActionInputs.FailWhenNotFound))
            .AssertOptionAttrProps("fail-when-not-found", false, false, "If true, will fail the workflow if the nuget package of the requested version does not exist.");
    }
    #endregion
}
