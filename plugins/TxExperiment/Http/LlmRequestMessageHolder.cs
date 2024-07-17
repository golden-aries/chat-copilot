// Copyright (c) Microsoft. All rights reserved.

namespace TxExperiment.Http;
/// <summary>
/// Holds Messages
/// </summary>
public class MessagesHolder
{
    /// <summary>
    /// Request sequential number
    /// </summary>
    public long? Seq { get; set; }

    /// <summary>
    /// Messages
    /// </summary>
    public List<LlmChatMessage>? Messages { get; set; }
}
