// <copyright file="PackageNotFoundExceptionTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace PackageMonsterTests.Exceptions;

using FluentAssertions;
using PackageMonster.Exceptions;

/// <summary>
/// Tests the <see cref="PackageNotFoundException"/> class.
/// </summary>
public class PackageNotFoundExceptionTests
{
    #region Constructor Tests
    [Fact]
    public void Ctor_WithNoParam_CorrectlySetsExceptionMessage()
    {
        // Act
        var exception = new PackageNotFoundException();

        // Assert
        exception.Message.Should().Be("The package was not found.");
        exception.HResult.Should().Be(60);
    }

    [Fact]
    public void Ctor_WhenInvokedWithSingleMessageParam_CorrectlySetsMessage()
    {
        // Act
        var exception = new PackageNotFoundException("test-message");

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
        var deviceException = new PackageNotFoundException("test-exception", innerException);

        // Assert
        deviceException.InnerException.Message.Should().Be("inner-exception");
        deviceException.Message.Should().Be("test-exception");
        deviceException.HResult.Should().Be(60);
    }
    #endregion
}
