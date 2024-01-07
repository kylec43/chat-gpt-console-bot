﻿using System.Text;
using System.Text.Json;
using ChatGptConsoleBot.Api;
using ChatGptConsoleBot.Dto.CompletionApi;
using ChatGptConsoleBot.Factories;
using ChatGptConsoleBot.JsonConverters.OpenAi;

namespace ChatGptConsoleBot.Services;

internal class CompletionService : ICompletionService
{
    private IHttpClient completionClient;

    public CompletionService(IOpenAiClientFactory clientFactory)
    {
        completionClient = clientFactory.CreateCompletionClient();
    }

    public async Task<CompletionResponse> Chat(CompletionPostBody body)
    {
        var httpContent = BuildHttpContent(body);
        var response = await completionClient.Post(null, httpContent);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<CompletionResponse>(json);
    }

    private HttpContent BuildHttpContent(CompletionPostBody body)
    {
        var json = SerializePostBody(body);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private string SerializePostBody(CompletionPostBody body)
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new CompletionPostBodyConverter());
        return JsonSerializer.Serialize(body, options);
    }
}