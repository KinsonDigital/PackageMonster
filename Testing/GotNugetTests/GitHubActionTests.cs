// <copyright file="GitHubActionTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using FluentAssertions;
using GotNuget;
using GotNuget.Exceptions;
using GotNuget.Services;
using GotNugetTests.Helpers;
using Moq;

namespace GotNugetTests;

public class GitHubActionTests
{
    private readonly Mock<IGitHubConsoleService> _mockConsoleService;
    private readonly Mock<INugetDataService> _mockDataService;
    private readonly Mock<IActionOutputService> _mockActionOutputService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GitHubActionTests"/> class.
    /// </summary>
    public GitHubActionTests()
    {
        _mockConsoleService = new Mock<IGitHubConsoleService>();
        _mockDataService = new Mock<INugetDataService>();
        _mockActionOutputService = new Mock<IActionOutputService>();
    }

    #region Method Tests
    [Fact]
    public async void Run_WhenInvoked_ShowsWelcomeMessage()
    {
        var inputs = CreateInputs();
        var action = CreateAction();

        // Act
        await action.Run(inputs, () => { }, _ => { });

        // Assert
        _mockConsoleService.VerifyOnce(m => m.WriteLine("Welcome To The GotNuget GitHub Action!!"));
        _mockConsoleService.VerifyOnce(m => m.BlankLine());
    }

    [Theory]
    [InlineData("test-package", "4.5.6", "true")]
    [InlineData("TEST-PACKAGE", "4.5.6", "true")]
    [InlineData("test-package", "7.8.9", "false")]
    public async void Run_WhenNugetPackageWithVersionExists_SetsOutputToCorrectValue(
        string packageName,
        string version,
        string expectedOutput)
    {
        // Arrange
        _mockDataService.Setup(m => m.GetNugetVersions(packageName))
            .ReturnsAsync(new[] { "1.2.3", "4.5.6" });
        var onCompletedInvoked = false;

        var inputs = CreateInputs(packageName, version);
        var action = CreateAction();

        // Act
        var act = () => action.Run(inputs, () => onCompletedInvoked = true, _ => { });

        // Assert
        await act.Should().NotThrowAsync();
        _mockActionOutputService.VerifyOnce(m => m.SetOutputValue("nuget-exists", expectedOutput));
        onCompletedInvoked.Should().BeTrue("the 'onCompleted()' was never invoked");
    }

    [Fact]
    public async void Run_WhenPackageIsNotFoundWithFailSetToTrue_ThrowsException()
    {
        // Arrange
        var inputs = CreateInputs();
        var action = CreateAction();

        // Act
        var act = () => action.Run(inputs, () => { }, e => throw e);

        // Assert
        await act.Should()
            .ThrowAsync<NugetNotFoundException>()
            .WithMessage($"The nuget package '{inputs.PackageName}' with the version '{inputs.Version}' was not found.");
    }

    [Fact]
    public async void Run_WhenAnExceptionIsThrown_InvokesOnErrorActionParam()
    {
        // Arrange
        _mockDataService.Setup(m => m.GetNugetVersions(It.IsAny<string>()))
            .Callback<string>(_ => throw new Exception("test-exception"));
        Exception? exception = null;

        var inputs = CreateInputs();
        var action = CreateAction();

        // Act
        await action.Run(inputs, () => { }, e => exception = e);

        // Assert
        exception.Should().NotBeNull("because the 'onError()' action parameter was supposed to be executed.");
        exception.Message.Should().Be("test-exception");
    }

    [Fact]
    public void Dispose_WhenInvoked_DisposesOfAction()
    {
        // Arrange
        var action = CreateAction();

        // Act
        action.Dispose();
        action.Dispose();

        // Assert
        _mockDataService.VerifyOnce(m => m.Dispose());
    }
    #endregion

    /// <summary>
    /// Creates a new instance of <see cref="ActionInputs"/> for the purpose of testing.
    /// </summary>
    /// <returns>The instance to test.</returns>
    private static ActionInputs CreateInputs(string packageName = "test-package", string version = "1.2.3") => new ()
    {
        PackageName = packageName,
        Version = version,
    };

    /// <summary>
    /// Creates a new instance of <see cref="GitHubAction"/> for the purpose of testing.
    /// </summary>
    /// <returns>The instance to test.</returns>
    private GitHubAction CreateAction()
        => new (_mockConsoleService.Object, _mockDataService.Object, _mockActionOutputService.Object);
}
