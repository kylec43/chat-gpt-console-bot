using System.Text.Json.Serialization;
using ChatGptBotProject.Collections.CompletionApi;

namespace ChatGptBotProject.Dto.CompletionApi;

internal record struct CompletionResponse
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
