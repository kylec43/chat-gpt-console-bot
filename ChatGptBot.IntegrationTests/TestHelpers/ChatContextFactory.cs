using ChatGptBot.Constants;
using ChatGptBot.Entities;

namespace ChatGptBot.IntegrationTests.TestHelpers;

static internal class ChatContextFactory
{
    public static ChatGptContext CreateFake(string model)
    {
        return new ChatGptContext
        {
            Messages = new ChatMessages
            {
                new ChatMessage { Role = ChatRole.USER, Content = "How are you?" }
            },
            Model = model
        };
    }

}
