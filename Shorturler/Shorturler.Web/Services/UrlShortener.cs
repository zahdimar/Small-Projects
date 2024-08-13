namespace Shorturler.Services;

public class UrlShortener: IUrlShortener
{
    public async Task<Guid> GetTokenOrCreate(string url)
    {
        return await Task.FromResult(Guid.NewGuid());
    }

    public async Task<string> GetFullUrl(Guid token)
    {
        return await Task.FromResult(token.ToString());
    }
}