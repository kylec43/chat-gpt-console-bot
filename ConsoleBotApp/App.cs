using ChatBot;
using ChatBot.Collections;
using ChatBot.Dto;
using ChatBot.Enum;
using ConsoleBotApp.ConsoleWriter;

namespace ConsoleBotApp;

public class App
{
    private IChatBot bot;
    private IChatWriterFactory writerFactory;

    public App(IChatBot bot, IChatWriterFactory writerFactory)
    {
        this.bot = bot;
        this.writerFactory = writerFactory;
    }

    public async Task Run()
    {
        var lineLength = 60;
        var userWriter = writerFactory.Create("User", lineLength);
        var botWriter = writerFactory.Create("Bot", lineLength);
        var history = new Messages();

        while (true)
        {
            userWriter.Write("");
            var message = Console.ReadLine();
            if (message == "exit")
            {
                return;
            }

            var userMessage = new Message { Identifier = Identifier.User, Content = message! };
            history.Add(userMessage);
            var response = await bot.Chat(history);
            history.Add(response.Message);
            botWriter.WriteLine(response.MessageContent);
        }
    }
}
