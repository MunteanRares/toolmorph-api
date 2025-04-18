using toolmorph_api.Models;
using Newtonsoft.Json;

namespace toolmorph_api.Services
{
    public class ImageProcessingService : IImageProcessingService
    {
        private readonly string _baseUrl = "http://127.0.0.1:8000";
        private readonly HttpClient _httpClient;

        public ImageProcessingService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<PaletteResponse> ExtractPalette(IFormFile image)
        {
            var formData = new MultipartFormDataContent();
            var fileStream = image.OpenReadStream();
            var fileContent = new StreamContent(fileStream);

            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(image.ContentType);
            formData.Add(fileContent, "file", image.FileName);

            var response = await _httpClient.PostAsync($"{_baseUrl}/extract-palette", formData);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get palette: {response.StatusCode}");
                return new PaletteResponse { Palettes = ["ERROR"] };
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var paletteResponse = JsonConvert.DeserializeObject<PaletteResponse>(jsonString);

            return paletteResponse ?? new PaletteResponse { Palettes = ["ERROR"] };
        }

    }
}
