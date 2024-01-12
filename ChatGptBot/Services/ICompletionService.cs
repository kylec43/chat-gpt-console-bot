using ChatGptConsoleBot.Dto.CompletionApi;

namespace ChatGptConsoleBot.Services;

public interface ICompletionService
{
    Task<CompletionResponse> Chat(CompletionPostBody body);
}
