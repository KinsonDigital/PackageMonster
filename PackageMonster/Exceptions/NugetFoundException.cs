namespace PackageMonster.Exceptions;

/// <summary>
/// Occurs when a NuGet package is found.
/// </summary>
public class NugetFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NugetFoundException"/> class.
    /// </summary>
    public NugetFoundException()
        : base("The NuGet package was not found.") => HResult = 60;

    /// <summary>
    /// Initializes a new instance of the <see cref="NugetFoundException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public NugetFoundException(string message)
        : base(message) => HResult = 60;

    /// <summary>
    /// Initializes a new instance of the <see cref="NugetFoundException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">
    ///     The <see cref="Exception"/> instance that caused the current exception.
    /// </param>
    public NugetFoundException(string message, Exception innerException)
        : base(message, innerException) => HResult = 60;
}
