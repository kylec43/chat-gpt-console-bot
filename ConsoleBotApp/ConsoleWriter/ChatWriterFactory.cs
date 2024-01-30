namespace ConsoleBotApp.ConsoleWriter;

public class ChatWriterFactory : IChatWriterFactory
{
    public IConsoleWriter Create(string nameOfChatter, int lineLength)
    {
        return new ChatWriter(nameOfChatter, lineLength);
    }
}
