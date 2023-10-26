// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.Planning;
using Microsoft.SemanticKernel.Planning.Stepwise;

var cb = new ConfigurationBuilder();
cb.AddJsonFile("appsettings.json");
var config = cb.Build();
var sc = new ServiceCollection();
sc.AddSingleton<IConfigurationRoot>(config);
sc.AddOptions<StepwisePlannerConfig>().Bind(config.GetSection(nameof(StepwisePlanner)));
var sp = sc.BuildServiceProvider();
var opts = sp.GetRequiredService<IOptions<StepwisePlannerConfig>>();
var optsm = sp.GetRequiredService<IOptionsMonitor<StepwisePlannerConfig>>();
Console.WriteLine("Done!");
