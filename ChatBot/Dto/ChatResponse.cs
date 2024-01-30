using ChatBot.Enum;

namespace ChatBot.Dto;

public record struct ChatResponse
{
    public Message Message { get; init; }
    public string MessageContent => Message.Content;
    public Identifier MessageIdentifier => Message.Identifier;
}
