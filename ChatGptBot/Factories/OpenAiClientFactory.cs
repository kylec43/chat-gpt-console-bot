using ChatGptConsoleBot.Api;
using ChatGptConsoleBot.Dto.Config;

namespace ChatGptConsoleBot.Factories;

internal class OpenAiClientFactory : IOpenAiClientFactory, IOpenAiCompletionClientFactory
{
    private OpenAiConfig config;
    public OpenAiClientFactory(OpenAiConfig config)
    {
        this.config = config;
    }

    private IHttpClient CreateOpenAiClientWithRelativeUri(string? relativeUri)
    {
        return new OpenAiClient(this.config, relativeUri);
    }

    public IHttpClient CreateOpenAiClient()
    {
        return this.CreateOpenAiClientWithRelativeUri(null);
    }

    public IHttpClient CreateCompletionClient()
    {
        return this.CreateOpenAiClientWithRelativeUri("/v1/chat/completions");
    }
}
