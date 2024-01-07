using System.Text.Json.Serialization;

namespace ChatGptConsoleBot.Dto.CompletionApi;

internal record struct Token
{
    [JsonPropertyName("token")]
    public string TokenValue { get; set; }

    [JsonPropertyName("logprob")]
    public int Logprob { get; set; }

    [JsonPropertyName("bytes")]
    public List<int> Bytes { get; set; }

    [JsonPropertyName("top_logprobs")]
    public List<Token>? TopLogprobs { get; set; }


}
