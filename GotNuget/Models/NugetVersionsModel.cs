// <copyright file="NugetVersionsModel.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace GotNuget.Models;

/// <summary>
/// Holds information about a nuget package.
/// </summary>
/// <remarks>
///     This information comes from www.nuget.org
/// </remarks>
public record NugetVersionsModel
{
    /// <summary>
    /// Gets or sets the list of versions available for a nuget package.
    /// </summary>
    public string[] Versions { get; set; } = Array.Empty<string>();
}
