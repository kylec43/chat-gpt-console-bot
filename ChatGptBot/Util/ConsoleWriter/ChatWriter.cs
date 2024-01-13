namespace ChatGptBotProject.Util.ConsoleWriter;

internal class ChatWriter : IConsoleWriter
{
    private string nameOfChatter;

    public ChatWriter(string nameOfChatter)
    {
        this.nameOfChatter = nameOfChatter;
    }

    public void Write(string message)
    {
        var identifier = this.GetIdentifier();
        Console.Write($"{identifier}\n{message}");
    }

    public void WriteLine(string message)
    {
        var identifier = this.GetIdentifier();
        Console.WriteLine($"{identifier}\n{message}");
    }

    private string GetIdentifier()
    {
        const int LINE_LENGTH = 60;
        var numberOfDashes =  LINE_LENGTH - this.nameOfChatter.Length;
        var numberOfDashesInFront = (int)(numberOfDashes / 2);
        var dashesToAdd = numberOfDashes - (numberOfDashesInFront * 2);
        var numberOfDashesInBack = numberOfDashesInFront + dashesToAdd;
        var frontDashes = new String('-', numberOfDashesInFront);
        var backDashes = new String('-', numberOfDashesInBack);
        return $"{frontDashes}{this.nameOfChatter}{backDashes}";
    }
}
