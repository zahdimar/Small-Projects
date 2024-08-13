using Shorturler.Database;

namespace Shorturler.Services;

public class UrlShortener(ShortUrlRepository urlRepository) : IUrlShortener
{

    public async Task<Guid> GetTokenOrCreateAsync(string url, CancellationToken ct)
    {
        var id = await urlRepository.GetIdByUrl(url, ct);
        if (id is not null)
        {
            return id.Value;
        }
        var result = await urlRepository.SaveShortUrlAsync(url, ct);
        return result.Id;
    }

    public async Task<string> GetFullUrlAsync(Guid token, CancellationToken ct)
    {
        return await urlRepository.GetFullUrlByIdAsync(token, ct);
    }
}