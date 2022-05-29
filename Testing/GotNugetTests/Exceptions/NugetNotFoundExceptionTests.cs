// <copyright file="NugetNotFoundExceptionTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using FluentAssertions;
using GotNuget.Exceptions;

namespace GotNugetTests.Exceptions;

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
        exception.HResult.Should().Be(60);
    }

    [Fact]
    public void Ctor_WhenInvokedWithSingleMessageParam_CorrectlySetsMessage()
    {
        // Act
        var exception = new NugetNotFoundException("test-message");

        // Assert
        exception.Message.Should().Be("test-message");
        exception.HResult.Should().Be(60);
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
        deviceException.HResult.Should().Be(60);
    }
    #endregion
}
