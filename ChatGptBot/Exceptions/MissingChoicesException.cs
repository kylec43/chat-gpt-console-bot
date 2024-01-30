namespace ChatGptBot.Exceptions;

internal class MissingChoicesException : Exception
{
    public MissingChoicesException(string? message): base(message) 
    { 
    }
}
