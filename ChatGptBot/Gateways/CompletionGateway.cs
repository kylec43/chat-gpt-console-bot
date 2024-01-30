using System.Text;
using System.Text.Json;
using ChatGptBot.Clients;
using ChatGptBot.Entities;
using ChatGptBotProject.Factories;

namespace ChatGptBot.Gateways;

public class CompletionGateway : IChatGptGateway
{
    private readonly IHttpClient completionClient;

    public CompletionGateway(IOpenAiCompletionClientFactory clientFactory)
    {
        this.completionClient = clientFactory.CreateCompletionClient();
    }

    public async Task<ChatGptResponse> Chat(ChatGptContext chatContext)
    {
        var httpContent = this.BuildHttpContent(chatContext);
        var response = await this.completionClient.Post(null, httpContent);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ChatGptResponse>(json);
    }

    private HttpContent BuildHttpContent(ChatGptContext chatContext)
    {
        var json = JsonSerializer.Serialize(chatContext);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}