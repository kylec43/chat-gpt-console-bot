using ChatGptBot.Config;

namespace Configuration.IntegrationTests;

internal class JsonConfigFactoryTest
{
    private JsonConfigFactory configFactory;

    [SetUp]
    public void SetUp()
    {
        configFactory = new JsonConfigFactory();
    }

    [Test]
    public void Create_CanCreateTheConfigCorrectly()
    {
        // Act
        Config config = configFactory.Create();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(config.OpenAi);
            if (config.OpenAi is IOpenAiConfig openAi)
            {
                Assert.NotNull(openAi.GptModel);
                Assert.NotNull(openAi.ApiKey);
                Assert.NotNull(openAi.SystemContext);
                Assert.NotNull(openAi.BaseUrl);
            }
        });
    }
}
