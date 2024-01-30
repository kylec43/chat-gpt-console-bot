using ChatGptBot.Config;
using Microsoft.Extensions.DependencyInjection;
using Configuration;
using ChatGptBot.Gateways;
using ChatGptBotProject.Factories;
using ChatBot;
using ChatGptBot.Bots;
using ConsoleBotApp;
using ConsoleBotApp.ConsoleWriter;


class Program
{
    static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var provider = serviceCollection.BuildServiceProvider();
        var app = provider.GetService<App>();
        await app!.Run();
    }

    static void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IConfigFactory, JsonConfigFactory>();
        serviceCollection.AddSingleton<IOpenAiConfig>((provider) =>
        {
            var configFactory = provider.GetService<IConfigFactory>();
            var appSettings = configFactory!.Create();
            return appSettings.OpenAi;
        });

        serviceCollection.AddTransient<IChatGptGateway, CompletionGateway>();
        serviceCollection.AddTransient<IOpenAiCompletionClientFactory, OpenAiClientFactory>();
        serviceCollection.AddTransient<IChatBot, ChatGpt>();
        serviceCollection.AddTransient<IChatWriterFactory, ChatWriterFactory>();
        serviceCollection.AddTransient<App>();
    }
}