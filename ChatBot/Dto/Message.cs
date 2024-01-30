using ChatBot.Enum;

namespace ChatBot.Dto;

public record struct Message
{
    public Identifier Identifier { get; init; }
    public string Content { get; init; }
}
