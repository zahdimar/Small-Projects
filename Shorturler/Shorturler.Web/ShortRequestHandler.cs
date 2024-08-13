using Shorturler.Services;

namespace Shorturler;

public class ShortRequestHandler(IUrlShortener urlShortener)
{
    private const string Host = "http://localhost:5203/short/";

    public async Task<string> GetShortUrlAsync(string url)
    {
        var id = await urlShortener.GetTokenOrCreate(url);
        return $"{Host}{id}";
    }
    
    public async Task<string> GetFullUrlByTokenAsync(Guid token)
    {
        return await urlShortener.GetFullUrl(token);
    }
}