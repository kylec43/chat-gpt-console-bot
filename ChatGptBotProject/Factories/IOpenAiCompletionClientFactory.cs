using ChatGptBotProject.Clients;

namespace ChatGptBotProject.Factories;

internal interface IOpenAiCompletionClientFactory
{
    IHttpClient CreateCompletionClient();
}
