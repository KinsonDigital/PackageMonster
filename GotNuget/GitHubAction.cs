// <copyright file="GitHubAction.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using GotNuget.Exceptions;
using GotNuget.Services;

namespace GotNuget;

/// <inheritdoc/>
public sealed class GitHubAction : IGitHubAction
{
    private readonly IGitHubConsoleService _gitHubConsoleService;
    private readonly INugetDataService _nugetDataService;
    private readonly IActionOutputService _actionOutputService;
    private bool _isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="GitHubAction"/> class.
    /// </summary>
    /// <param name="gitHubConsoleService">Writes to the console.</param>
    /// <param name="nugetDataService">Provides access to nuget data.</param>
    /// <param name="actionOutputService">Sets the output data of the action.</param>
    public GitHubAction(
        IGitHubConsoleService gitHubConsoleService,
        INugetDataService nugetDataService,
        IActionOutputService actionOutputService)
    {
        _gitHubConsoleService = gitHubConsoleService;
        _nugetDataService = nugetDataService;
        _actionOutputService = actionOutputService;
    }

    /// <inheritdoc/>
    public async Task Run(ActionInputs inputs, Action onCompleted, Action<Exception> onError)
    {
        ShowWelcomeMessage();

        try
        {
            _gitHubConsoleService.Write($"Searching for package '{inputs.PackageName} v{inputs.Version}' . . . ");
            var versions = await _nugetDataService.GetNugetVersions(inputs.PackageName);

            var versionFound = versions
                .Any(version =>
                    string.Equals(version, inputs.Version, StringComparison.CurrentCultureIgnoreCase));

            var searchEndMsg = versionFound ? "package found!!" : "package not found!!";

            _gitHubConsoleService.WriteLine(searchEndMsg);
            _gitHubConsoleService.BlankLine();

            _actionOutputService.SetOutputValue("nuget-exists", versionFound.ToString().ToLower());

            var emoji = inputs.FailWhenNotFound is false
                ? "✅"
                : string.Empty;

            var foundResultMsg = $"{emoji}The nuget package '{inputs.PackageName}'";
            foundResultMsg += $" with the version 'v{inputs.Version}' was{(versionFound ? string.Empty : " not")} found.";

            if (versionFound is false)
            {
                if (inputs.FailWhenNotFound is true)
                {
                    throw new NugetNotFoundException(foundResultMsg);
                }
            }

            _gitHubConsoleService.BlankLine();
            _gitHubConsoleService.WriteLine(foundResultMsg);
            _gitHubConsoleService.BlankLine();
        }
        catch (Exception e)
        {
            onError(e);
        }

        onCompleted();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _nugetDataService.Dispose();

        _isDisposed = true;
    }

    /// <summary>
    /// Shows a welcome message with additional information.
    /// </summary>
    private void ShowWelcomeMessage()
    {
        _gitHubConsoleService.WriteLine("Welcome To The GotNuget GitHub Action!! 🍫");
        _gitHubConsoleService.BlankLine();
    }
}
