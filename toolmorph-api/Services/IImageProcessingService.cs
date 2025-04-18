
using toolmorph_api.Models;

namespace toolmorph_api.Services
{
    public interface IImageProcessingService
    {
        Task<PaletteResponse> ExtractPalette(IFormFile image);
    }
}