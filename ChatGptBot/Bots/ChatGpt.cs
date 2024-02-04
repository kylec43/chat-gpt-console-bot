using ChatBot;
using ChatBot.Collections;
using ChatBot.Dto;
using ChatBot.Enum;
using ChatGptBot.Config;
using ChatGptBot.Constants;
using ChatGptBot.Entities;
using ChatGptBot.Exceptions;
using ChatGptBot.Gateways;

namespace ChatGptBot.Bots;

public class ChatGpt : IChatBot
{
    private IChatGptGateway chatGateWay;
    private IOpenAiConfig openAiConfig;

    public ChatGpt(IChatGptGateway chatGateWay, IOpenAiConfig config)
    {
        this.chatGateWay = chatGateWay;
        this.openAiConfig = config;
    }

    public async Task<ChatResponse> Chat(Messages messages)
    {
        var chatContext = this.CreateChatContext(messages);
        var chatGptResponse = await chatGateWay.Chat(chatContext);
        var firstChoiceMessage = this.GetFirstMessageFromResponse(chatGptResponse);
        return new ChatResponse
        {
            Message = new Message { Role = Role.Bot, Content = firstChoiceMessage.Content }
        };
    }

    private ChatGptContext CreateChatContext(Messages messages)
    {
        var chatMessages = this.BuildChatMessages(messages);
        return new ChatGptContext
        {
            Messages = chatMessages,
            Model = this.openAiConfig.GptModel
        };
    }

    private ChatMessages BuildChatMessages(Messages messages)
    {
        var chatMessages = new ChatMessages();
        foreach (var systemContext in this.openAiConfig.SystemContext)
        {
            var systemMessage = new ChatMessage { Role = ChatRole.SYSTEM, Content = systemContext };
            chatMessages.Add(systemMessage);
        }

        foreach (var message in messages)
        {
            var role = message.Role == Role.Bot ? ChatRole.ASSISTANT : ChatRole.USER;
            var chatMessage = new ChatMessage { Role = role, Content = message.Content };
            chatMessages.Add(chatMessage);
        }

        return chatMessages;
    }

    private ChatMessage GetFirstMessageFromResponse(ChatGptResponse response)
    {
        var choiceMessages = response.ChoiceMessages;
        var isMissingChoiceMessages = choiceMessages is not ChatMessages || choiceMessages.Count == 0;
        if (isMissingChoiceMessages)
        {
            throw new MissingChoicesException("Chat Gpt Response does not contain any choices");
        }

        return response.ChoiceMessages.First();
    }
}
