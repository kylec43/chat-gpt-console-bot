﻿namespace ChatGptConsoleBot.Api;

internal interface IHttpClient
{
    Task<HttpResponseMessage> Get(string? uri);
    Task<HttpResponseMessage> Post(string? uri = null, HttpContent? httpContent = null);
}