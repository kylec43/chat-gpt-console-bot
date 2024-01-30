namespace ConsoleBotApp.ConsoleWriter;

public interface IChatWriterFactory
{
    IConsoleWriter Create(string nameOfChatter, int lineLength);
}
