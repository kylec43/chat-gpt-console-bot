using ChatGptConsoleBot.Api;

namespace ChatGptConsoleBot.Factories;

internal interface IOpenAiClientFactory
{
    public IHttpClient CreateOpenAiClient();
    public IHttpClient CreateCompletionClient();
}
