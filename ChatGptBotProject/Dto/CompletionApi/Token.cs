﻿using System.Text.Json.Serialization;

namespace ChatGptBotProject.Dto.CompletionApi;

internal record struct Token
{
    [JsonPropertyName("token")]
    public string TokenValue { get; init; }

    [JsonPropertyName("logprob")]
    public int Logprob { get; init; }

    [JsonPropertyName("bytes")]
    public List<int> Bytes { get; init; }

    [JsonPropertyName("top_logprobs")]
    public List<Token>? TopLogprobs { get; init; }


}
