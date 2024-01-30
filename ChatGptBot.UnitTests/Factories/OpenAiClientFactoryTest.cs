using ChatGptBotProject.Factories;
using Configuration;

namespace ChatGptBot.UnitTests.Factories;

internal class OpenAiClientFactoryTest
{
    private OpenAiConfig config;
    private OpenAiClientFactory factory;

    [SetUp]
    public void SetUp()
    {
        this.config = new OpenAiConfig
        {
            BaseUrl = "https://api.openai.com"
        };

        this.factory = new OpenAiClientFactory(config);
    }

    [Test]
    public void CreateCompletionClient_CanCreateClientWithCorrectBaseUrl()
    {
        // Act
        var client = this.factory.CreateCompletionClient();

        // Assert
        var expectedUrl = $"{this.config.BaseUrl}/v1/chat/completions";
        Assert.That(client.BaseAddress?.ToString(), Is.EqualTo(expectedUrl));
    }
}
