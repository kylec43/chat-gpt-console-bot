using ChatGptConsoleBot.Dto;

namespace ChatGptConsoleBot.Bots.ResponseStrategies;

public interface IRespondStrategy
{
    void Respond(string response);
}
