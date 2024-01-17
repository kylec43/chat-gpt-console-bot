using System.Net.Http.Headers;
using ChatGptBotProject.Dto.Config;

namespace ChatGptBotProject.Clients;

internal class OpenAiClient : IHttpClient
{
    private readonly HttpClient client;

    public Uri? BaseAddress { get => this.client.BaseAddress; }

    public OpenAiClient(
        OpenAiConfig config, 
        string? relativeUri = null, 
        HttpMessageHandler? httpMessageHandler = null)
    {
        this.client = CreateClient(config, relativeUri, httpMessageHandler);
    }

    private HttpClient CreateClient(
        OpenAiConfig config, 
        string? relativeUri, 
        HttpMessageHandler? httpMessageHandler)
    {
        var client = httpMessageHandler is not null 
            ? new HttpClient(httpMessageHandler) 
            : new HttpClient();

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
