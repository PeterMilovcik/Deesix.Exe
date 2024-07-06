namespace Deesix.Application.UnitTests;

[TestFixture]
public class CreateRealmTests
{
    [Test]
    public void Execute_GivenValidRequest_ReturnsSuccessResultWithExpectedRealm()
    {
        // Arrange
        var request = new CreateRealm.Request
        {
            WorldId = 1,
            GeneratedRealm = new GenerateRealm.Response.GeneratedRealm
            {
                Name = "TestRealm",
                Description = "A realm for testing."
            }
        };
        var createRealm = new CreateRealm();

        // Act
        var response = createRealm.Execute(request);

        // Assert
        Assert.That(response.Realm.IsSuccess, 
            "Expected the realm creation to be successful.");
        Assert.That(request.WorldId, Is.EqualTo(response.Realm.Value.WorldId), 
            "Expected the created realm to have the same WorldId as the request.");
        Assert.That(request.GeneratedRealm.Name, Is.EqualTo(response.Realm.Value.Name), 
            "Expected the created realm to have the same Name as the request.");
        Assert.That(request.GeneratedRealm.Description, Is.EqualTo(response.Realm.Value.Description), 
            "Expected the created realm to have the same Description as the request.");
    }
}