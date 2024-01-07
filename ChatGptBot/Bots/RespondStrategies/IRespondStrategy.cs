using ChatGptConsoleBot.Dto;

namespace ChatGptConsoleBot.Bots.ResponseStrategies;

internal interface IRespondStrategy
{
    void Respond(ChatResponse response);
}
