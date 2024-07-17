// Copyright (c) Microsoft. All rights reserved.

namespace TxExperiment.Http;
/// <summary>
/// LlmResponseMessageHolder
/// </summary>
public class LlmResponseMessageHolder
{
    /// <summary>
    /// Lllm request id
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Object
    /// </summary>
    public string? Object { get; set; }


}

/// <summary>
/// Llm Response Choice
/// </summary>
public class LlmResponseChoice
{
    /// <summary>
    /// FinishReason
    /// </summary>
    public string? FinishReason { get; set; }

    /// <summary>
    /// Response Choise Index
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Content
    /// </summary>
    public string? Content { get; set; }

    
}
