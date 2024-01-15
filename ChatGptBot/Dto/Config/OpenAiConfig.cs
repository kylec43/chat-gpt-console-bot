﻿namespace ChatGptBotProject.Dto.Config;

internal record struct OpenAiConfig
{
    public string BaseUrl { get; set; }
    public string ApiKey { get; set; }
    public string GptModel { get; set; }
    public List<string> SystemContext { get; set; }
}
