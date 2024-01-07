using ChatGptConsoleBot.Dto.CompletionApi;

namespace ChatGptConsoleBot.Services;

internal interface ICompletionService
{
    Task<CompletionResponse> Chat(CompletionPostBody body);
}
