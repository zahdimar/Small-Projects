namespace Shorturler;

public class ShortRequestHandler
{
    private const string Host = "http://localhost:5203/short/";

    public Task<string> GetShortUrl(string url)
    {
        var id = Guid.NewGuid();
        return Task.FromResult($"{Host}{id}");
    }
    
    public Task<string> GetFullUrlByToken(Guid token)
    {
        return Task.FromResult(token.ToString());
    }
}