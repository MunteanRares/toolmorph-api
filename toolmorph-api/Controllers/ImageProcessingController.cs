using Microsoft.AspNetCore.Mvc;
using toolmorph_api.Models;
using toolmorph_api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace toolmorph_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageProcessingController : ControllerBase
    {
        private readonly IImageProcessingService _imgProcessingService;
        public ImageProcessingController(IImageProcessingService imgProcessingService)
        {
            _imgProcessingService = imgProcessingService;
        }

        [HttpPost("extract-palette")]
        public async Task<PaletteResponse> ExtractPalette(IFormFile file)
        {
            return await _imgProcessingService.ExtractPalette(file);
        }

        [HttpPost("background-remover")]
        public async Task<IActionResult> RemoveBackground(IFormFile file)
        {
            var result = await _imgProcessingService.RemoveBackground(file);

            return File(result.FileBytes, result.ContentType, result.FileName);
        }
    }
}
