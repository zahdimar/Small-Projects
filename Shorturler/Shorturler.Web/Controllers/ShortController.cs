
using Microsoft.AspNetCore.Mvc;

namespace Shorturler.Controllers;

[ApiController]
[Route("[controller]")]
public class ShortController(ShortRequestHandler requestHandler) : Controller
{
    /// <summary>
    /// Creates short url
    /// </summary>
    /// <param name="url"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<string> Create([FromBody] string url, CancellationToken ct)
    {
        return await requestHandler.GetShortUrlAsync(url, ct);
    }
    
    /// <summary>
    /// Redirect to found URL
    /// </summary>
    /// <param name="token"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("{token}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid token, CancellationToken ct)
    {
        var fullUrl = await requestHandler.GetFullUrlByTokenAsync(token, ct);

        if (string.IsNullOrEmpty(fullUrl))
        {
            return NotFound(); 
        }
        return Redirect(fullUrl);
    }
}