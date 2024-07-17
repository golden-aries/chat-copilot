//// Copyright (c) Microsoft. All rights reserved.

//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.SemanticKernel.AI.ChatCompletion;

//namespace TxExperiment;
//public class TxAzSerialziableChatResult : IChatResult
//{
//    private readonly IChatResult _src;

//    public TxAzSerialziableChatResult(IChatResult src)
//    {
//        this._src = src ?? throw new ArgumentNullException(nameof(src));
//    }

//    public Task<ChatMessageBase> GetChatMessageAsync(CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }
//}
