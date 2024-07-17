// Copyright (c) Microsoft. All rights reserved.

namespace TxExperiment.Http;
/// <summary>
/// LlmFunctionCall
/// </summary>
public class LlmFunctionCall
{
    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Arguments
    /// </summary>
    public string? Arguments { get; set; }
}
