using ChatGptBotProject.Collections.CompletionApi;

namespace ChatGptBotProject.Dto.CompletionApi;

internal record struct CompletionPostBody
{
    public Messages Messages { get; set; }
    public string Model { get; set; }

    public CompletionPostBody AddMessage(Message message)
    {
        Messages.Add(message);
        return this;
    }
}
