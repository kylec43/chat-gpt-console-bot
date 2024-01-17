using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatGptBotProject.Dto.CompletionApi;

namespace ChatGptBotProject.IntegrationTests.TestHelpers;

internal static class CompletionResponseAsserter
{
    public static void AssertResponse(CompletionResponse response)
    {
        Assert.Multiple(() =>
        {
            Assert.IsNotNull(response.Id);
            Assert.IsNotNull(response.Created);
            Assert.IsNotNull(response.Object);
            Assert.IsNotNull(response.Model);
            AssertChoices(response.Choices);
            AssertUsage(response.Usage);
        });
    }

    private static void AssertUsage(Usage? usage)
    {
        Assert.IsNotNull(usage);
        if (usage is null)
        {
            return;
        }

        Assert.IsNotNull(usage?.CompletionTokens);
        Assert.IsNotNull(usage?.TotalTokens);
        Assert.IsNotNull(usage?.PromptTokens);
    }

    private static void AssertChoices(List<Choice>? choices)
    {
        Assert.IsNotNull(choices);
        if (choices is null)
        {
            return;
        }

        Assert.That(choices.Count, Is.GreaterThan(0));
        foreach (var choice in choices)
        {
            AssertChoice(choice);
        }
    }

    private static void AssertChoice(Choice choice)
    {
        Assert.IsNotNull(choice.Index);
        Assert.IsNotNull(choice.FinishReason);
        Assert.IsNotNull(choice.Message);
        if (choice.Message is Message message)
        {
            Assert.IsNotNull(message.Role);
            Assert.IsNotNull(message.Content);
        }

        AssertLogProbs(choice.LogProbs);
    }

    private static void AssertLogProbs(LogProbs? logProbs)
    {
        if (logProbs is null)
        {
            return;
        }

        AssertTokens(logProbs?.Tokens);
    }

    private static void AssertTokens(List<Token>? tokens)
    {
        if (tokens is null)
        {
            return;
        }

        foreach (var token in tokens)
        {
            Assert.IsNotNull(token.TokenValue);
            Assert.IsNotNull(token.Bytes);
            Assert.IsNotNull(token.Logprob);
            AssertTokens(token.TopLogprobs);
        }
    }
}
