// <copyright file="ActionOutputService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace PackageMonster.Services;

using System.IO.Abstractions;
using System.Text;
using Exceptions;
using Guards;

/// <inheritdoc/>
public class ActionOutputService : IActionOutputService
{
    private const string GitHubOutput = "GITHUB_OUTPUT";
    private readonly IEnvVarService envVarService;
    private readonly IFile file;
    private readonly IGitHubConsoleService consoleService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActionOutputService"/> class.
    /// </summary>
    /// <param name="envVarService">Manages environment variables.</param>
    /// <param name="file">Manages files.</param>
    public ActionOutputService(IEnvVarService envVarService, IFile file, IGitHubConsoleService consoleService)
    {
        EnsureThat.CtorParamIsNotNull(envVarService);
        EnsureThat.CtorParamIsNotNull(file);
        EnsureThat.CtorParamIsNotNull(consoleService);

        this.envVarService = envVarService;
        this.file = file;
        this.consoleService = consoleService;

    }

    /// <inheritdoc/>
    public void SetOutputValue(string name, string value)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new NullOrEmptyStringException($"The parameter '{nameof(name)}' must not be null or empty.");
        }

        var outputPath = this.envVarService.GetEnvironmentVariable(GitHubOutput);

        if (string.IsNullOrEmpty(outputPath))
        {
            this.consoleService.WriteLine($"WARNING: The environment variable '{GitHubOutput}' was not specified.");
            return;
        }

        if (this.file.Exists(outputPath) is false)
        {
            throw new FileNotFoundException("The GitHub output file was not found.", outputPath);
        }

        var outputLines = this.file.ReadAllLines(outputPath).ToList();
        outputLines.Add($"{name}={value}");

        var fileContent = new StringBuilder();

        foreach (var line in outputLines)
        {
            fileContent.AppendLine(line);
        }

        this.file.WriteAllText(outputPath, fileContent.ToString());
    }
}
