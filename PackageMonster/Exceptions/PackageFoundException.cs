namespace PackageMonster.Exceptions;

/// <summary>
/// Occurs when a package is found.
/// </summary>
public class PackageFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PackageFoundException"/> class.
    /// </summary>
    public PackageFoundException()
        : base("The package was not found.") => HResult = 60;

    /// <summary>
    /// Initializes a new instance of the <see cref="PackageFoundException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public PackageFoundException(string message)
        : base(message) => HResult = 60;

    /// <summary>
    /// Initializes a new instance of the <see cref="PackageFoundException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">
    ///     The <see cref="Exception"/> instance that caused the current exception.
    /// </param>
    public PackageFoundException(string message, Exception innerException)
        : base(message, innerException) => HResult = 60;
}
