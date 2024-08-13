using Shorturler.Services;

namespace Shorturler;

public class ShortRequestHandler(IUrlShortener urlShortener)
{
    //todo: load from settings
    private const string Host = "http://localhost:5203/short/";

    /// <summary>
    /// Lookup or create short URL 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="ct"></param>
    /// <returns>Short url</returns>
    public async Task<string> GetShortUrlAsync(string url, CancellationToken ct)
    {
        var id = await urlShortener.GetTokenOrCreateAsync(url, ct);
        return $"{Host}{id}";
    }
    
    /// <summary>
    /// Lookup for full url by token
    /// </summary>
    /// <param name="token"></param>
    /// <param name="ct"></param>
    /// <returns>full url</returns>
    public async Task<string> GetFullUrlByTokenAsync(Guid token, CancellationToken ct)
    {
        return await urlShortener.GetFullUrlAsync(token, ct);
    }
}