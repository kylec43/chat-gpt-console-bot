using System.Text.Json.Serialization;

namespace ChatGptBot.Entities;

public record struct LogProbs
{
    [JsonPropertyName("content")]
    public List<Token>? Tokens { get; init; }
}
