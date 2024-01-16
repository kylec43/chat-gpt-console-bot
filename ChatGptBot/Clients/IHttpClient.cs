namespace ChatGptBotProject.Clients;

internal interface IHttpClient
{
    Uri? BaseAddress { get; }
    Task<HttpResponseMessage> Get(string? uri);
    Task<HttpResponseMessage> Post(string? uri = null, HttpContent? httpContent = null);
}
