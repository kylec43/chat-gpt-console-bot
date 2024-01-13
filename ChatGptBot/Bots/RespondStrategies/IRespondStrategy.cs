using ChatGptBotProject.Dto;

namespace ChatGptBotProject.Bots.ResponseStrategies;

public interface IRespondStrategy
{
    void Respond(string response);
}
