using ChatGptBot.Clients;
using ChatGptBot.Config;

namespace ChatGptBotProject.Factories;

public class OpenAiClientFactory : IOpenAiCompletionClientFactory
{
    private readonly IOpenAiConfig config;
    public OpenAiClientFactory(IOpenAiConfig config)
    {
        this.config = config;
    }

    private IHttpClient CreateOpenAiClientWithRelativeUri(string? relativeUri)
    {
        return new OpenAiClient(this.config, relativeUri);
    }

    public IHttpClient CreateCompletionClient()
    {
        return this.CreateOpenAiClientWithRelativeUri("/v1/chat/completions");
    }
}
