using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Shorturler.Database;

public class DbContextFactory
{
    public static void RegisterDbContext(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ShorturlerDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<ShortUrlRepository>();
    }
}