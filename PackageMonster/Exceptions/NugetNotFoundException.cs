// <copyright file="NugetNotFoundException.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace GotNuget.Exceptions;

/// <summary>
/// Occurs when a NuGet package is not found.
/// </summary>
public class NugetNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NugetNotFoundException"/> class.
    /// </summary>
    public NugetNotFoundException()
        : base("The NuGet package was not found.") => HResult = 60;

    /// <summary>
    /// Initializes a new instance of the <see cref="NugetNotFoundException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public NugetNotFoundException(string message)
        : base(message) => HResult = 60;

    /// <summary>
    /// Initializes a new instance of the <see cref="NugetNotFoundException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">
    ///     The <see cref="Exception"/> instance that caused the current exception.
    /// </param>
    public NugetNotFoundException(string message, Exception innerException)
        : base(message, innerException) => HResult = 60;
}
