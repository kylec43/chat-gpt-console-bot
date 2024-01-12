using System.Text.Json.Serialization;

namespace ChatGptConsoleBot.Dto.CompletionApi;

public record struct Message
{
    [JsonPropertyName("role")]
    public string Role { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }
}
