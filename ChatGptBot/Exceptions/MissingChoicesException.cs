namespace ChatGptConsoleBot.Exceptions;

public class MissingChoicesException : Exception
{
    public MissingChoicesException(string? message): base(message) 
    { 
    }
}
