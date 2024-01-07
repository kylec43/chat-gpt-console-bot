using ChatGptConsoleBot.Dto;
using ChatGptConsoleBot.Dto.OpenAi;

namespace ChatGptConsoleBot.Services.OpenAi;

internal interface ICompletionService
{
    Task<CompletionResponse> Chat(CompletionPostBody body);
}
