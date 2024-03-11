// Copyright (c) Microsoft. All rights reserved.

using Microsoft.Extensions.Hosting;

namespace console_chat;
public class ChatService : BackgroundService
{
    private readonly IHostApplicationLifetime appLifeTime;
    private readonly IHostLifetime hostLifetime;

    public ChatService(IHostApplicationLifetime appLifeTime, IHostLifetime hostLifetime)
    {
        this.appLifeTime = appLifeTime ?? throw new ArgumentNullException(nameof(appLifeTime));
        this.hostLifetime = hostLifetime;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var source = CancellationTokenSource.CreateLinkedTokenSource(
            this.appLifeTime.ApplicationStopping,
            stoppingToken);

        string msg = "Hello There";
        while (!source.Token.IsCancellationRequested)
        {
            await Console.Out.WriteLineAsync(msg);
            var req = await Console.In.ReadLineAsync(source.Token).ConfigureAwait(false);
        }
    }
}
