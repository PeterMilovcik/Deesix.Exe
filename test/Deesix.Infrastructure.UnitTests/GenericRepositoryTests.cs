using Deesix.Domain.Entities;
using Deesix.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Deesix.Infrastructure.UnitTests;

public class GenericRepositoryTests
{
    private ApplicationDbContext context;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDeesix")
            .Options;
        context = new ApplicationDbContext(options);
        context.Database.EnsureCreated();
    }
    
    [TearDown]
    public void TearDown()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }

    [Test]
    public void AddWorldTest()
    {
        // Arrange
        var repository = new GenericRepository<World>(context);
        var entity = new SomeWorld();        
        // Act
        var savedEntity = repository.Add(entity);
        // Assert
        Assert.That(savedEntity, Is.Not.Null, "Expected the saved entity to be not null.");
        Assert.That(savedEntity.Id, Is.GreaterThan(0), "Expected the saved entity to have an Id greater than 0.");
        Assert.That(savedEntity.Name, Is.EqualTo(entity.Name), "Expected the saved entity to have the same Name as the input entity.");
        Assert.That(savedEntity.Description, Is.EqualTo(entity.Description), "Expected the saved entity to have the same Description as the input entity.");
    }
}