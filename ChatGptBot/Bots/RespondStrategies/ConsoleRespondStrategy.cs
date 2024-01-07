using ChatGptConsoleBot.Bots.ResponseStrategies;
using ChatGptConsoleBot.Dto;

namespace ChatGptConsoleBot.Bots.RespondStrategies;

internal class ConsoleRespondStrategy : IRespondStrategy
{
    public void Respond(ChatResponse response)
    {
        Console.WriteLine("--------------------------------");
        Console.WriteLine(response.Message);
    }
}
