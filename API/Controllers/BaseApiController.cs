using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // ApiController attribute is responsible for validate the url parameter in Route
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}