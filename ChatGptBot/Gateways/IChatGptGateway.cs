using ChatGptBot.Entities;

namespace ChatGptBot.Gateways;

public interface IChatGptGateway
{
    Task<ChatGptResponse> Chat(ChatGptContext chatContext);
}
