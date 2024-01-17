using ChatGptBotProject.Collections.CompletionApi;
using ChatGptBotProject.Constants;
using ChatGptBotProject.Dto.CompletionApi;
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
        var postBody = new CompletionPostBody
        {
            Messages = new Messages
            {
                new Message { Role = ChatRole.USER, Content = "How are you?" }
            },
            Model = this.openAiConfig.GptModel
        };

        // Act
        var response = await this.completionService.Chat(postBody);

        // Assert
        CompletionResponseAsserter.AssertResponse(response);
    }

   
}
