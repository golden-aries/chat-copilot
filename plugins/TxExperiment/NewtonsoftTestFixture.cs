// Copyright (c) Microsoft. All rights reserved.

using System.Text.Json;
using Azure.AI.OpenAI;
using TxExperiment;
//using Newtonsoft.Json;

namespace PrivateTests;
internal static class NewtonsoftTestFixture
{
    /// <summary>
    /// Serialize
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string Serialize(ChatCompletions obj)
    {
        //var result = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented)
        var result = JsonSerializer.Serialize(obj, SerializerEx.Options );
        return result;
    }

    public static ChatCompletions? DeSerialize(string obj)
    {
        //var result = JsonConvert.DeserializeObject<ChatCompletions>(obj);
        var result = JsonSerializer.Deserialize<ChatCompletions>(obj);
        return result;
    }
}
