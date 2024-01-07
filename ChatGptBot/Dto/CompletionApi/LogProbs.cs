using System.Text.Json.Serialization;

namespace ChatGptConsoleBot.Dto.CompletionApi;

internal class LogProbs
{
    [JsonPropertyName("content")]
    public List<Token>? Tokens { get; set; }
}
