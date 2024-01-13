using ChatGptBotProject.Bots.RespondStrategies;
using ChatGptBotProject.Util.ConsoleWriter;
using Moq;

namespace ChatBotUnitTests.Bots.RespondStrategies;

public class ConsoleRespondStrategyTest
{
    [Test]
    public void Respond_CanWriteToConsole_WhenGivenMessage()
    {
        // Arrange
        var message = "Hello World";
        var writer = new Mock<IConsoleWriter>();
        var strategy = new ConsoleRespondStrategy(writer.Object);

        // Act
        strategy.Respond(message);

        // Assert
        writer.Verify(w => w.WriteLine(message), Times.Once());
    }
}
