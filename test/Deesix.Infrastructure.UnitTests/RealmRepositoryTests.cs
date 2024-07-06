using Deesix.Domain.Entities;
using Deesix.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Deesix.Infrastructure.UnitTests;

public class RealmRepositoryTests
{
    private ApplicationDbContext context;
    private GenericRepository<Realm> repository;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDeesixRealm")
            .Options;
        context = new ApplicationDbContext(options);
        context.Database.EnsureCreated();
        repository = new GenericRepository<Realm>(context);
    }

    [TearDown]
    public void TearDown()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }

    [Test]
    public void AddRealmTest()
    {
        // Arrange
        var createdRealm = new Realm
        {
            WorldId = 1,
            Name = "Test Realm",
            Description = "A test realm",
        };

        // Act
        var savedRealm = repository.Add(createdRealm);

        // Assert
        Assert.That(savedRealm, Is.Not.Null, 
            "Expected the saved realm to be not null.");
        Assert.That(savedRealm.Id, Is.GreaterThan(0), 
            "Expected the saved realm to have an Id greater than 0.");
        Assert.That(createdRealm.Name, Is.EqualTo(savedRealm.Name), 
            "Expected the saved realm to have the same Name as the input realm.");
        Assert.That(createdRealm.Description, Is.EqualTo(savedRealm.Description), 
            "Expected the saved realm to have the same Description as the input realm.");
    }
}