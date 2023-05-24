// <copyright file="ActionInputs.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using PackageMonster.Services;

namespace PackageMonster;

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
    /// Gets or sets the package version to check.
    /// </summary>
    /// <remarks>
    /// Version search is not case-sensitive.
    /// </remarks>
    [Option(
        "version",
        Required = true,
        HelpText = "The version of the package to check.  This is not case-sensitive.")]
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the repository.
    /// </summary>
    [Option(
        "source",
        Required = false,
        HelpText = "The source repository to check.  Defaults to `nuget`.")]
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the json path to extract the versions.
    /// </summary>
    [Option(
        "json-path",
        Required = false,
        HelpText = "The json path to the versions.")]
    public string VersionsJsonPath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether or not the action will fail if the package was not found.
    /// </summary>
    [Option(
        "fail-when-not-found",
        Required = false,
        Default = false,
        HelpText = "If true, will fail the workflow if the package of the requested version does not exist.")]
    public bool? FailWhenNotFound { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether or not the action will fail if the package was found.
    /// </summary>
    [Option(
        "fail-when-found",
        Required = false,
        Default = false,
        HelpText = "If true, will fail the workflow if the package of the requested version does exist.")]
    public bool? FailWhenFound { get; set; }
}
