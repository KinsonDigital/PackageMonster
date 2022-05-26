// <copyright file="NugetNotFoundExceptionTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using FluentAssertions;
using NugetExistsChecker.Exceptions;

namespace NugetExistsCheckerTests.Exceptions;

/// <summary>
/// Tests the <see cref="NugetNotFoundException"/> class.
/// </summary>
public class NugetNotFoundExceptionTests
{
    #region Constructor Tests
    [Fact]
    public void Ctor_WithNoParam_CorrectlySetsExceptionMessage()
    {
        // Act
        var exception = new NugetNotFoundException();

        // Assert
        exception.Message.Should().Be("The nuget package was not found.");
    }

    [Fact]
    public void Ctor_WhenInvokedWithSingleMessageParam_CorrectlySetsMessage()
    {
        // Act
        var exception = new NugetNotFoundException("test-message");

        // Assert
        exception.Message.Should().Be("test-message");
    }

    [Fact]
    public void Ctor_WhenInvokedWithMessageAndInnerException_ThrowsException()
    {
        // Arrange
        var innerException = new Exception("inner-exception");

        // Act
        var deviceException = new NugetNotFoundException("test-exception", innerException);

        // Assert
        deviceException.InnerException.Message.Should().Be("inner-exception");
        deviceException.Message.Should().Be("test-exception");
    }
    #endregion
}
