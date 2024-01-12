using ChatGptConsoleBot.Collections.CompletionApi;

namespace ChatGptConsoleBot.Dto.CompletionApi;

public record struct CompletionPostBody
{
    public Messages Messages { get; set; }
    public string Model { get; set; }

    public CompletionPostBody AddMessage(Message message)
    {
        Messages.Add(message);
        return this;
    }
}
