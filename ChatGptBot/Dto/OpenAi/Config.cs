namespace ChatGptConsoleBot.Dto.OpenAi;

internal record struct Config
{
    public string ApiKey { get; }
    public string BaseUrl { get; }

    public Config(string baseUrl, string apiKey)
    {
        BaseUrl = baseUrl;
        ApiKey = apiKey;
    }
}
