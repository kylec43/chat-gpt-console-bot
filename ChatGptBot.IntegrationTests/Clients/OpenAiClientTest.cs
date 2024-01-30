using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ChatGptBot.Clients;
using ChatGptBot.Entities;
using ChatGptBot.IntegrationTests.TestHelpers;
using Configuration;

namespace ChatGptBot.IntegrationTests.Clients;

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
        var postBody = ChatContextFactory.CreateFake(this.openAiConfig.GptModel);
        var serializedBody = JsonSerializer.Serialize(postBody);
        var httpContent = new StringContent(serializedBody, Encoding.UTF8, new MediaTypeHeaderValue("application/json"));

        // Act
        var httpResponse = await this.client.Post("/v1/chat/completions", httpContent);

        // Assert
        var stringContent = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<ChatGptResponse>(stringContent);
        ChatGptResponseAsserter.AssertResponse(response);
    }
}
