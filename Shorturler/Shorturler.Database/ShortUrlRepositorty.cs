using Microsoft.EntityFrameworkCore;
using Shorturler.Database.Models;

namespace Shorturler.Database; 

public class ShortUrlRepository(ShorturlerDbContext context)
{
    public async Task<Guid?> GetIdByUrl(string fullUrl)
    {
        var existed = await context.ShortUrls.FirstOrDefaultAsync(s => s.FullUrl == fullUrl);
        return existed?.Id;
    }
    
    public async Task<ShortUrl> SaveShortUrlAsync(string fullUrl)
    {
        var shortUrl = new ShortUrl
        {
            Id = Guid.NewGuid(),
            FullUrl = fullUrl
        };

        await context.ShortUrls.AddAsync(shortUrl);
        await context.SaveChangesAsync();
        return shortUrl;
    }

    public async Task<string> GetFullUrlByIdAsync(Guid id)
    {
        var shortUrl = await context.ShortUrls.FindAsync(id);
        return shortUrl?.FullUrl;
    }
} 
