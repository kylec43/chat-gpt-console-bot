using ChatGptConsoleBot.Api;

namespace ChatGptConsoleBot.Factories;

internal interface IOpenAiCompletionClientFactory
{
    IHttpClient CreateCompletionClient();
}
