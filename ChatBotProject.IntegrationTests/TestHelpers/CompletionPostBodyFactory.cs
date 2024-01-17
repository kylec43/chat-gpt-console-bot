using ChatGptBotProject.Collections.CompletionApi;
using ChatGptBotProject.Constants;
using ChatGptBotProject.Dto.CompletionApi;

namespace ChatGptBotProject.IntegrationTests.TestHelpers;

static internal class CompletionPostBodyFactory
{
    public static CompletionPostBody CreateFake(string model)
    {
        return new CompletionPostBody
        {
            Messages = new Messages
            {
                new Message { Role = ChatRole.USER, Content = "How are you?" }
            },
            Model = model
        };
    }

}
