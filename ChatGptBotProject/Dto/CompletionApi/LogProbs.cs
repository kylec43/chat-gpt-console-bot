using System.Text.Json.Serialization;

namespace ChatGptBotProject.Dto.CompletionApi;

internal record struct LogProbs
{
    [JsonPropertyName("content")]
    public List<Token>? Tokens { get; init; }
}
