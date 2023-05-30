// <copyright file="PackageNotFoundException.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace PackageMonster.Exceptions;

/// <summary>
/// Occurs when a package is not found.
/// </summary>
public class PackageNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PackageNotFoundException"/> class.
    /// </summary>
    public PackageNotFoundException()
        : base("The package was not found.") => HResult = 60;

    /// <summary>
    /// Initializes a new instance of the <see cref="PackageNotFoundException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public PackageNotFoundException(string message)
        : base(message) => HResult = 60;

    /// <summary>
    /// Initializes a new instance of the <see cref="PackageNotFoundException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">
    ///     The <see cref="Exception"/> instance that caused the current exception.
    /// </param>
    public PackageNotFoundException(string message, Exception innerException)
        : base(message, innerException) => HResult = 60;
}
