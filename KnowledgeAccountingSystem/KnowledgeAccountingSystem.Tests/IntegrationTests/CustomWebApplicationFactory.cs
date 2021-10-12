using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using KnowledgeAccountingSystem;
using KnowledgeAccountingSystem.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace KnowledgeAccountingSystem.Tests.IntegrationTests
{
    internal class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                RemoveLibraryDbContextRegistration(services);

                var serviceProvider = GetInMemoryServiceProvider();

                services.AddDbContextPool<KnowledgeDbContext>(options =>
                {
                    options.UseInMemoryDatabase(Guid.Empty.ToString());
                    options.UseInternalServiceProvider(serviceProvider);
                });

                using (var scope = services.BuildServiceProvider().CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<KnowledgeDbContext>();

                    UnitTestHelper.SeedData(context);
                }
            });
        }

        private static ServiceProvider GetInMemoryServiceProvider()
        {
            return new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
        }

        private static void RemoveLibraryDbContextRegistration(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<KnowledgeDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
        }

    }
}
