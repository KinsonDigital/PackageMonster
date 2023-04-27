// <copyright file="GitHubAction.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace PackageMonster;

using Exceptions;
using Services;

/// <inheritdoc/>
public sealed class GitHubAction : IGitHubAction
{
    private readonly IGitHubConsoleService gitHubConsoleService;
    private readonly INugetDataService nugetDataService;
    private readonly IActionOutputService actionOutputService;
    private bool isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="GitHubAction"/> class.
    /// </summary>
    /// <param name="gitHubConsoleService">Writes to the console.</param>
    /// <param name="nugetDataService">Provides access to NuGet data.</param>
    /// <param name="actionOutputService">Sets the output data of the action.</param>
    public GitHubAction(
        IGitHubConsoleService gitHubConsoleService,
        INugetDataService nugetDataService,
        IActionOutputService actionOutputService)
    {
        this.gitHubConsoleService = gitHubConsoleService;
        this.nugetDataService = nugetDataService;
        this.actionOutputService = actionOutputService;
    }

    /// <inheritdoc/>
    public async Task Run(ActionInputs inputs, Action onCompleted, Action<Exception> onError)
    {
        ShowWelcomeMessage();

        try
        {
            this.gitHubConsoleService.Write($"Searching for package '{inputs.PackageName} v{inputs.Version}' . . . ");
            var versions = await this.nugetDataService.GetNugetVersions(inputs.PackageName);

            var versionFound = versions
                .Any(version =>
                    string.Equals(version, inputs.Version, StringComparison.CurrentCultureIgnoreCase));

            var searchEndMsg = versionFound ? "package found!!" : "package not found!!";

            this.gitHubConsoleService.WriteLine(searchEndMsg);
            this.gitHubConsoleService.BlankLine();

            this.actionOutputService.SetOutputValue("nuget-exists", versionFound.ToString().ToLower());

            var emoji = inputs.FailWhenNotFound is false
                ? "✅"
                : string.Empty;

            var foundResultMsg = $"{emoji}The NuGet package '{inputs.PackageName}'";
            foundResultMsg += $" with the version 'v{inputs.Version}' was{(versionFound ? string.Empty : " not")} found.";

            if (versionFound is false)
            {
                if (inputs.FailWhenNotFound is true)
                {
                    throw new NugetNotFoundException(foundResultMsg);
                }
            }

            this.gitHubConsoleService.BlankLine();
            this.gitHubConsoleService.WriteLine(foundResultMsg);
            this.gitHubConsoleService.BlankLine();
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
        if (this.isDisposed)
        {
            return;
        }

        this.nugetDataService.Dispose();

        this.isDisposed = true;
    }

    /// <summary>
    /// Shows a welcome message.
    /// </summary>
    private void ShowWelcomeMessage()
    {
        this.gitHubConsoleService.WriteLine("Welcome To The GotNuget GitHub Action!! 🍫");
        this.gitHubConsoleService.BlankLine();
    }
}
