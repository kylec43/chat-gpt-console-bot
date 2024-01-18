using System.Text.Json.Serialization;
using ChatGptBotProject.Collections.CompletionApi;

namespace ChatGptBotProject.Dto.CompletionApi;

internal record struct CompletionPostBody
{
    [JsonPropertyName("messages")]
    public Messages Messages { get; init; }

    [JsonPropertyName("model")]
    public string Model { get; init; }

    public CompletionPostBody AddMessage(Message message)
    {
        Messages.Add(message);
        return this;
    }
}
