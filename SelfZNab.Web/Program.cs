using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SelfZNab.Domain.Repositories;
using SelfZNab.Domain.Services;
using SelfZNab.Infra.Data;
using SelfZNab.Infra.Repositories;
using SelfZNab.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Creates a builder object which prepares the application to run
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder
            .Services.AddControllers()
            .AddXmlSerializerFormatters()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
            );

        // Add optional extra features to the application.
        builder.Services.AddEndpointsApiExplorer(); // helps explore API endpoints
        builder.Services.AddSwaggerGen(); // adds swagger

        builder.Services.Configure<ConnectionStringOptions>(
            builder.Configuration.GetSection("ConnectionStrings")
        );
        // Specify to the application which databse it's going to use.
        builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
            (service, options) =>
            {
                var opt = service.GetRequiredService<IOptions<ConnectionStringOptions>>();
                options.UseSqlite(opt.Value.DefaultConnection);
            }
        );

        builder.Services.AddScoped<ITorrentRepository, TorrentRepository>();
        builder.Services.AddScoped<ITorrentService, TorrentService>();

        // Build the application and get it ready to run
        var app = builder.Build();

        // Check if the application is running, if so set Swagger for API documentation
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Make sure http requests are redirected to Https for security
        app.UseHttpsRedirection();

        app.MapControllers(); // in order for swagger to work

        // Start the application and keep it running
        app.Run();
    }
}
