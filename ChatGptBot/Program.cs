using ChatGptConsoleBot.Bots;
using ChatGptConsoleBot.Bots.RespondStrategies;
using ChatGptConsoleBot.Factories;
using ChatGptConsoleBot.Services;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Ask chat gpt something");
        var message = Console.ReadLine();
        if (message == null)
        {
            throw new Exception("Unable to get your question!");
        }

        var bot = CreateBot();
        await bot.Chat(message);

        Console.WriteLine("Ask chat gpt something else?");
        message = Console.ReadLine();
        if (message == null)
        {
            throw new Exception("Unable to get your question!");
        }

        await bot.Chat(message);
    }

    static ChatGptBot CreateBot()
    {
        var configFactory = new JsonConfigFactory();
        var config = configFactory.Create();
        var clientFactory = new OpenAiClientFactory(config.OpenAi);
        var completionService = new CompletionService(clientFactory);
        var respondStrategy = new ConsoleRespondStrategy();
        return new ChatGptBot(completionService, respondStrategy, config.OpenAi);
    }
}