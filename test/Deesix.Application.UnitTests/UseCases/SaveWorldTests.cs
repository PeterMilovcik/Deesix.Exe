using NUnit.Framework;
using Moq;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Application.UseCases;
using Deesix.Application.UnitTests.TestDoubles;

namespace Deesix.Application.UnitTests;

[TestFixture]
public class SaveWorldTests
{
    private Mock<IRepository<World>> mockRepository;
    private SaveWorld saveWorld;

    [SetUp]
    public void Setup()
    {
        mockRepository = new Mock<IRepository<World>>();
        saveWorld = new SaveWorld(mockRepository.Object);
    }

    [Test]
    public async Task ExecuteAsync_WhenCalledWithValidRequest_ShouldReturnSavedWorld()
    {
        // Arrange
        var world = new World
        {
            Name = "Some Name",
            Description = "Some Description",
            WorldSettings = new SomeWorldSettings()
        };
        var request = new SaveWorld.Request { World = world };
        mockRepository.Setup(x => x.InsertAsync(world)).ReturnsAsync(world);

        // Act
        var response = await saveWorld.ExecuteAsync(request);

        // Assert
        Assert.That(response.World.IsSuccess, 
            "Expected the result to be successful");
        Assert.That(response.World.Value, Is.Not.Null, 
            "Expected the saved world to not be null");
        Assert.That(response.World.Value.Name, Is.EqualTo(world.Name), 
            "Expected the saved world to have the same name as the input world");
        Assert.That(response.World.Value.Description, Is.EqualTo(world.Description), 
            "Expected the saved world to have the same description as the input world");
    }

}