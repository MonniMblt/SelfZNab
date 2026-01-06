using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SelfZNab.Web;

namespace SelfZNab.Integration.Tests;

public class DotnetApiWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;

    public DotnetApiWebApplicationFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.Configure<ConnectionStringOptions>(option =>
                option.DefaultConnection = _connectionString
            );
        });
    }
}
