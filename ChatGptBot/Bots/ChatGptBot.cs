using ChatGptBotProject.Bots.ResponseStrategies;
using ChatGptBotProject.Collections.CompletionApi;
using ChatGptBotProject.Constants;
using ChatGptBotProject.Dto;
using ChatGptBotProject.Dto.CompletionApi;
using ChatGptBotProject.Dto.Config;
using ChatGptBotProject.Exceptions;
using ChatGptBotProject.Services;

namespace ChatGptBotProject.Bots;

public class ChatGptBot
{
    private ICompletionService completionService;
    private IRespondStrategy respondStrategy;
    private CompletionPostBody postBody;
    private OpenAiConfig config;

    public ChatGptBot(ICompletionService completionService, IRespondStrategy respondStrategy, OpenAiConfig config)
    {
        this.completionService = completionService;
        this.respondStrategy = respondStrategy;
        this.postBody = new CompletionPostBody
        {
            Model = config.GptModel,
            Messages = new Messages()
        };

        this.config = config;
        this.AddSystemContextToPostBody(this.config, this.postBody);
    }

    private void AddSystemContextToPostBody(OpenAiConfig config, CompletionPostBody postBody)
    {
        foreach (var content in config.SystemContext)
        {
            postBody.AddMessage(new Message { Role = ChatRole.SYSTEM, Content = content });
        }
    }

    public async Task Chat(string message)
    {
        var newMessage = new Message
        {
            Role = ChatRole.USER,
            Content = message
        };

        this.postBody.AddMessage(newMessage);
        var completionResponse = await completionService.Chat(postBody);
        var choiceMessages = completionResponse.ChoiceMessages;
        
        var isMissingChoiceMessages = choiceMessages is not Messages || choiceMessages.Count == 0;
        if (isMissingChoiceMessages)
        {
            throw new MissingChoicesException("Completion Service Response does not contain any choices");
        }

        var firstChoiceMessage = completionResponse.ChoiceMessages.First();
        this.postBody.AddMessage(firstChoiceMessage);
        this.respondStrategy.Respond(firstChoiceMessage.Content);
    }
}
