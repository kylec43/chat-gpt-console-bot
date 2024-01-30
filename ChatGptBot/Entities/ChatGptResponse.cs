using System.Text.Json.Serialization;

namespace ChatGptBot.Entities;

public record struct ChatGptResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("object")]
    public string Object { get; init; }

    [JsonPropertyName("created")]
    public int Created { get; init; }

    [JsonPropertyName("system_fingerprint")]
    public string? SystemFingerprint { get; init; }

    [JsonPropertyName("model")]
    public string Model { get; init; }

    [JsonPropertyName("choices")]
    public List<Choice> Choices { get; init; }

    [JsonPropertyName("usage")]
    public Usage Usage { get; init; }

    public ChatMessages ChoiceMessages
    {
        get
        {
            var messages = new ChatMessages();
            foreach (var choice in Choices)
            {
                messages.Add(choice.Message);
            }

            return messages;
        }
    }
}
