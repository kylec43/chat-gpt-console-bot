using System.Net;
using ChatGptBot.Clients;
using ChatGptBot.Entities;
using ChatGptBot.Gateways;
using ChatGptBotProject.Factories;
using Moq;

namespace ChatGptBot.UnitTests.Gateways;

internal class CompletionGatewayTest
{
    private Mock<IHttpClient> client;
    private CompletionGateway completionGateway;

    [SetUp]
    public void SetUp()
    {
        var clientFactory = new Mock<IOpenAiCompletionClientFactory>();
        this.client = new Mock<IHttpClient>();
        clientFactory
            .Setup(c => c.CreateCompletionClient())
            .Returns(this.client.Object);

        this.completionGateway = new CompletionGateway(clientFactory.Object);
    }

    [Test]
    public void Chat_CanThrowAnHttpRequestException_WhenStatusCodeIsNotSuccessful(
        [Values(100, 300, 400, 500)] int statusCode)
    {
        // Arrange
        var httpResponse = new HttpResponseMessage((HttpStatusCode)statusCode);
        this.client
            .Setup(c => c.Post(null, It.IsAny<HttpContent>()))
            .Returns(Task.FromResult(httpResponse));

        var chatContext = new ChatGptContext();

        // Act & Assert
        Assert.ThrowsAsync< HttpRequestException>(async () => await this.completionGateway.Chat(chatContext));
    }
}
