﻿using System.Text.Json.Serialization;

namespace ChatGptConsoleBot.Dto.OpenAi;

internal record struct Choice
{
    [JsonPropertyName("index")]
    public int Index { get; set; }

    [JsonPropertyName("message")]
    public Message Message { get; set; }

    [JsonPropertyName("logprobs")]
    public LogProbs? LogProbs { get; set; }

    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; set; }
}
