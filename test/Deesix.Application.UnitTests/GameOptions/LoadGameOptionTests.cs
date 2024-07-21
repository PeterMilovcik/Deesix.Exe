using Deesix.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Deesix.Application.GameOptions;

namespace Deesix.Application.UnitTests;

[TestFixture]
public class LoadGameOptionTests : TestFixture
{
        private LoadGameOption? loadGameOption;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            loadGameOption = Services.GetRequiredService<IEnumerable<IGameOption>>().OfType<LoadGameOption>().FirstOrDefault();
            loadGameOption.Should().NotBeNull(because: "it is registered as a service.");
        }
}
