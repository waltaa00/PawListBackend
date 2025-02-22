using PawListBackend.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PawListBackend.Services
{
    public class DogApiService
    {
        private readonly HttpClient _httpClient;

        public DogApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetch the dog data from The Dog API
        public async Task FetchDogsFromApiAsync()
        {
            try
            {
                // Fetch data from The Dog API
                var response = await _httpClient.GetStringAsync("https://api.thedogapi.com/v1/breeds");
                var dogs = JsonConvert.DeserializeObject<List<Dog>>(response);

                if (dogs != null)
                {
                    foreach (var dog in dogs)
                    {
                        // Map the attributes as required and ensure no null values
                        if (string.IsNullOrEmpty(dog.Breed))
                        {
                            dog.Breed = "Unknown Breed"; // Provide a default value if Breed is missing
                        }

                        // Fetch and set image URL using the ReferenceImageId
                        if (!string.IsNullOrEmpty(dog.ReferenceImageId))
                        {
                            dog.ImageUrl = await GetImageUrlAsync(dog.ReferenceImageId);
                        }

                        // Set defaults for other fields if necessary
                        dog.BredFor = string.IsNullOrEmpty(dog.BredFor) ? "No breed information" : dog.BredFor;
                        dog.Temperament = string.IsNullOrEmpty(dog.Temperament) ? "No temperament info available" : dog.Temperament;
                        dog.Lifespan = string.IsNullOrEmpty(dog.Lifespan) ? "Lifespan not provided" : dog.Lifespan;
                        dog.CountryOfOrigin = string.IsNullOrEmpty(dog.CountryOfOrigin) ? "No country information" : dog.CountryOfOrigin;

                        // Here, you'd likely call a repository method to save the dog to your database.
                        // Since this is just fetching data, this part might not be necessary in DogApiService.
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching dogs: {ex.Message}");
            }
        }

        // Fetch the image URL using reference_image_id
        public async Task<string> GetImageUrlAsync(string referenceImageId)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"https://api.thedogapi.com/v1/images/{referenceImageId}");

                // Deserialize the JSON response into an object
                var imageResponse = JsonConvert.DeserializeObject<ImageResponse>(response);

                // Return the URL from the response, or a default image URL if the URL is not available
                return imageResponse?.Url ?? "default-image-url.jpg";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching image: {ex.Message}");
                return "default-image-url.jpg";
            }
        }
    }
}
