// Copyright (c) Microsoft. All rights reserved.

using System.Reflection;
using console_chat;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

//var b = new HostBuilder();
//b.UseConsoleLifetime();
var hb = new HostApplicationBuilder();
hb.Configuration.AddJsonFile("appsettings.json");
Assembly asm = Assembly.GetEntryAssembly()!;
hb.Configuration.AddUserSecrets(asm, optional: false, reloadOnChange: false);
hb.Services.AddSingleton<IHostLifetime, ConsoleLifetime>();
hb.Services.AddHostedService<ChatService>();

var host = hb.Build();
var conf = host.Services.GetRequiredService<IConfiguration>();
var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();
await host.RunAsync(lifetime.ApplicationStopping);
