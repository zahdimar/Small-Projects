namespace Shorturler.Database;

using Microsoft.EntityFrameworkCore;
using Models;

public class ShorturlerDbContext(DbContextOptions<ShorturlerDbContext> options) : DbContext(options)
{
    public DbSet<ShortUrl> ShortUrls { get; set; }
}
