// <copyright file="GitHubActionTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using Moq;
using NugetExistsChecker;
using NugetExistsChecker.Services;

namespace NugetExistsCheckerTests;

public class GitHubActionTests
{
    private readonly Mock<IGitHubConsoleService> _mockConsoleService;
    private readonly Mock<IActionOutputService> _mockActionOutputService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GitHubActionTests"/> class.
    /// </summary>
    public GitHubActionTests()
    {
        _mockConsoleService = new Mock<IGitHubConsoleService>();
        _mockActionOutputService = new Mock<IActionOutputService>();
    }

    #region Method Tests
    #endregion

    /// <summary>
    /// Creates a new instance of <see cref="ActionInputs"/> for the purpose of testing.
    /// </summary>
    /// <returns>The instance to test.</returns>
    private static ActionInputs CreateInputs() => new ()
    {
        Message = "test-owner",
    };

    /// <summary>
    /// Creates a new instance of <see cref="GitHubAction"/> for the purpose of testing.
    /// </summary>
    /// <returns>The instance to test.</returns>
    private GitHubAction CreateAction()
        => new (_mockConsoleService.Object, _mockActionOutputService.Object);
}
