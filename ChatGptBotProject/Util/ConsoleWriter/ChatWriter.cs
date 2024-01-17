namespace ChatGptBotProject.Util.ConsoleWriter;

internal class ChatWriter : IConsoleWriter
{
    private string nameOfChatter;
    public string Identifier => this.GetIdentifier();
    public int LineLength { get; }

    public ChatWriter(string nameOfChatter, int lineLength = 60)
    {
        this.nameOfChatter = nameOfChatter;
        this.LineLength = lineLength;
    }

    public void Write(string message)
    {
        Console.Write($"{this.Identifier}\n{message}");
    }

    public void WriteLine(string message)
    {
        Console.WriteLine($"{this.Identifier}\n{message}");
    }

    private string GetIdentifier()
    {
        var numberOfDashes =  this.LineLength - this.nameOfChatter.Length;
        if (numberOfDashes <= 0) 
        {
            return this.nameOfChatter;
        }

        var numberOfDashesInFront = (int)(numberOfDashes / 2);
        var dashesToAdd = numberOfDashes - (numberOfDashesInFront * 2);
        var numberOfDashesInBack = numberOfDashesInFront + dashesToAdd;
        var frontDashes = new String('-', numberOfDashesInFront);
        var backDashes = new String('-', numberOfDashesInBack);
        return $"{frontDashes}{this.nameOfChatter}{backDashes}";
    }
}
