using System.Net.Http.Headers;
using ChatGptBotProject.Dto.Config;

namespace ChatGptBotProject.Api;

internal class OpenAiClient : IHttpClient
{
    private readonly HttpClient client;

    public OpenAiClient(OpenAiConfig config, string? relativeUri = null)
    {
        client = CreateClient(config, relativeUri);
    }

    private HttpClient CreateClient(OpenAiConfig config, string? relativeUri)
    {
        var client = new HttpClient();
        var auth = new AuthenticationHeaderValue("Bearer", config.ApiKey);
        client.DefaultRequestHeaders.Authorization = auth;
        var baseUri = new Uri(config.BaseUrl);
        client.BaseAddress = new Uri(baseUri, relativeUri);
        return client;
    }

    public Task<HttpResponseMessage> Get(string? uri = null)
    {
        return client.GetAsync(uri);
    }

    public Task<HttpResponseMessage> Post(string? uri = null, HttpContent? httpContent = null)
    {
        return client.PostAsync(uri, httpContent);
    }
}
