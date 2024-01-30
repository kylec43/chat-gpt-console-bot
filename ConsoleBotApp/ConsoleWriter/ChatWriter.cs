namespace ConsoleBotApp.ConsoleWriter;

internal class ChatWriter : IConsoleWriter
{
    private string nameOfChatter;
    public string Identifier => GetIdentifier();
    public int LineLength { get; }

    public ChatWriter(string nameOfChatter, int lineLength = 60)
    {
        this.nameOfChatter = nameOfChatter;
        LineLength = lineLength;
    }

    public void Write(string message)
    {
        Console.Write($"{Identifier}\n{message}");
    }

    public void WriteLine(string message)
    {
        Console.WriteLine($"{Identifier}\n{message}");
    }

    private string GetIdentifier()
    {
        var numberOfDashes = LineLength - nameOfChatter.Length;
        if (numberOfDashes <= 0)
        {
            return nameOfChatter;
        }

        var numberOfDashesInFront = numberOfDashes / 2;
        var dashesToAdd = numberOfDashes - numberOfDashesInFront * 2;
        var numberOfDashesInBack = numberOfDashesInFront + dashesToAdd;
        var frontDashes = new string('-', numberOfDashesInFront);
        var backDashes = new string('-', numberOfDashesInBack);
        return $"{frontDashes}{nameOfChatter}{backDashes}";
    }
}
