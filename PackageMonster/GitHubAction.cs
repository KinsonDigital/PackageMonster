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
    private readonly IDataService dataService;
    private readonly IActionOutputService actionOutputService;
    private bool isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="GitHubAction"/> class.
    /// </summary>
    /// <param name="gitHubConsoleService">Writes to the console.</param>
    /// <param name="dataService">Provides access to data.</param>
    /// <param name="actionOutputService">Sets the output data of the action.</param>
    public GitHubAction(
        IGitHubConsoleService gitHubConsoleService,
        IDataService dataService,
        IActionOutputService actionOutputService)
    {
        this.gitHubConsoleService = gitHubConsoleService;
        this.dataService = dataService;
        this.actionOutputService = actionOutputService;
    }

    /// <inheritdoc/>
    public async Task Run(ActionInputs inputs, Action onCompleted, Action<Exception> onError)
    {
        ShowWelcomeMessage();

        try
        {
            this.gitHubConsoleService.Write($"Searching for package '{inputs.PackageName} v{inputs.Version}' . . . ");
            var versions = await this.dataService.GetVersions(inputs.PackageName, inputs.Source, inputs.VersionsJsonPath);

            var versionFound = versions
                .Any(version =>
                    string.Equals(version, inputs.Version, StringComparison.CurrentCultureIgnoreCase));

            var searchEndMsg = versionFound ? "package found!!" : "package not found!!";

            this.gitHubConsoleService.WriteLine(searchEndMsg);
            this.gitHubConsoleService.BlankLine();

            this.actionOutputService.SetOutputValue("result", versionFound.ToString().ToLower());

            var emoji = inputs.FailWhenNotFound is false
                ? "✅"
                : string.Empty;

            var foundResultMsg = $"{emoji}The package '{inputs.PackageName}'";
            foundResultMsg += $" with the version 'v{inputs.Version}' was{(versionFound ? string.Empty : " not")} found.";

            if (versionFound is false)
            {
                if (inputs.FailWhenNotFound is true)
                {
                    throw new PackageNotFoundException(foundResultMsg);
                }
            }
            else
            {
                if (inputs.FailWhenFound is true)
                {
                    throw new PackageFoundException(foundResultMsg);
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

        this.dataService.Dispose();

        this.isDisposed = true;
    }

    /// <summary>
    /// Shows a welcome message.
    /// </summary>
    private void ShowWelcomeMessage()
    {
        this.gitHubConsoleService.WriteLine("Welcome To The PackageMonster GitHub Action!!");
        this.gitHubConsoleService.BlankLine();
    }
}
