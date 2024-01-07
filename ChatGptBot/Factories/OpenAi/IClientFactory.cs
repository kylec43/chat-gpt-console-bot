using ChatGptConsoleBot.Api;

namespace ChatGptConsoleBot.Factories.OpenAi;

internal interface IClientFactory
{
    public IHttpClient CreateOpenAiClient();
    public IHttpClient CreateCompletionClient();
}
