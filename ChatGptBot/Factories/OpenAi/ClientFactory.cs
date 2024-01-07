using ChatGptConsoleBot.Api;
using ChatGptConsoleBot.Api.OpenAi;
using ChatGptConsoleBot.Dto.OpenAi;

namespace ChatGptConsoleBot.Factories.OpenAi;

internal class ClientFactory : IClientFactory
{
    private Config CreateConfig()
    {
        var baseUrl = "https://api.openai.com";
        var apiKey = "";
        return new Config(baseUrl, apiKey);
    }

    private IHttpClient CreateClient(Config config, string? relativeUri = null)
    {
        return new Client(config, relativeUri);
    }

    public IHttpClient CreateOpenAiClient()
    {
        var config = this.CreateConfig();
        return this.CreateClient(config);
    }

    public IHttpClient CreateCompletionClient()
    {
        var config = this.CreateConfig();
        var relativeUri = "/v1/chat/completions";
        return this.CreateClient(config, relativeUri);
    }
}
