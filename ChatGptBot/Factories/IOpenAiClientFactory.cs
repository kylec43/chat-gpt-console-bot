using ChatGptBotProject.Clients;

namespace ChatGptBotProject.Factories;

internal interface IOpenAiClientFactory
{
    IHttpClient CreateOpenAiClient();
}
