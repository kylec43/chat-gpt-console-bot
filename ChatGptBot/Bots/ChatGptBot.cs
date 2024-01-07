using System.Text;
using ChatGptConsoleBot.Bots.ResponseStrategies;
using ChatGptConsoleBot.Collections.CompletionApi;
using ChatGptConsoleBot.Constants;
using ChatGptConsoleBot.Dto;
using ChatGptConsoleBot.Dto.CompletionApi;
using ChatGptConsoleBot.Dto.OpenAi;
using ChatGptConsoleBot.Services;

namespace ChatGptConsoleBot.Bots;

internal class ChatGptBot
{
    private ICompletionService completionService;
    private IRespondStrategy respondStrategy;
    private Messages messageHistory;

    public ChatGptBot(ICompletionService completionService, IRespondStrategy respondStrategy)
    {
        this.completionService = completionService;
        this.respondStrategy = respondStrategy;
        this.messageHistory = new Messages();
    }

    public async Task Chat(string message)
    {
        var postBody = this.BuildPostBody(message);
        var completionResponse = await completionService.Chat(postBody);
        var messages = this.GetMessagesFromResponse(completionResponse);
        this.messageHistory.AddMessages(messages);
        this.RespondWithMessages(messages);
    }

    private CompletionPostBody BuildPostBody(string message)
    {
        this.messageHistory.Add(new Message("user", message));
        var model = GptModelName.GPT_3_TURBO;
        return new CompletionPostBody(this.messageHistory, model);
    }

    private Messages GetMessagesFromResponse(CompletionResponse completionResponse)
    {
        var messages = new Messages();
        foreach (var choice in completionResponse.Choices)
        {
            messages.Add(choice.Message);
        }

        return messages;
    }

    private void RespondWithMessages(Messages messages)
    {
        var messageContents = messages.Select(message => message.Content);
        var message = String.Join("\n", messageContents);
        var chatResponse = new ChatResponse { Message = message };
        this.respondStrategy.Respond(chatResponse);
    }
}
