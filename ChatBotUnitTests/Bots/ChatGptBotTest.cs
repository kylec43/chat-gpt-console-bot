using ChatGptConsoleBot.Bots;
using ChatGptConsoleBot.Bots.ResponseStrategies;
using ChatGptConsoleBot.Collections.CompletionApi;
using ChatGptConsoleBot.Constants;
using ChatGptConsoleBot.Dto.CompletionApi;
using ChatGptConsoleBot.Dto.Config;
using ChatGptConsoleBot.Exceptions;
using ChatGptConsoleBot.Services;
using Moq;


namespace ChatBotUnitTests.Bots;

public class ChatGptBotTest
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
        this.config = new OpenAiConfig();
        this.config.GptModel = GptModelName.GPT_3_TURBO;
        this.config.SystemContext = new List<string> { "Random Context" };
        this.chatBot = new ChatGptBot(this.completionServiceMock.Object, this.respondStrategyMock.Object, config);
        
    }

    public CompletionResponse CreateFakeResponse(string responseMessage, int choiceCount)
    {
        var choices = new List<Choice>();
        for (int i = 0; i < choiceCount; i++)
        {
            var choice = new Choice
            {
                Message = new Message
                {
                    Content = responseMessage
                }
            };

            choices.Add(choice);
        }

        return new CompletionResponse
        {
            Choices = choices
        };
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
}
