using System.Text.Json.Serialization;

namespace ChatGptBot.Entities;

public record struct ChatGptContext
{
    [JsonPropertyName("messages")]
    public ChatMessages Messages { get; init; }

    [JsonPropertyName("model")]
    public string Model { get; init; }

    public ChatGptContext AddMessage(ChatMessage message)
    {
        Messages.Add(message);
        return this;
    }
}
