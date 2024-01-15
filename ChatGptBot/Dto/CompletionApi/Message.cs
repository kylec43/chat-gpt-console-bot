using System.Text.Json.Serialization;

namespace ChatGptBotProject.Dto.CompletionApi;

internal record struct Message
{
    [JsonPropertyName("role")]
    public string Role { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }
}
