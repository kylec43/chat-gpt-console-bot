using ChatGptBotProject.Dto.Config;
using ChatGptBotProject.Factories;
using ChatGptBotProject.IntegrationTests.TestHelpers;
using ChatGptBotProject.Services;

namespace ChatGptBotProject.IntegrationTests.Services;

internal class CompletionServiceTest
{
    private CompletionService completionService;
    private OpenAiConfig openAiConfig;

    [SetUp]
    public void SetUp()
    {
        var configFactory = new JsonConfigFactory();
        var config = configFactory.Create();
        this.openAiConfig = config.OpenAi;
        var clientFactory = new OpenAiClientFactory(this.openAiConfig);
        this.completionService = new CompletionService(clientFactory);
    }

    // This test will consume API Tokens
    [Test, Category("OpenAiTest"), Explicit]
    public async Task Chat_CanReturnCorrectCompletionResponse_WhenHttpResponseIsValid()
    {
        // Arrange
        var postBody = CompletionPostBodyFactory.CreateFake(this.openAiConfig.GptModel);

        // Act
        var response = await this.completionService.Chat(postBody);

        // Assert
        CompletionResponseAsserter.AssertResponse(response);
    }

   
}
