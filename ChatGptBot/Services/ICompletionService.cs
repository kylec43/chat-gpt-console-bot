using ChatGptBotProject.Dto.CompletionApi;

namespace ChatGptBotProject.Services;

internal interface ICompletionService
{
    Task<CompletionResponse> Chat(CompletionPostBody body);
}
