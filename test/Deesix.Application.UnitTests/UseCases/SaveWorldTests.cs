using Moq;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Application.UseCases;
using Deesix.Application.UnitTests.TestDoubles;

namespace Deesix.Application.UnitTests;

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
        mockRepository.Setup(x => x.Add(It.IsAny<World>())).Returns(world);

        // Act
        var response = await saveWorld.ExecuteAsync(request);

        // Assert
        Assert.That(response.World.IsSuccess, 
            $"Expected the result to be successful.");
        Assert.That(response.World.Value, Is.EqualTo(world), 
            $"Expected the saved world to be the same as the input world.");
    }

}