using ChatGptBotProject.Api;

namespace ChatGptBotProject.Factories;

internal interface IOpenAiCompletionClientFactory
{
    IHttpClient CreateCompletionClient();
}
