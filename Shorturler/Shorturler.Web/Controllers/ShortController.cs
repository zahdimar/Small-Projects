
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid token)
    {
        var fullUrl = await requestHandler.GetFullUrlByTokenAsync(token);

        if (string.IsNullOrEmpty(fullUrl))
        {
            return NotFound(); // Return 404 if the URL is not found
        }

        // Redirect to the full URL
        return Redirect(fullUrl);
    }
}