using System.Text.Json.Serialization;

namespace ChatGptConsoleBot.Dto.OpenAi;

internal class LogProbs
{
    [JsonPropertyName("content")]
    public List<Token>? Tokens { get; set; }
}
