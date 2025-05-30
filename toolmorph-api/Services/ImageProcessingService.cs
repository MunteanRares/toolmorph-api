using toolmorph_api.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.HttpResults;

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

        public async Task<ObjectDetectionResponse> ObjectDetection(IFormFile file)
        {
            var formData = new MultipartFormDataContent();
            var fileStream = file.OpenReadStream();
            var fileContent = new StreamContent(fileStream);

            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
            formData.Add(fileContent, "file", file.FileName);

            var response = await _httpClient.PostAsync($"{_baseUrl}/object-detection", formData);

            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to detect objects: {response.StatusCode}");
                return new ObjectDetectionResponse { Predictions = [] };
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var objectDetectionResponse = JsonConvert.DeserializeObject<ObjectDetectionResponse>(jsonString);

            return objectDetectionResponse ?? new ObjectDetectionResponse { Predictions = [] };
        }

        public async Task<RemovedBackgroundResponse> RemoveBackground(IFormFile file)
        {
            var formData = new MultipartFormDataContent();
            var fileStream = file.OpenReadStream();
            var fileContent = new StreamContent(fileStream);

            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
            formData.Add(fileContent, "file", file.FileName);

            var response = await _httpClient.PostAsync($"{_baseUrl}/background-remover", formData);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to remove background: {response.StatusCode}");
            }

            byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

            var result = new RemovedBackgroundResponse
            {
                FileBytes = imageBytes,
                FileName = "removed_background.png",
                ContentType = "image/png"
            };

            return result;
        }
    }
}
