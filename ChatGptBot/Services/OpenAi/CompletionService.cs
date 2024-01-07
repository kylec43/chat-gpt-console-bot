using System.Text;
using System.Text.Json;
using ChatGptConsoleBot.Api;
using ChatGptConsoleBot.Dto;
using ChatGptConsoleBot.Dto.OpenAi;
using ChatGptConsoleBot.Factories.OpenAi;
using ChatGptConsoleBot.JsonConverters.OpenAi;

namespace ChatGptConsoleBot.Services.OpenAi;

internal class CompletionService : ICompletionService
{
    private IHttpClient completionClient;

    public CompletionService(IClientFactory clientFactory) 
    {
        this.completionClient = clientFactory.CreateCompletionClient();
    }

    public async Task<CompletionResponse> Chat(CompletionPostBody body)
    {
        var httpContent = this.BuildHttpContent(body);
        var response = await this.completionClient.Post(null, httpContent);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<CompletionResponse>(json);
    }

    private HttpContent BuildHttpContent(CompletionPostBody body)
    {
        var json = this.SerializePostBody(body);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private string SerializePostBody(CompletionPostBody body)
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new CompletionPostBodyConverter());
        return JsonSerializer.Serialize(body, options);
    }
}
