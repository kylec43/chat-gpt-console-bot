using ChatGptConsoleBot.Bots.ResponseStrategies;
using ChatGptConsoleBot.Dto;
using ChatGptConsoleBot.Util.ConsoleWriter;

namespace ChatGptConsoleBot.Bots.RespondStrategies;

public class ConsoleRespondStrategy : IRespondStrategy
{
    private IConsoleWriter writer;

    public ConsoleRespondStrategy(IConsoleWriter writer)
    {
        this.writer = writer;
    }

    public void Respond(string response)
    {
        this.writer.WriteLine(response);
    }
}
