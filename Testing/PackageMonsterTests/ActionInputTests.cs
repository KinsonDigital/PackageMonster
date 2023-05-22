// <copyright file="ActionInputTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable PossibleMultipleEnumeration
namespace PackageMonsterTests;

using CommandLine;
using FluentAssertions;
using PackageMonster;
using PackageMonsterTests.Helpers;

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
            .AssertOptionAttrProps("version", true, "The version of the NuGet package to check.  This is not case-sensitive.");

        inputs.PackageName.Should().BeEmpty();
        typeof(ActionInputs).GetProperty(nameof(ActionInputs.FailWhenNotFound)).Should().BeDecoratedWith<OptionAttribute>();
        inputs.GetAttrFromProp<OptionAttribute>(nameof(ActionInputs.FailWhenNotFound))
            .AssertOptionAttrProps("fail-when-not-found", false, false, "If true, will fail the workflow if the NuGet package of the requested version does not exist.");
        
        inputs.PackageName.Should().BeEmpty();
        typeof(ActionInputs).GetProperty(nameof(ActionInputs.FailWhenFound)).Should().BeDecoratedWith<OptionAttribute>();
        inputs.GetAttrFromProp<OptionAttribute>(nameof(ActionInputs.FailWhenFound))
            .AssertOptionAttrProps("fail-when-found", false, false, "If true, will fail the workflow if the NuGet package of the requested version does exist.");
    }
    #endregion
}
