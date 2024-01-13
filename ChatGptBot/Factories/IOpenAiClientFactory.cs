using ChatGptBotProject.Api;

namespace ChatGptBotProject.Factories;

internal interface IOpenAiClientFactory
{
    IHttpClient CreateOpenAiClient();
}
