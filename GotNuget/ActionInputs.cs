// <copyright file="ActionInputs.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace GotNuget;

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
    /// Gets or sets the NuGet package version to check.
    /// </summary>
    /// <remarks>
    /// Version search is not case-sensitive.
    /// </remarks>
    [Option(
        "version",
        Required = true,
        HelpText = "The version of the NuGet package to check.  This is not case-sensitive.")]
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether or not the action will fail if the package was not found.
    /// </summary>
    [Option(
        "fail-when-not-found",
        Required = false,
        Default = false,
        HelpText = "If true, will fail the workflow if the NuGet package of the requested version does not exist.")]
    public bool? FailWhenNotFound { get; set; }
}
