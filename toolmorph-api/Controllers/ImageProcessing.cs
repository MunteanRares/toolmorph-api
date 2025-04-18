using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace toolmorph_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageProcessing : ControllerBase
    {
        [HttpPost("extract-palette")]
        public IEnumerable<string> extractPalette([FromForm] IFormFile file)
        {
            // Actual call to python api service.
            return new List<string> { "Color1", "Color2", "Color3" };
        }
    }
}
