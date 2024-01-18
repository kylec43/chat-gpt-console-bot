namespace ChatGptBotProject.Dto.Config;

internal record struct OpenAiConfig
{
    public string BaseUrl { get; init; }
    public string ApiKey { get; init; }
    public string GptModel { get; init; }
    public List<string> SystemContext { get; init; }
}
