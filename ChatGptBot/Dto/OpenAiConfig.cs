namespace ChatGptConsoleBot.Dto;

internal record struct OpenAiConfig
{
    public string ApiKey { get; }
    public string BaseUrl { get; }

    public OpenAiConfig(string baseUrl, string apiKey)
    {
        BaseUrl = baseUrl;
        ApiKey = apiKey;
    }
}
