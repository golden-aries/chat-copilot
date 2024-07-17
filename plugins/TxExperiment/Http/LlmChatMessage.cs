// Copyright (c) Microsoft. All rights reserved.

namespace TxExperiment.Http;

/// <summary>
/// Tx implementation of chat message
/// </summary>
public class LlmChatMessage
{
    /// <summary>
    /// Role
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// Content
    /// </summary>
    public string? Content { get; set; }
}
