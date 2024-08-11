using CSharpFunctionalExtensions;
using Deesix.Infrastructure.Generators;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Tests.Infrastructure.Generators.OpenAIGenerator;

[Explicit("OpenAI API call")]
public class MultipleOutputs : TestFixture
{
    public IOpenAIGenerator OpenAIGenerator { get; private set; }

    public override void SetUp()
    {
        base.SetUp();
        OpenAIGenerator = Services.GetRequiredService<IOpenAIGenerator>();
        OpenAIGenerator.Should().NotBeNull(
            because: "the OpenAI generator should be registered in the services");
    }

    [Test]
    public async Task Should_GenerateMultipleOutputs()
    {
        // Arrange
        var systemPrompt = "You are fictional character name generator.";
        var userPrompt = "Generate a fictional character name. Don't write anything else.";
        int numberOfOutputs = 10;
        // Act
        var textResults = await OpenAIGenerator
            .GenerateMultipleTextAsync(systemPrompt, userPrompt, numberOfOutputs, 1.75, 1);
        // Assert
        textResults.TapError(error => Console.WriteLine($"Error: {error}"));
        textResults.IsSuccess.Should().BeTrue(
            because: "the OpenAI generator should return a successful result.");
        textResults.Value.Should().HaveCount(numberOfOutputs,   
            because: "the OpenAI generator should return the number of outputs requested");
        foreach (var textResult in textResults.Value)
        {
            Console.WriteLine(textResult);
        }
    }
}
