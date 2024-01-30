using ChatGptBot.Clients;

namespace ChatGptBotProject.Factories;

public interface IOpenAiCompletionClientFactory
{
    IHttpClient CreateCompletionClient();
}
