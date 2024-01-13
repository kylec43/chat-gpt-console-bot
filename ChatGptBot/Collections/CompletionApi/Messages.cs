using System.Collections;
using ChatGptBotProject.Dto.CompletionApi;

namespace ChatGptBotProject.Collections.CompletionApi;

public class Messages : IEnumerable<Message>
{
    private List<Message> messages;

    public int Count
    {
        get => messages.Count;
    }

    public Message this[int i]
    {
        get => messages[i];
    }

    public Messages()
    {
        messages = new List<Message>();
    }

    public Messages Add(Message message)
    {
        messages.Add(message);
        return this;
    }

    public Messages AddMessages(Messages messages)
    {
        this.messages.AddRange(messages);
        return this;
    }

    public IEnumerator<Message> GetEnumerator()
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
