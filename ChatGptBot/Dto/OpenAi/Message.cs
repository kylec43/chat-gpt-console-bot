using System.Text.Json.Serialization;

namespace ChatGptConsoleBot.Dto.OpenAi;

internal record struct Message
{
    [JsonPropertyName("role")]
    public string Role { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    public Message(string role, string content)
    {
        Role = role;
        Content = content;
    }
}
