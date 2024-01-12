using System.Text.Json.Serialization;
using ChatGptConsoleBot.Collections.CompletionApi;

namespace ChatGptConsoleBot.Dto.CompletionApi;

public record struct CompletionResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("object")]
    public string Object { get; set; }

    [JsonPropertyName("created")]
    public int Created { get; set; }

    [JsonPropertyName("system_fingerprint")]
    public string SystemFingerprint { get; set; }

    [JsonPropertyName("model")]
    public string Model { get; set; }

    [JsonPropertyName("choices")]
    public List<Choice> Choices { get; set; }

    [JsonPropertyName("usage")]
    public Usage Usage { get; set; }

    public Messages ChoiceMessages
    {
        get
        {
            var messages = new Messages();
            foreach (var choice in this.Choices)
            {
                messages.Add(choice.Message);
            }

            return messages;
        }
    }
}
