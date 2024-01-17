namespace ChatGptBotProject.Dto.Config;

internal record struct Config
{
    public OpenAiConfig OpenAi { get; set; }
}
