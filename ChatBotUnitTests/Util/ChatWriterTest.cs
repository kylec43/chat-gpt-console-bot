using ChatGptBotProject.Util.ConsoleWriter;

namespace ChatGptBotProject.UnitTests.Util;

internal class ChatWriterTest
{
    [Test]
    public void LineLength_CanReturnTheCorrectLineLength()
    {
        // Arrange
        var expectedLineLength = 100;
        var writer = new ChatWriter("", expectedLineLength);

        // Act
        var length = writer.LineLength;

        // Assert
        Assert.That(length, Is.EqualTo(expectedLineLength));
    }

    [Test, Sequential]
    public void Identifier_CanReturnReturnTheIdentifier(
        [Values(7, 6, 4, 3)] int lineLength,
        [Values("-John--", "-John-", "John", "John")] string expectedIdentifier)
    {
        // Arrange
        var nameOfChatter = "John";
        var writer = new ChatWriter(nameOfChatter, lineLength);

        // Act
        var identifier = writer.Identifier;

        // Assert
        Assert.That(identifier, Is.EqualTo(expectedIdentifier));
    }
}
