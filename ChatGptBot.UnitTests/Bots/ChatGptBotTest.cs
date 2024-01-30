using ChatBot.Collections;
using ChatBot.Dto;
using ChatBot.Enum;
using ChatGptBot.Bots;
using ChatGptBot.Constants;
using ChatGptBot.Entities;
using ChatGptBot.Exceptions;
using ChatGptBot.Gateways;
using Configuration;
using Moq;


namespace ChatGptBot.UnitTests.Bots;

internal class ChatGptBotTest
{
    private Mock<IChatGptGateway> chatGptGatewayMock;
    private OpenAiConfig config;
    private ChatGpt chatBot;

    [SetUp]
    public void SetUp()
    {
        this.chatGptGatewayMock = new Mock<IChatGptGateway>();
        this.config = new OpenAiConfig
        {
            GptModel = GptModelName.GPT_3_TURBO,
            SystemContext = new List<string> { "Random Context" }
        };

        this.chatBot = new ChatGpt(this.chatGptGatewayMock.Object, config);
    }

    [Test]
    public async Task Chat_CanPassTheCorrectChatContextToChatGptGateway()
    {
        // Arrange
        var messageContent = "Hello World";
        var messagesToSend = new Messages
        {
            new Message { Identifier = Identifier.User, Content = messageContent }
        };

        var firstChoiceResponseMessage = "Chat Response Message";
        var chatResponse = this.CreateFakeResponse(firstChoiceResponseMessage);
        this.chatGptGatewayMock
            .Setup(c => c.Chat(It.IsAny<ChatGptContext>()))
            .Returns(Task.FromResult(chatResponse));

        // Act
        var result = await this.chatBot.Chat(messagesToSend);

        // Assert
        this.chatGptGatewayMock.Verify(
            c => c.Chat(It.Is((ChatGptContext chatContext) 
                => chatContext.Messages[0].Content == this.config.SystemContext[0]
                && chatContext.Messages[0].Role == ChatRole.SYSTEM
                && chatContext.Messages[1].Content == messageContent
                && chatContext.Messages[1].Role == ChatRole.USER
            )), 
            Times.Once
        );
    }

    [Test]
    public async Task Chat_CanReturnCorrectChatResponse_WhenReceivingResponseWithMoreThanZeroChoices()
    {
        // Arrange
        var messageContent = "Hello World";
        var messagesToSend = new Messages
        {
            new Message { Identifier = Identifier.User, Content = messageContent }
        };

        var firstChoiceResponseMessage = "Chat Response Message";
        var chatGptResponse = this.CreateFakeResponse(firstChoiceResponseMessage, "Second choice message");
        this.chatGptGatewayMock
            .Setup(c => c.Chat(It.IsAny<ChatGptContext>()))
            .Returns(Task.FromResult(chatGptResponse));

        // Act
        var result = await this.chatBot.Chat(messagesToSend);

        // Assert
        Assert.That(result.MessageContent, Is.EqualTo(firstChoiceResponseMessage));
    }

    [Test]
    public void Chat_CanThrowAMissingChoicesException_WhenReceivingAResponseWithZeroChoices()
    {
        // Arrange
        var messagesToSend = new Messages
        {
            new Message { Identifier = Identifier.User, Content = "" }
        };

        var emptyResponse = this.CreateFakeResponse();
        this.chatGptGatewayMock
            .Setup(c => c.Chat(It.IsAny<ChatGptContext>()))
            .Returns(Task.FromResult(emptyResponse));

        // Act & Assert
        Assert.ThrowsAsync<MissingChoicesException>(async () => await this.chatBot.Chat(messagesToSend));
    }

    private ChatGptResponse CreateFakeResponse(params string[] responseMessages)
    {
        var choices = new List<Choice>();
        foreach (var message in responseMessages)
        {
            var assistantMessage = new ChatMessage { Role = ChatRole.ASSISTANT, Content = message };
            var choice = new Choice { Message = assistantMessage };
            choices.Add(choice);
        }

        return new ChatGptResponse { Choices = choices };
    }
}
