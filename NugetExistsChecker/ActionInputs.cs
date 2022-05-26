// <copyright file="ActionInputs.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace NugetExistsChecker;

/// <summary>
/// Holds all of the GitHub actions inputs.
/// </summary>
public class ActionInputs
{
    /// <summary>
    /// Gets or sets the name of the package.
    /// </summary>
    [Option(
        "package-name",
        Required = true,
        HelpText = "The name of the package.  This is not case-sensitive.")]
    public string PackageName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the package.
    /// </summary>
    [Option(
        "version",
        Required = true,
        HelpText = "The version of the nuget package to check for.  This is not case-sensitive.")]
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the package.
    /// </summary>
    [Option(
        "fail-when-not-found",
        Required = false,
        Default = false,
        HelpText = "If true, will fail the workflow if the nuget package of the requested version does not exist.")]
    public string FailWhenNotFound { get; set; } = string.Empty;
}
