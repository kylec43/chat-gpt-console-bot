using ChatGptBotProject.Dto.CompletionApi;

namespace ChatGptBotProject.Services;

public interface ICompletionService
{
    Task<CompletionResponse> Chat(CompletionPostBody body);
}
