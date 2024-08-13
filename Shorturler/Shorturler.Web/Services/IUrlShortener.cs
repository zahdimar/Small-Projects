namespace Shorturler.Services;

public interface IUrlShortener
{
    /// <summary>
    /// Lookup or create short URL 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="ct"></param>
    /// <returns>short URL token</returns>
    Task<Guid> GetTokenOrCreateAsync(string url, CancellationToken ct);
    
    /// <summary>
    /// Lookup for full url by token
    /// </summary>
    /// <param name="token"></param>
    /// <param name="ct"></param>
    /// <returns>full url</returns>
    Task<string> GetFullUrlAsync(Guid token, CancellationToken ct);
}