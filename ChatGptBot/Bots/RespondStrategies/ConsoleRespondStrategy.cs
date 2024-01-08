using ChatGptConsoleBot.Bots.ResponseStrategies;
using ChatGptConsoleBot.Dto;
using ChatGptConsoleBot.Util.ConsoleWriter;

namespace ChatGptConsoleBot.Bots.RespondStrategies;

internal class ConsoleRespondStrategy : IRespondStrategy
{
    private IConsoleWriter writer;

    public ConsoleRespondStrategy(IConsoleWriter writer)
    {
        this.writer = writer;
    }

    public void Respond(ChatResponse response)
    {
        this.writer.WriteLine(response.Message);
    }
}
