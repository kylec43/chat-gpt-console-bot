using System.Text;
using System.Text.Json;
using ChatGptBotProject.Clients;
using ChatGptBotProject.Collections.CompletionApi;
using ChatGptBotProject.Constants;
using ChatGptBotProject.Dto.CompletionApi;
using ChatGptBotProject.Dto.Config;
using ChatGptBotProject.Factories;
using ChatGptBotProject.IntegrationTests.TestHelpers;

namespace ChatGptBotProject.IntegrationTests.Clients;

internal class OpenAiClientTest
{
    private OpenAiClient client;
    private OpenAiConfig openAiConfig;

    [SetUp]
    public void SetUp()
    {
        var configFactory = new JsonConfigFactory();
        var config = configFactory.Create();
        this.openAiConfig = config.OpenAi;
        this.client = new OpenAiClient(this.openAiConfig);
    }

    // This test will consume API Tokens
    [Test, Category("OpenAiTest"), Explicit]
    public async Task Post_CanReturnValidResponse_WhenCallingCompletionApi()
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

        var serializedBody = JsonSerializer.Serialize(postBody);
        var httpContent = new StringContent(serializedBody, Encoding.UTF8, "application/json");

        // Act
        var httpResponse = await this.client.Post("/v1/chat/completions", httpContent);

        // Assert
        var stringContent = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<CompletionResponse>(stringContent);
        CompletionResponseAsserter.AssertResponse(response);
    }
}
