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
        var userInterface = new Mock<IUserInterface>().Object;
        saveWorld = new SaveWorld(mockRepository.Object, userInterface);
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
        mockRepository.Setup(x => x.Add(It.IsAny<World>())).Returns(world);

        // Act
        var result = await saveWorld.ExecuteAsync(world);

        // Assert
        Assert.That(result.IsSuccess, 
            $"Expected the result to be successful.");
        Assert.That(result.Value, Is.EqualTo(world), 
            $"Expected the saved world to be the same as the input world.");
    }

}