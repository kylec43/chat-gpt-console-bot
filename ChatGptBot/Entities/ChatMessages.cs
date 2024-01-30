using System.Collections;

namespace ChatGptBot.Entities;

public class ChatMessages : IEnumerable<ChatMessage>
{
    private List<ChatMessage> messages;

    public int Count
    {
        get => messages.Count;
    }

    public ChatMessage this[int i]
    {
        get => messages[i];
    }

    public ChatMessages()
    {
        messages = new List<ChatMessage>();
    }

    public ChatMessages Add(ChatMessage message)
    {
        messages.Add(message);
        return this;
    }

    public ChatMessages AddMessages(ChatMessages messages)
    {
        this.messages.AddRange(messages);
        return this;
    }

    public IEnumerator<ChatMessage> GetEnumerator()
    {
        foreach (var message in messages)
        {
            yield return message;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
