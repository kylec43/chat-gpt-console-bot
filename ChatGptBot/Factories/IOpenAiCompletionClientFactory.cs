using ChatGptBotProject.Clients;

namespace ChatGptBotProject.Factories;

public interface IOpenAiCompletionClientFactory
{
    IHttpClient CreateCompletionClient();
}
