using ChatGptBotProject.Bots;
using ChatGptBotProject.Bots.ResponseStrategies;
using ChatGptBotProject.Dto.Config;
using ChatGptBotProject.Factories;
using ChatGptBotProject.Services;
using Moq;


namespace ChatBotUnitTests.Bots;

internal class ChatGptBotTest
{
    private Mock<IRespondStrategy> respondStrategyMock;
    private ChatGptBot chatBot;

    [SetUp]
    public void SetUp()
    {
        var configFactory = new JsonConfigFactory();
        var config = configFactory.Create();
        var clientFactory = new OpenAiClientFactory(config.OpenAi);
        var completionService = new CompletionService(clientFactory);
        this.respondStrategyMock = new Mock<IRespondStrategy>();
        this.chatBot = new ChatGptBot(completionService, this.respondStrategyMock.Object, config.OpenAi);

    }

    // This test will consume API Tokens
    [Test, Category("OpenAiTest"), Explicit]
    public async Task Chat_CanPassChatResponseToResponder_WhenChatting()
    {
        // Arrange
        var messagesToSend = new List<string>
        {
            "How are you?",
            "This is a test message to you",
            "Can you hear me?"
        };

        // Act
        foreach (var message in messagesToSend)
        {
            await this.chatBot.Chat(message);
        }

        // Assert
        this.respondStrategyMock
            .Verify(r => r.Respond(It.IsAny<string>()), Times.Exactly(messagesToSend.Count));
    }
}