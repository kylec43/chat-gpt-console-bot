using ChatBot.Collections;
using ChatBot.Dto;
using ChatBot.Enum;
using ChatGptBot.Bots;
using ChatGptBot.Gateways;
using ChatGptBotProject.Factories;
using Configuration;


namespace ChatGptBot.IntegrationTests.Bots;

internal class ChatGptBotTest
{
    private ChatGpt chatBot;

    [SetUp]
    public void SetUp()
    {
        var configFactory = new JsonConfigFactory();
        var config = configFactory.Create();
        var clientFactory = new OpenAiClientFactory(config.OpenAi);
        var completionGateway = new CompletionGateway(clientFactory);
        this.chatBot = new ChatGpt(completionGateway, config.OpenAi);
    }

    // This test will consume API Tokens
    [Test, Category("OpenAiTest"), Explicit]
    public async Task Chat_CanReturnAValidChatResponse_WhenChatting()
    {
        // Arrange
        var messagesToSend = new Messages
        {
            new Message { Identifier = Identifier.User, Content = "How are you?" },
            new Message { Identifier = Identifier.Bot, Content = "Good" },
            new Message { Identifier = Identifier.User, Content = "Awesome" }
        };

        // Act
        var response = await this.chatBot.Chat(messagesToSend);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsNotNull(response.Message);
            Assert.IsNotNull(response.MessageContent);
            Assert.That(response.MessageIdentifier, Is.EqualTo(Identifier.Bot));
        });
    }
}