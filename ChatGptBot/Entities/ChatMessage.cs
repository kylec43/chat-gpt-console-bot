using System.Text.Json.Serialization;

namespace ChatGptBot.Entities;

public record struct ChatMessage
{
    [JsonPropertyName("role")]
    public string Role { get; init; }

    [JsonPropertyName("content")]
    public string Content { get; init; }
}
