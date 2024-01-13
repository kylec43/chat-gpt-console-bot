using System.Text.Json.Serialization;

namespace ChatGptBotProject.Dto.CompletionApi;

public record struct LogProbs
{
    [JsonPropertyName("content")]
    public List<Token>? Tokens { get; set; }
}
