// <copyright file="ActionInputTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable PossibleMultipleEnumeration
namespace PackageMonsterTests;

using CommandLine;
using FluentAssertions;
using PackageMonster;
using Helpers;

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
        var packageNameOptionAttr = inputs.GetAttrFromProp<OptionAttribute>(nameof(ActionInputs.PackageName));
        packageNameOptionAttr.LongName.Should().Be("package-name");
        packageNameOptionAttr.Required.Should().BeTrue();
        packageNameOptionAttr.HelpText.Should().Be("The name of the package.  This is not case-sensitive.");

        inputs.PackageName.Should().BeEmpty();
        typeof(ActionInputs).GetProperty(nameof(ActionInputs.Version)).Should().BeDecoratedWith<OptionAttribute>();
        var versionOptionAttr = inputs.GetAttrFromProp<OptionAttribute>(nameof(ActionInputs.Version));
        versionOptionAttr.LongName.Should().Be("version");
        versionOptionAttr.Required.Should().BeTrue();
        versionOptionAttr.HelpText.Should().Be("The version of the NuGet package to check.  This is not case-sensitive.");

        inputs.PackageName.Should().BeEmpty();
        typeof(ActionInputs).GetProperty(nameof(ActionInputs.FailWhenNotFound)).Should().BeDecoratedWith<OptionAttribute>();

        var failWhenNotFoundOptionAttr = inputs.GetAttrFromProp<OptionAttribute>(nameof(ActionInputs.FailWhenNotFound));
        failWhenNotFoundOptionAttr.LongName.Should().Be("fail-when-not-found");
        failWhenNotFoundOptionAttr.Required.Should().BeFalse();
        failWhenNotFoundOptionAttr.Default.Should().Be(false);
        failWhenNotFoundOptionAttr.HelpText
            .Should().Be("If true, will fail the workflow if the NuGet package of the requested version does not exist.");
    }
    #endregion
}
