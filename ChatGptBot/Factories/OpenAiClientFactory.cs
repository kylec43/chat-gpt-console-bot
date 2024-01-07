using ChatGptConsoleBot.Api;
using ChatGptConsoleBot.Api.OpenAi;
using ChatGptConsoleBot.Dto;

namespace ChatGptConsoleBot.Factories;

internal class OpenAiClientFactory : IOpenAiClientFactory
{
    private OpenAiConfig CreateConfig()
    {
        var baseUrl = "https://api.openai.com";
        var apiKey = "";
        return new OpenAiConfig(baseUrl, apiKey);
    }

    private IHttpClient CreateClient(OpenAiConfig config, string? relativeUri = null)
    {
        return new OpenAiClient(config, relativeUri);
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
