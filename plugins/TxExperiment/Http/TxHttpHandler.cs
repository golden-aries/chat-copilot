// Copyright (c) Microsoft. All rights reserved.

using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
//using Newtonsoft.Json;

namespace TxExperiment.Http;
/// <summary>
/// http handler
/// </summary>
public class TxHttpHandler : DelegatingHandler
{
    private static readonly DefaultContractResolver s_contractResolver = new DefaultContractResolver
    {
        NamingStrategy = new SnakeCaseNamingStrategy()
    };

    private static readonly JsonSerializerSettings s_newtonSoftJsonSettings = new JsonSerializerSettings
    {
        ContractResolver = s_contractResolver,
        Formatting = Formatting.Indented,
        //NullValueHandling = NullValueHandling.Ignore,
    };

    /// <summary>
    /// Constructs TxHttpChander instance
    /// </summary>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public TxHttpHandler(ILogger<TxHttpHandler> logger)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    private static readonly string s_prefix = "*************** LLM";

    private static readonly string s_requestsLeft = "x-ratelimit-remaining-requests";
    private static readonly string s_tokensLeft = "x-ratelimit-remaining-tokens";
    private static readonly string s_msReqId = "x-ms-client-request-id";
    private static long s_seq;
    private static readonly Stopwatch s_stopWatch = Stopwatch.StartNew();
    private readonly ILogger<TxHttpHandler> _logger;

    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        long seq = 0;
        string? reqId = null;
        try
        {
            request.Headers.TryGetValues(s_msReqId, out var reqIds);
            reqId = reqIds?.FirstOrDefault();
            _ = request.Content ??
                 throw new InvalidOperationException("HttpRequest Content is null");

            var reqStr = await (request.Content.ReadAsStringAsync(cancellationToken))
                .ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(reqStr))
            {
                throw new InvalidOperationException($"{s_prefix} | Request | Unexpected | ReqId {reqId} | {request.RequestUri}");

            }

            var holder = JsonConvert.DeserializeObject<MessagesHolder>(reqStr, s_newtonSoftJsonSettings)
                   ?? throw new Newtonsoft.Json.JsonException("Unable to deserialize ChatHistory");

            //var holder = JsonSerializer.Deserialize<MessagesHolder>(reqStr)
            //       ?? throw new JsonException("Unable to deserialize ChatHistory");

            var tokenQty = TokenCounter.GetContextMessagesTokenCount(holder.Messages!);

            TimeSpan fromPreviousRequest;
            lock (s_stopWatch)
            {
                seq = s_seq += 1;
                s_stopWatch.Stop();
                fromPreviousRequest = s_stopWatch.Elapsed;
            }
            holder.Seq = seq;
            var llmReq = JsonConvert.SerializeObject(holder, s_newtonSoftJsonSettings);
            //var llmReq = JsonSerializer.Serialize(holder, SerializerEx.Options);
            //this._logger.LogInformation(
            //    "LLM Http Request {Sequense} | Tokens {TokenQty} | ReqId {ReqId} | {RequestUri} |\n  {LlmRequest}",
            //    seq, tokenQty, reqId, request.RequestUri, llmReq);
            this._logger.LogInformation(
                "LLM Http Request {Sequense} | Tokens {TokenQty} | ReqId {ReqId} | {RequestUri} |\n  {LlmRequest}",
                seq, tokenQty, reqId, request.RequestUri, reqStr);

            //Debug.WriteLine($"{s_prefix} | {seq} Request  | Tokens {tokenQty} | ReqId {reqId} | {request.RequestUri}\n");
            //Debug.WriteLine($"{DateTime.Now} | Delay From previous: {fromPreviousRequest} \n" + llmReq);

            var resp = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            lock (s_stopWatch)
            {
                s_stopWatch.Start();
            }
            if (resp.IsSuccessStatusCode || 429 == (int)resp.StatusCode)
            {

                resp.Headers.TryGetValues(s_requestsLeft, out var requestsLeft);
                resp.Headers.TryGetValues(s_tokensLeft, out var tokensLeft);
                request.Headers.TryGetValues(s_msReqId, out var respReqIds);
                var respReqId = respReqIds?.FirstOrDefault();
                Debug.Assert(reqId == respReqId);
                var rl = requestsLeft?.FirstOrDefault();
                var tl = tokensLeft?.FirstOrDefault();

                if (resp.IsSuccessStatusCode)
                {
                    var respStr = await resp.Content
                        .ReadAsStringAsync(cancellationToken)
                        .ConfigureAwait(false);

                    this._logger.LogTrace(
               "LLM Http Response {Sequense} | Response {StatusCode} | Requests Left: {RequestsLeft} | Tokens Left: {TokensLeft} | ReqId {ReqId} | {RequestUri}\n {ResponceContent}",
                            seq, resp.StatusCode, rl, tl, reqId, request.RequestUri, respStr);
                    //resp.Content = new StringContent(reqStr);
                    //Debug.WriteLine(respStr);
                }
                else
                {
                    //Debug.WriteLine($"{s_prefix} | {seq} Response {resp.StatusCode} | Requests Left: {rl} | Tokens Left: {tl} | ReqId {respReqId}\n");
                    this._logger.LogTrace("LLM Http Response {Sequense} | Response {StatusCode} | Requests Left: {RequestsLeft} | Tokens Left: {TokensLeft} | ReqId {ReqId} | {RequestUri}",
                            seq, resp.StatusCode, rl, tl, reqId, request.RequestUri);
                }
            }
            else
            {
                this._logger.LogTrace("LLM Http Response {Sequense} | Response {StatusCode} | ReqId {ReqId} | {RequestUri}",
                           seq, resp.StatusCode, reqId, request.RequestUri);
                //Debug.WriteLine($"{s_prefix} | {s_seq} Response {resp.StatusCode}");
            }
            return resp;

        }
        catch (Exception ex)
        {
            this._logger.LogError("Error while sending LLM request {Sequense} RequestId: {RequestId} \n{Ex}", seq, reqId, ex);
            Debug.WriteLine($"{s_prefix} | {seq} Eception: ex.Message");
            throw;
        }
    }
}
