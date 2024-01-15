﻿using System.Dynamic;
using System.Linq.Expressions;
using System.Net;
using System.Text.Json;
using ChatGptBotProject.Clients;
using ChatGptBotProject.Collections.CompletionApi;
using ChatGptBotProject.Dto.CompletionApi;
using ChatGptBotProject.Factories;
using ChatGptBotProject.Services;
using Moq;

namespace ChatGptBotProject.Tests.Services;

internal class CompletionServiceTest
{
    private Mock<IHttpClient> client;
    private CompletionService completionService;

    [SetUp]
    public void SetUp()
    {
        var clientFactory = new Mock<IOpenAiCompletionClientFactory>();
        this.client = new Mock<IHttpClient>();
        clientFactory
            .Setup(c => c.CreateCompletionClient())
            .Returns(this.client.Object);

        this.completionService = new CompletionService(clientFactory.Object);
    }

    [Test]
    public void Chat_CanThrowAnHttpRequestException_WhenStatusCodeIsNotSuccessful(
        [Values(100, 300, 400, 500)] int statusCode)
    {
        // Arrange
        var httpResponse = this.CreateFakeHttpResponse(statusCode);
        this.client
            .Setup(c => c.Post(null, It.IsAny<HttpContent>()))
            .Returns(Task.FromResult(httpResponse));

        var postBody = this.CreateFakeCompletionPostBody();

        // Act & Assert
        Assert.ThrowsAsync< HttpRequestException>(async () => await this.completionService.Chat(postBody));
    }

    [Test]
    public async Task Chat_CanPassExpectedJson_WhenCallingCompletionClient()
    {
        // Arrange
        var httpResponse = this.CreateFakeHttpResponse(200);
        var taskResponse = Task.FromResult(httpResponse);
        this.client
            .Setup(c => c.Post(null, It.IsAny<HttpContent>()))
            .Returns(taskResponse);

        var postBody = this.CreateFakeCompletionPostBody();

        // Act
        var result = await this.completionService.Chat(postBody);

        // Assert
        Expression<Func<HttpContent, bool>> isExpectedPostBody;
        isExpectedPostBody = content => this.IsHttpContentExpectedPostBody(content, postBody); 
        this.client.Verify(c => c.Post(It.IsAny<string>(), It.Is(isExpectedPostBody)));
    }

    public async Task Chat_CanReturnCorrectCompletionResponse_WhenHttpResponseIsValid()
    {
        // Arrange
        var httpResponse = this.CreateFakeHttpResponse(200);
        var taskResponse = Task.FromResult(httpResponse);
        this.client
            .Setup(c => c.Post(null, It.IsAny<HttpContent>()))
            .Returns(taskResponse);

        var postBody = this.CreateFakeCompletionPostBody();

        // Act
        var result = await this.completionService.Chat(postBody);

        // Assert
        var expectedResult = this.CreateExpectedCompletionResponse();
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(expectedResult.Id));
            Assert.That(result.Object, Is.EqualTo(expectedResult.Object));
            Assert.That(result.Created, Is.EqualTo(expectedResult.Created));
            Assert.That(result.Model, Is.EqualTo(expectedResult.Model));
            Assert.That(result.Choices, Is.EqualTo(expectedResult.Choices));
            Assert.That(result.Usage, Is.EqualTo(expectedResult.Usage));
            Assert.That(result.SystemFingerprint, Is.EqualTo(expectedResult.SystemFingerprint));
        });
    }

    private CompletionResponse CreateExpectedCompletionResponse()
    {
        return new CompletionResponse
        {
            Id = "chatcmpl-8gm0zHxbQeyGyjWsHRd8hZf4oJzkw",
            Object = "chat.completion",
            Created = 1705205037,
            Model = "gpt-3.5-turbo-0613",
            Choices = new List<Choice>()
            {
                new Choice
                {
                    Index = 0,
                    LogProbs = null,
                    FinishReason = "stop",
                    Message = new Message
                    {
                        Role = "assistant",
                        Content = "Test Response"
                    }
                }

            },
            Usage = new Usage
            {
                PromptTokens = 21,
                CompletionTokens = 41,
                TotalTokens = 62
            },
            SystemFingerprint = null,
        };
    }

    private HttpResponseMessage CreateFakeHttpResponse(int statusCode)
    {
        var content = """
            {
              "id": "chatcmpl-8gm0zHxbQeyGyjWsHRd8hZf4oJzkw",
              "object": "chat.completion",
              "created": 1705205037,
              "model": "gpt-3.5-turbo-0613",
              "choices": [
                {
                  "index": 0,
                  "message": {
                    "role": "assistant",
                    "content": "Test Response"
                  },
                  "logprobs": null,
                  "finish_reason": "stop"
                }
              ],
              "usage": {
                "prompt_tokens": 21,
                "completion_tokens": 41,
                "total_tokens": 62
              },
              "system_fingerprint": null
            }
            """;

        var httpContent = new StringContent(content);

        return new HttpResponseMessage
        {
            StatusCode = (HttpStatusCode)statusCode,
            Content = httpContent
        };
    }


    private CompletionPostBody CreateFakeCompletionPostBody()
    {
        return new CompletionPostBody
        {
            Messages = new Messages
            {
                new Message { Role = "role1", Content = "Test Content" },
                new Message { Role = "role2", Content = "Test Content2" }
            },
            Model = "Unknown"
        };
    }

    private bool IsHttpContentExpectedPostBody(HttpContent httpContent, CompletionPostBody expectedPostBody)
    {
        dynamic? obj = JsonSerializer.Deserialize<ExpandoObject>(httpContent.ReadAsStream());

        // Verify messages are expected messages
        var messages = JsonSerializer.Deserialize<List<dynamic>>(obj?.messages);
        var expectedMessages = expectedPostBody.Messages;
        for (int i = 0; i < messages.Count; i++)
        {
            dynamic? message = JsonSerializer.Deserialize<ExpandoObject>(messages[i]);
            string role = JsonSerializer.Deserialize<string>(message?.role);
            string content = JsonSerializer.Deserialize<string>(message?.content);
            if (role != expectedMessages[i].Role)
            {
                return false;
            }

            if (content != expectedMessages[i].Content)
            {
                return false;
            }
        }

        // Verify model is expected model
        string model = JsonSerializer.Deserialize<string>(obj.model);
        return model == expectedPostBody.Model;
    }

}
