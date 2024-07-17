// Copyright (c) Microsoft. All rights reserved.

using System.Reflection;
using System.Text.Json;
using Azure.AI.OpenAI;

namespace TxExperiment;

/// <summary>
/// completion serializer
/// </summary>
public static class TxChatCompletionsSerializer
{
    public static ChatCompletions? DeserializeChatCompletions(JsonElement element)
    {
        var method = typeof(ChatCompletions).GetMethod(
            "DeserializeChatCompletions",
            BindingFlags.Static | BindingFlags.NonPublic);

        var result = method.Invoke(typeof(ChatCompletions), new object[] { element });
        return result as ChatCompletions;
    }
}
