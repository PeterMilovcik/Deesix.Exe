using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Tests.TestDoubles;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Tests.Infrastructure.Generators.WorldGeneratorTests;

[Explicit("OpenAI API call")]
public class GenerateWorldDescriptions : TestFixture
{
    public IGenerator Generator { get; private set; }

    public override void SetUp()
    {
        base.SetUp();
        Generator = Services.GetRequiredService<IGenerator>();
        Generator.Should().NotBeNull(
            because: "the generator should be registered in the services");
    }

    [Test]
    public async Task Should_GenerateMultipleDescriptions()
    {
        // Arrange
        var world = new World
        {
            Genre = "High Fantasy",
            WorldSettings = new SomeWorldSettings()
        };
        int numberOfOutputs = 5;
        // Act
        var result = await Generator.World.GenerateWorldDescriptionsAsync(world, numberOfOutputs);
        // Assert
        result.TapError(error => Console.WriteLine($"Error: {error}"));
        result.IsSuccess.Should().BeTrue(
            because: "the generator should return a successful result.");
        result.Value.Should().HaveCount(numberOfOutputs,   
            because: "the generator should return the number of outputs requested");
        foreach (var worldDescription in result.Value)
        {
            Console.WriteLine(worldDescription);
        }
    }
}
