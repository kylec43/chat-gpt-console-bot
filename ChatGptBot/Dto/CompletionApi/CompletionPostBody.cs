using ChatGptConsoleBot.Collections.CompletionApi;

namespace ChatGptConsoleBot.Dto.CompletionApi;

internal record struct CompletionPostBody
{
    public Messages Messages { get; set; }
    public string Model { get; set; }

    public CompletionPostBody(Messages messages, string model)
    {
        Messages = messages;
        Model = model;
    }
}
