using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Deesix.Tests
{
    public static class TestServiceCollectionExtensions
    {
        public static IServiceCollection AddInMemoryDeesixContext(
            this IServiceCollection services)
        {
            // Remove existing DbContext registration if any
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<Deesix.Infrastructure.DataAccess.ApplicationDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Register InMemory DB
            services.AddDbContext<Deesix.Infrastructure.DataAccess.ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("DeesixTestDb");
            });

            return services;
        }
    }
}
