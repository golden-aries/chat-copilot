// Copyright (c) Microsoft. All rights reserved.

using System.Text.Json;

namespace TxExperiment;
internal static class SerializerEx
{
    /// <summary>
    /// serialization options
    /// </summary>
    public static JsonSerializerOptions Options = new() { WriteIndented = true };
}
