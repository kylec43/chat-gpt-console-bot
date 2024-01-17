namespace ChatGptBotProject.Exceptions;

internal class MissingChoicesException : Exception
{
    public MissingChoicesException(string? message): base(message) 
    { 
    }
}
