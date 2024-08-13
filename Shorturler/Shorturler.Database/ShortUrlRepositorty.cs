using Microsoft.EntityFrameworkCore;
using Shorturler.Database.Models;

namespace Shorturler.Database; 

public class ShortUrlRepository(ShorturlerDbContext context)
{
    public async Task<Guid?> GetIdByUrl(string fullUrl, CancellationToken ct)
    {
        var existed = await context.ShortUrls.FirstOrDefaultAsync(s => s.FullUrl == fullUrl, ct);
        return existed?.Id;
    }
    
    public async Task<ShortUrl> SaveShortUrlAsync(string fullUrl, CancellationToken ct)
    {
        var shortUrl = new ShortUrl
        {
            Id = Guid.NewGuid(),
            FullUrl = fullUrl
        };

        await context.ShortUrls.AddAsync(shortUrl, ct);
        await context.SaveChangesAsync(ct);
        return shortUrl;
    }

    public async Task<string> GetFullUrlByIdAsync(Guid id, CancellationToken ct)
    {
        var shortUrl = await context.ShortUrls.FindAsync([id], ct);
        return shortUrl?.FullUrl;
    }
} 
