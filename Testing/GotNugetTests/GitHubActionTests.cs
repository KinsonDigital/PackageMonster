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
    private readonly Mock<IGitHubConsoleService> mockConsoleService;
    private readonly Mock<INugetDataService> mockDataService;
    private readonly Mock<IActionOutputService> mockActionOutputService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GitHubActionTests"/> class.
    /// </summary>
    public GitHubActionTests()
    {
        this.mockConsoleService = new Mock<IGitHubConsoleService>();
        this.mockDataService = new Mock<INugetDataService>();
        this.mockActionOutputService = new Mock<IActionOutputService>();
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
        this.mockConsoleService.VerifyOnce(m => m.WriteLine("Welcome To The GotNuget GitHub Action!! 🍫"));
        this.mockConsoleService.Verify(m => m.BlankLine(), Times.Exactly(2));
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
        var expectedSearchMsgStart = $"Searching for package '{packageName} v{version}' . . . ";
        var expectedSearchMsgEnd = expectedOutput == "true" ? "package found!!" : "package not found!!";

        this.mockDataService.Setup(m => m.GetNugetVersions(packageName))
            .ReturnsAsync(new[] { "1.2.3", "4.5.6" });
        var onCompletedInvoked = false;

        var inputs = CreateInputs(packageName, version, false);
        var action = CreateAction();

        // Act
        var act = () => action.Run(inputs, () => onCompletedInvoked = true, e => throw e);

        // Assert
        await act.Should().NotThrowAsync();
        var expectedResultMsg = $"✅The nuget package '{packageName}'";
        expectedResultMsg += $" with the version 'v{version}' was{(expectedOutput == "false" ? " not" : string.Empty)} found.";

        this.mockConsoleService.VerifyOnce(m => m.Write(expectedSearchMsgStart, false));
        this.mockConsoleService.VerifyOnce(m => m.WriteLine(expectedSearchMsgEnd));
        this.mockConsoleService.VerifyOnce(m => m.WriteLine(expectedResultMsg));
        this.mockConsoleService.Verify(m => m.BlankLine(), Times.Exactly(4));
        this.mockActionOutputService.VerifyOnce(m => m.SetOutputValue("nuget-exists", expectedOutput));
        onCompletedInvoked.Should().BeTrue("the 'onCompleted()' was never invoked");
    }

    [Fact]
    public async void Run_WhenPackageIsNotFoundWithFailSetToTrue_ThrowsExceptionWithFailure()
    {
        // Arrange
        var inputs = CreateInputs(failWhenNotFound: true);

        this.mockDataService.Setup(m => m.GetNugetVersions(It.IsAny<string>()))
            .ReturnsAsync(Array.Empty<string>());

        var action = CreateAction();

        // Act
        var act = () => action.Run(inputs, () => { }, e => throw e);

        // Assert
        await act.Should()
            .ThrowAsync<NugetNotFoundException>()
            .WithMessage($"The nuget package '{inputs.PackageName}' with the version 'v{inputs.Version}' was not found.");
    }

    [Theory]
    [InlineData(false)]
    [InlineData(null)]
    public async void Run_WhenPackageIsNotFoundWithFailSetToFalseOrNull_DoesNotThrowException(bool? failWhenNotFound)
    {
        // Arrange
        var inputs = CreateInputs(failWhenNotFound: failWhenNotFound);

        this.mockDataService.Setup(m => m.GetNugetVersions(It.IsAny<string>()))
            .ReturnsAsync(Array.Empty<string>());

        var action = CreateAction();

        // Act
        var act = () => action.Run(inputs, () => { }, e => throw e);

        // Assert
        await act.Should()
            .NotThrowAsync<NugetNotFoundException>();
    }

    [Fact]
    public async void Run_WhenAnExceptionIsThrown_InvokesOnErrorActionParam()
    {
        // Arrange
        this.mockDataService.Setup(m => m.GetNugetVersions(It.IsAny<string>()))
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
        this.mockDataService.VerifyOnce(m => m.Dispose());
    }
    #endregion

    /// <summary>
    /// Creates a new instance of <see cref="ActionInputs"/> for the purpose of testing.
    /// </summary>
    /// <returns>The instance to test.</returns>
    private static ActionInputs CreateInputs(
        string packageName = "test-package",
        string version = "1.2.3",
        bool? failWhenNotFound = true) => new ()
    {
        PackageName = packageName,
        Version = version,
        FailWhenNotFound = failWhenNotFound,
    };

    /// <summary>
    /// Creates a new instance of <see cref="GitHubAction"/> for the purpose of testing.
    /// </summary>
    /// <returns>The instance to test.</returns>
    private GitHubAction CreateAction()
        => new (this.mockConsoleService.Object, this.mockDataService.Object, this.mockActionOutputService.Object);
}
