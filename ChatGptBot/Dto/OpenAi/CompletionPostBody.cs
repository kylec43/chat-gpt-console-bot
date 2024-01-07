using ChatGptConsoleBot.Collections.OpenAi;

namespace ChatGptConsoleBot.Dto.OpenAi;

internal record struct CompletionPostBody
{
    public Messages Messages { get; set; }
    public string Model { get; set; }

    public CompletionPostBody(Messages messages, string model)
    {
        Messages = messages;
        this.Model = model;
    }
}
