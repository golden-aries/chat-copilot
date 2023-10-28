// Copyright (c) Microsoft. All rights reserved.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Planning;
using Microsoft.SemanticKernel.Planning.Stepwise;


using IServiceScope scope = TestScope();




Console.WriteLine("Done!");

/// <summary>
/// Calculate the number of tokens in a string using custom SharpToken token counter implementation with cl100k_base encoding.
/// </summary>
/// <param name="text">The string to calculate the number of tokens in.</param>
static int TokenCount(string text)
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
static int GetContextMessageTokenCount(AuthorRole authorRole, string content)
{
    var tokenCount = authorRole == AuthorRole.System ? TokenCount("\n") : 0;
    return tokenCount + TokenCount($"role:{authorRole.Label}") + TokenCount($"content:{content}");
}


/// <summary>
/// Rough token costing of ChatCompletionContextMessages object.
/// </summary>
/// <param name="chatHistory">ChatCompletionContextMessages object to calculate the number of tokens of.</param>
static int GetContextMessagesTokenCount(ChatHistory chatHistory)
{
    var tokenCount = 0;
    foreach (var message in chatHistory.Messages)
    {
        tokenCount += GetContextMessageTokenCount(message.Role, message.Content);
    }

    return tokenCount;
}

static IServiceScope TestScope()
{
    var cb = new ConfigurationBuilder();
    cb.AddJsonFile("appsettings.json");
    var config = cb.Build();
    var sc = new ServiceCollection();
    sc.AddSingleton<IConfigurationRoot>(config);
    sc.AddScoped<FooHolder>();

    sc.AddOptions<StepwisePlannerConfig>()
        .Bind(config.GetSection(nameof(StepwisePlanner)))
        .Configure<FooHolder>((config, foo) =>
        {
            config.Suffix = foo.Instance?.Id.ToString() ?? throw new ArgumentException("Foo is null!");
        });

    var sp = sc.BuildServiceProvider();
    var scope = sp.CreateScope();
    var holder = scope.ServiceProvider.GetRequiredService<FooHolder>();
    holder.Instance = new Foo { Id = 2 };
    var o = sp.GetRequiredService<IOptions<StepwisePlannerConfig>>();
    var osc = scope.ServiceProvider.GetRequiredService<IOptions<StepwisePlannerConfig>>();
    var oscopedmonitor = scope.ServiceProvider.GetRequiredService<IOptionsMonitor<StepwisePlannerConfig>>();
    var oscoped = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<StepwisePlannerConfig>>();
    return scope;
}

public class FooHolder
{
    public Foo? Instance { get; set; }
}
public class Foo
{
    public int Id { get; set; }
    public AuthorRole User { get; set; }
}
