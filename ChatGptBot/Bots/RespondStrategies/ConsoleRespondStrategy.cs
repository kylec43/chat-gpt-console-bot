using ChatGptBotProject.Bots.ResponseStrategies;
using ChatGptBotProject.Dto;
using ChatGptBotProject.Util.ConsoleWriter;

namespace ChatGptBotProject.Bots.RespondStrategies;

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
