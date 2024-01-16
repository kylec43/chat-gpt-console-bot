
using ChatGptBotProject.Dto.Config;
using ChatGptBotProject.Factories;

namespace ChatGptBotProject.IntegrationTests.Factories;

internal class JsonConfigFactoryTest
{
    private JsonConfigFactory configFactory;

    [SetUp]
    public void SetUp()
    {
        this.configFactory = new JsonConfigFactory();
    }

    [Test]
    public void Create_CanCreateTheConfigCorrectly()
    {
        // Act
        Config config = this.configFactory.Create();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(config.OpenAi);
            if (config.OpenAi is OpenAiConfig openAi)
            {
                Assert.NotNull(openAi.GptModel);
                Assert.NotNull(openAi.ApiKey);
                Assert.NotNull(openAi.SystemContext);
                Assert.NotNull(openAi.BaseUrl);
            }
        });
    }
}
