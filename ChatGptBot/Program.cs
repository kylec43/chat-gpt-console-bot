using ChatGptConsoleBot.Bots;
using ChatGptConsoleBot.Bots.RespondStrategies;
using ChatGptConsoleBot.Factories.OpenAi;
using ChatGptConsoleBot.Services.OpenAi;

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

        var clientFactory = new ClientFactory();
        var completionService = new CompletionService(clientFactory);
        var respondStrategy = new ConsoleRespondStrategy();
        var bot = new ChatGptBot(completionService, respondStrategy);
        await bot.Chat(message);

        Console.WriteLine("Ask chat gpt something else?");
        message = Console.ReadLine();
        if (message == null)
        {
            throw new Exception("Unable to get your question!");
        }

        await bot.Chat(message);
    }
}