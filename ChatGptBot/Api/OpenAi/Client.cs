using System.Net.Http.Headers;
using ChatGptConsoleBot.Dto.OpenAi;

namespace ChatGptConsoleBot.Api.OpenAi;

internal class Client : IHttpClient
{
    private readonly HttpClient client;

    public Client(Config config, string? relativeUri = null)
    {
        client = CreateClient(config, relativeUri);
    }

    private HttpClient CreateClient(Config config, string? relativeUri)
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
        return this.client.GetAsync(uri);
    }

    public Task<HttpResponseMessage> Post(string? uri = null, HttpContent? httpContent = null)
    {
        return this.client.PostAsync(uri, httpContent);
    }
}
