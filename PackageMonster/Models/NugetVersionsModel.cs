// <copyright file="NugetVersionsModel.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace PackageMonster.Models;

/// <summary>
/// Holds information about a NuGet package.
/// </summary>
/// <remarks>
///     This information comes from www.nuget.org
/// </remarks>
public record NugetVersionsModel
{
    /// <summary>
    /// Gets or sets the list of versions available for a NuGet package.
    /// </summary>
    public string[] Versions { get; set; } = Array.Empty<string>();
}
