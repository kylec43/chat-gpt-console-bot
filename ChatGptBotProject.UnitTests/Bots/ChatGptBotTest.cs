using System.Linq.Expressions;
using ChatGptBotProject.Bots;
using ChatGptBotProject.Bots.ResponseStrategies;
using ChatGptBotProject.Collections.CompletionApi;
using ChatGptBotProject.Constants;
using ChatGptBotProject.Dto.CompletionApi;
using ChatGptBotProject.Dto.Config;
using ChatGptBotProject.Exceptions;
using ChatGptBotProject.Services;
using Moq;


namespace ChatBotUnitTests.Bots;

internal class ChatGptBotTest
{
    private Mock<ICompletionService> completionServiceMock;
    private Mock<IRespondStrategy> respondStrategyMock;
    private OpenAiConfig config;
    private ChatGptBot chatBot;

    [SetUp]
    public void SetUp()
    {
        this.completionServiceMock = new Mock<ICompletionService>();
        this.respondStrategyMock = new Mock<IRespondStrategy>();
        this.config = new OpenAiConfig
        {
            GptModel = GptModelName.GPT_3_TURBO,
            SystemContext = new List<string> { "Random Context" }

        };

        this.chatBot = new ChatGptBot(this.completionServiceMock.Object, this.respondStrategyMock.Object, config);
    }

    [Test]
    public async Task Chat_CanPassCorrectPostBodyToCompletionService_WhenChatting(
        [Values(1, 2, 3)] int chatCount)
    {
        // Arrange
        var messageToSend = "Test Message";
        var responseMessage = "Chat Response Message";
        var choiceCount = 1;
        var completionResponse = this.CreateFakeResponse(responseMessage, choiceCount);

        this.completionServiceMock
            .Setup(c => c.Chat(It.IsAny<CompletionPostBody>()))
            .Returns(Task.FromResult(completionResponse));

        // Act
        for (int i = 0; i < chatCount; i++)
        {
            await this.chatBot.Chat(messageToSend);
        }

        // Assert
        var expectedPostBody = this.CreateExpectedPostBody(messageToSend, responseMessage, chatCount);

        Expression<Func<CompletionPostBody, bool>> isExpectedPostBody;
        isExpectedPostBody = (postBody) => this.IsPostBodyExpectedPostBody(postBody, expectedPostBody);

        this.completionServiceMock.Verify(
            c => c.Chat(It.Is(isExpectedPostBody)), 
            Times.Exactly(chatCount)
        );
    }

    [Test]
    public async Task Chat_CanExecuteResponderWithFirstChoiceMessage_WhenReceivingResponseWithMoreThanZeroChoices(
        [Values(1, 2)] int choiceCount)
    {
        // Arrange
        var messageToSend = "Test Message";
        var responseMessage = "Chat Response Message";
        var completionResponse = this.CreateFakeResponse(responseMessage, choiceCount);

        this.completionServiceMock
            .Setup(c => c.Chat(It.IsAny<CompletionPostBody>()))
            .Returns(Task.FromResult(completionResponse));

        // Act
        await this.chatBot.Chat(messageToSend);

        // Assert
        this.respondStrategyMock
            .Verify(r => r.Respond(responseMessage), Times.Once());
    }

    [Test]
    public void Chat_CanThrowAMissingChoicesException_WhenReceivingAResponseWithZeroChoices()
    {
        // Arrange
        var messageToSend = "Test Message";
        var responseMessage = "Chat Response Message";
        var choiceCount = 0;
        var completionResponse = this.CreateFakeResponse(responseMessage, choiceCount);

        this.completionServiceMock
            .Setup(c => c.Chat(It.IsAny<CompletionPostBody>()))
            .Returns(Task.FromResult(completionResponse));

        // Act & Assert
        Assert.ThrowsAsync<MissingChoicesException>(async () => await this.chatBot.Chat(messageToSend));
    }

    private CompletionPostBody CreateExpectedPostBody(string messageToSend, string responseMessage, int chatCount)
    {
        var messages = new Messages();
        var systemContextMessages = this.GetSystemContextMessages();
        messages.AddMessages(systemContextMessages);

        for (int i = 0; i < chatCount; i++)
        {
            var userMessage = new Message { Role = ChatRole.USER, Content = messageToSend };
            var assistantMessage = new Message { Role = ChatRole.ASSISTANT, Content = responseMessage };
            messages.Add(userMessage);
            messages.Add(assistantMessage);
        }

        return new CompletionPostBody { Model = this.config.GptModel, Messages = messages };
    }

    private Messages GetSystemContextMessages()
    {
        var messages = new Messages();
        foreach (var content in this.config.SystemContext)
        {
            var systemMessage = new Message { Role = ChatRole.SYSTEM, Content = content };
            messages.Add(systemMessage);
        }

        return messages;
    }

    private CompletionResponse CreateFakeResponse(string responseMessage, int choiceCount = 1)
    {
        var choices = new List<Choice>();
        for (int i = 0; i < choiceCount; i++)
        {
            var assistantMessage = new Message { Role = ChatRole.ASSISTANT, Content = responseMessage };
            var choice = new Choice { Message = assistantMessage };
            choices.Add(choice);
        }

        return new CompletionResponse { Choices = choices };
    }

    private bool IsPostBodyExpectedPostBody(CompletionPostBody postBody, CompletionPostBody expectedPostBody)
    {
        var expectedMessages = expectedPostBody.Messages;
        var messages = postBody.Messages;
        
        // Verify Message Count
        if (messages.Count != expectedMessages.Count)
        {
            return false;
        }

        // Verify Message Content
        for (int i = 0; i < messages.Count; i++)
        {
            if (messages[i] != expectedMessages[i])
            {
                return false;
            }
        }

        // Verify Model
        return postBody.Model == expectedPostBody.Model;
    }
}
