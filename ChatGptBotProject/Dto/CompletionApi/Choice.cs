using System.Text.Json.Serialization;

namespace ChatGptBotProject.Dto.CompletionApi;

internal record struct Choice
{
    [JsonPropertyName("index")]
    public int Index { get; init; }

    [JsonPropertyName("message")]
    public Message Message { get; init; }

    [JsonPropertyName("logprobs")]
    public LogProbs? LogProbs { get; init; }

    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; init; }
}
