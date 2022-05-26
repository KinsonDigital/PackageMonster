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
    /// Gets or sets the message to send.
    /// </summary>
    [Option(
        "message",
        Required = true,
        HelpText = "Prints a message to the console.")]
    public string Message { get; set; } = string.Empty;
}
