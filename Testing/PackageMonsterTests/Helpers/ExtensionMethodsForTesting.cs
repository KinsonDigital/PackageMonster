// <copyright file="ExtensionMethodsForTesting.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace PackageMonsterTests.Helpers;

using System.Reflection;
using FluentAssertions.Execution;

/// <summary>
/// Provides extension/helper methods to assist in unit testing.
/// </summary>
public static class ExtensionMethodsForTesting
{
    /// <summary>
    /// Gets an attribute of type <typeparamref name="T"/> on a property of the object
    /// that matches with the name <paramref name="propName"/>.
    /// </summary>
    /// <param name="value">The object that contains the property.</param>
    /// <param name="propName">The name of the property on the object.</param>
    /// <typeparam name="T">The type of attribute on the property.</typeparam>
    /// <returns>The existing attribute.</returns>
    /// <exception cref="AssertionFailedException">
    ///     Thrown if the property or attribute does not exist.
    /// </exception>
    public static T GetAttrFromProp<T>(this object value, string propName)
        where T : Attribute
    {
        var props = value.GetType().GetProperties();
        var noPropsAssertMsg = string.IsNullOrEmpty(propName)
            ? $"Cannot get an attribute on a property when the '{nameof(propName)}' parameter is null or empty."
            : $"Cannot get an attribute on a property when no property with the name '{propName}' exists.";

        if (props.Length <= 0)
        {
            var exceptionMsg = $"{noPropsAssertMsg}\nExpected: at least 1 item.\nActual: was 0 items.";
            throw new AssertionFailedException(exceptionMsg);
        }

        var propNotFoundAssertMsg = $"Cannot get an attribute on the property '{propName}' if the property does not exist.";
        var foundProp = (from p in props
            where p.Name == propName
            select p).FirstOrDefault();

        if (foundProp is null)
        {
            var exceptionMsg = $"{propNotFoundAssertMsg}\nExpected: not to be null.\nActual: was null.";
            throw new AssertionFailedException(exceptionMsg);
        }

        var noAttrsAssertMsg = $"Cannot get an attribute when the property '{propName}' does not have any attributes.";
        var attrs = foundProp.GetCustomAttributes<T>().ToArray();

        if (attrs.Length <= 0)
        {
            var exceptionMsg = $"{noAttrsAssertMsg}\nExpected: at least 1 item.\nActual: was 0 items.";
            throw new AssertionFailedException(exceptionMsg);
        }

        return attrs[0];
    }
}
