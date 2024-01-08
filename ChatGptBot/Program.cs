using ChatGptConsoleBot.Bots;
using ChatGptConsoleBot.Bots.RespondStrategies;
using ChatGptConsoleBot.Factories;
using ChatGptConsoleBot.Services;
using ChatGptConsoleBot.Util.ConsoleWriter;

class Program
{
    static async Task Main(string[] args)
    {
        var writer = new ChatWriter("User");
        var bot = CreateBot();

        while (true)
        {
            writer.Write("");
            var message = Console.ReadLine();
            if (message == "exit")
            {
                return;
            }

            await bot.Chat(message!);
        }
    }

    static ChatGptBot CreateBot()
    {
        var configFactory = new JsonConfigFactory();
        var config = configFactory.Create();
        var clientFactory = new OpenAiClientFactory(config.OpenAi);
        var completionService = new CompletionService(clientFactory);
        var writer = new ChatWriter("Chat Gpt");
        var respondStrategy = new ConsoleRespondStrategy(writer);
        return new ChatGptBot(completionService, respondStrategy, config.OpenAi);
    }
}