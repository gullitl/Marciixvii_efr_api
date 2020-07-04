using Marciixvii.EFR.Api;
using Marciixvii.EFR.App.Contracts;
using Marciixvii.EFR.App.DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;

namespace XUnitTestProject1 {
    public abstract class IntegrationTest {
        protected readonly HttpClient TestClient;
        protected readonly ICryptography DesCryptography;
        protected IntegrationTest() {
            WebApplicationFactory<Startup> appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => {
                    builder.ConfigureServices(services => {
                        services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
                        services.AddDbContext<AppDbContext>(Options => {
                            Options.UseInMemoryDatabase(databaseName: "TestDb");
                        });
                    });
                });
            IServiceScope scope = appFactory.Services.CreateScope();
            TestClient = appFactory.CreateClient();
            DesCryptography = scope.ServiceProvider.GetRequiredService<ICryptography>();
        }
    }
}
