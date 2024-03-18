using Asreyion.Core.Areas.Account.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Asreyion.Core.Areas.Account.Data;

public class AuthenticationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    private readonly IConfiguration configuration;

    public AuthenticationDbContext()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.core.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.core.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables();

        this.configuration = builder.Build();

        string connectionString = this.configuration["Database:AuthenticationConnection"] ?? "Data Source=Data/Authentication.db";

        if (connectionString.Contains("Filename=") || connectionString.Contains("Data Source="))
        {
            string? directory = Path.GetDirectoryName(connectionString.Replace("Filename=", "").Replace("Data Source=", ""));

            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
            {
                _ = Directory.CreateDirectory(path: directory);
            }
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = this.configuration["Database:AuthenticationConnection"] ?? "Data Source=Data/Authentication.db";
            string dbtype = this.configuration["Database:AuthenticationDatabaseType"] ?? "Sqlite";

            _ = dbtype switch
            {
                "Sqlite" => optionsBuilder.UseSqlite(connectionString),
                "SqlServer" => optionsBuilder.UseSqlServer(connectionString),
                _ => throw new NotSupportedException($"Database type '{dbtype}' is not supported."),
            };

            _ = optionsBuilder.UseSqlite(connectionString);
        }
    }
}
