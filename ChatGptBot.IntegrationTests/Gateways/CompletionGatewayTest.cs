using ChatGptBot.Gateways;
using ChatGptBotProject.Factories;
using ChatGptBot.IntegrationTests.TestHelpers;
using Configuration;

namespace ChatGptBot.IntegrationTests.Gateways;

internal class CompletionGatewayTest
{
    private CompletionGateway completionGateway;
    private OpenAiConfig openAiConfig;

    [SetUp]
    public void SetUp()
    {
        var configFactory = new JsonConfigFactory();
        var config = configFactory.Create();
        this.openAiConfig = config.OpenAi;
        var clientFactory = new OpenAiClientFactory(this.openAiConfig);
        this.completionGateway = new CompletionGateway(clientFactory);
    }

    // This test will consume API Tokens
    [Test, Category("OpenAiTest"), Explicit]
    public async Task Chat_CanReturnAValidCompletionResponse()
    {
        // Arrange
        var chatContext = ChatContextFactory.CreateFake(this.openAiConfig.GptModel);

        // Act
        var response = await this.completionGateway.Chat(chatContext);

        // Assert
        ChatGptResponseAsserter.AssertResponse(response);
    }

   
}
