using System.Text;
using System.Text.Json;
using ChatGptBotProject.Clients;
using ChatGptBotProject.Dto.CompletionApi;
using ChatGptBotProject.Factories;
using ChatGptBotProject.JsonConverters;

namespace ChatGptBotProject.Services;

internal class CompletionService : ICompletionService
{
    private IHttpClient completionClient;

    public CompletionService(IOpenAiCompletionClientFactory clientFactory)
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
