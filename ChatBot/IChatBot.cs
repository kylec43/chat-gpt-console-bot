using ChatBot.Collections;
using ChatBot.Dto;

namespace ChatBot;

public interface IChatBot
{
    Task<ChatResponse> Chat(Messages messages);
}
