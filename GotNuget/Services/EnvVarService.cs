// <copyright file="EnvVarService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace GotNuget.Services;

using System.Diagnostics.CodeAnalysis;

/// <inheritdoc/>
[ExcludeFromCodeCoverage(Justification = "Directly accesses the environment.")]
public class EnvVarService : IEnvVarService
{
    /// <inheritdoc/>
    public string GetEnvironmentVariable(string name, EnvironmentVariableTarget target = EnvironmentVariableTarget.Process) =>
        Environment.GetEnvironmentVariable(name, target) ?? string.Empty;
}
