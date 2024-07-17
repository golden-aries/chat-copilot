// Copyright (c) Microsoft. All rights reserved.

using Microsoft.SemanticKernel.ChatCompletion;

namespace TxExperiment.Http;
public class TokenCounter
{
    /// <summary>
    /// Calculate the number of tokens in a string using custom SharpToken token counter implementation with cl100k_base encoding.
    /// </summary>
    /// <param name="text">The string to calculate the number of tokens in.</param>
    public static int TokenCount(string text)
    {
        var tokenizer = SharpToken.GptEncoding.GetEncoding("cl100k_base");
        var tokens = tokenizer.Encode(text);
        return tokens.Count;
    }

    /// <summary>
    /// Rough token costing of ChatHistory's message object.
    /// Follows the syntax defined by Azure OpenAI's ChatMessage object: https://learn.microsoft.com/en-us/azure/ai-services/openai/reference#chatmessage
    /// e.g., "message": {"role":"assistant","content":"Yes }
    /// </summary>
    /// <param name="authorRole">Author role of the message.</param>
    /// <param name="content">Content of the message.</param>
    public static int GetContextMessageTokenCount(AuthorRole authorRole, string content)
    {
        var tokenCount = authorRole == AuthorRole.System ? TokenCount("\n") : 0;
        return tokenCount + TokenCount($"role:{authorRole.Label}") + TokenCount($"content:{content}");
    }


    /// <summary>
    /// Rough token costing of ChatCompletionContextMessages object.
    /// </summary>
    public static int GetContextMessagesTokenCount(IEnumerable<LlmChatMessage> messages)
    {
        var tokenCount = 0;
        foreach (var message in messages)
        {
            tokenCount += GetContextMessageTokenCount(message.Role!, message.Content!);
        }

        return tokenCount;
    }

    /// <summary>
    /// Rough token costing of ChatHistory's message object.
    /// Follows the syntax defined by Azure OpenAI's ChatMessage object: https://learn.microsoft.com/en-us/azure/ai-services/openai/reference#chatmessage
    /// e.g., "message": {"role":"assistant","content":"Yes }
    /// </summary>
    /// <param name="authorRole">Author role of the message.</param>
    /// <param name="content">Content of the message.</param>
    public static int GetContextMessageTokenCount(string role, string content)
    {
        var tokenCount = string.Equals("system", role, StringComparison.OrdinalIgnoreCase) ? TokenCount("\n") : 0;
        return tokenCount + TokenCount($"role:{role}") + TokenCount($"content:{content}");
    }
}
