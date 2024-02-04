using ChatBot.Enum;

namespace ChatBot.Dto;

public record struct Message
{
    public Role Role { get; init; }
    public string Content { get; init; }
}
