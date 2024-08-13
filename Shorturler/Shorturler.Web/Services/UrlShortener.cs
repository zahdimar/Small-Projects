using Shorturler.Database;

namespace Shorturler.Services;

public class UrlShortener(ShortUrlRepository urlRepository) : IUrlShortener
{

    public async Task<Guid> GetTokenOrCreate(string url)
    {
        var id = await urlRepository.GetIdByUrl(url);
        if (id is not null)
        {
            return id.Value;
        }
        
        var result = await urlRepository.SaveShortUrlAsync(url);
        return result.Id;
    }

    public async Task<string> GetFullUrl(Guid token)
    {
        return await urlRepository.GetFullUrlByIdAsync(token);
    }
}