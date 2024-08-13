
using Microsoft.AspNetCore.Mvc;

namespace Shorturler.Controllers;

[ApiController]
[Route("[controller]")]
public class ShortController(ShortRequestHandler requestHandler) : Controller
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<string> Create([FromBody] string url)
    {
        return await requestHandler.GetShortUrlAsync(url);
    }
    
    [HttpGet("{token}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<string> Get(Guid token)
    {
        return await requestHandler.GetFullUrlByTokenAsync(token);
    }
}