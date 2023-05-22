using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiLab.Dal;

namespace WebApiLab.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public JsonSerializerOptions SerializerOptions { get; }
        public CustomWebApplicationFactory()
        {
            JsonSerializerOptions jso = new(JsonSerializerDefaults.Web);
            jso.Converters.Add(new JsonStringEnumConverter());
            SerializerOptions = jso;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("Development");
            builder.ConfigureServices(services =>
            {
                services.AddScoped(sp => new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NEPTUNTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                    .UseApplicationServiceProvider(sp)
                    .Options);
            });
            var host = base.CreateHost(builder);
            using var scope = host.Services.CreateScope();
            scope.ServiceProvider.GetRequiredService<AppDbContext>()
                .Database.EnsureCreated();
            return host;
        }
    }
}
