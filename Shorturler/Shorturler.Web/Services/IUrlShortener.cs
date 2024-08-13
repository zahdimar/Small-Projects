namespace Shorturler.Services;

public interface IUrlShortener
{
    Task<Guid> GetTokenOrCreate(string url);
    Task<string> GetFullUrl(Guid token);
}