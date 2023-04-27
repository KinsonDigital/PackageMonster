// <copyright file="EnsureThatTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using PackageMonster.Guards;

namespace PackageMonsterTests.Guards;

/// <summary>
/// Holds tests for the <see cref="EnsureThatTests"/> class.
/// </summary>
public class EnsureThatTests
{
    #region Method Tests
    [Fact]
    [SuppressMessage("csharpsquid", "S3236", Justification = "Explicit on purpose for testing.")]
    public void CtorParamIsNotNull_WithNullValue_ThrowsException()
    {
        // Arrange
        string? testValue = null;

        // Act
        var act = () => EnsureThat.CtorParamIsNotNull(testValue, nameof(testValue));

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("The parameter must not be null. (Parameter 'testValue')");
    }

    [Fact]
    [SuppressMessage("csharpsquid", "S3236", Justification = "Explicit on purpose for testing.")]
    public void CtorParamIsNotNull_WhenValueIsNotNull_DoesNotThrowException()
    {
        // Arrange
        const string testValue = "test-value";

        // Act
        var act = () => EnsureThat.CtorParamIsNotNull(testValue, nameof(testValue));

        // Assert
        act.Should().NotThrow();
    }
    #endregion
}
