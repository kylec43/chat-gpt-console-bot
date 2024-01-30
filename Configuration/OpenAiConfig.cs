using ChatGptBot.Config;

namespace Configuration;

public class OpenAiConfig : IOpenAiConfig
{
    public string BaseUrl { get; init; }
    public string ApiKey { get; init; }
    public string GptModel { get; init; }
    public List<string> SystemContext { get; init; }
}
