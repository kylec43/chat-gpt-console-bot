using ChatGptConsoleBot.Api;

namespace ChatGptConsoleBot.Factories;

internal interface IOpenAiClientFactory
{
    IHttpClient CreateOpenAiClient();
}
