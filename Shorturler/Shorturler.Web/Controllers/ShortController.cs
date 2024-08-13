
using Microsoft.AspNetCore.Mvc;

namespace Shorturler.Controllers;

[ApiController]
[Route("[controller]")]
public class ShortController : Controller
{
    private const string Host = "http://localhost:5203/short/";
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<string> Create([FromBody] string url)
    {
        return $"{Host}{Guid.NewGuid()}";
    }
    
    [HttpGet("{url}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<string> Get(string url)
    {
        return url;
    }
}