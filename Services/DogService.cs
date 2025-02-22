using PawListBackend.Repositories;
using PawListBackend.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace PawListBackend.Services
{
    public class DogService
    {
        private readonly DogRepository _repository;
        private readonly HttpClient _httpClient;

        public DogService(DogRepository repository, HttpClient httpClient)
        {
            _repository = repository;
            _httpClient = httpClient;
        }

        // Retrieves all Dog records from the database
        public async Task<List<Dog>> GetAllDogsAsync() => await _repository.GetAllDogsAsync();

        // Retrieves a specific Dog record by its Id
        public async Task<Dog?> GetDogByIdAsync(int id) => await _repository.GetDogByIdAsync(id);

        // Adds a new Dog record to the database
        public async Task AddDogAsync(Dog dog) => await _repository.AddDogAsync(dog);

        // Updates an existing Dog record in the database
        public async Task UpdateDogAsync(Dog dog) => await _repository.UpdateDogAsync(dog);

        // Deletes a Dog record from the database by its Id
        public async Task DeleteDogAsync(int id) => await _repository.DeleteDogAsync(id);

        // Fetches dogs from The Dog API and stores them in the database
        public async Task FetchAndAddDogsFromApiAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://api.thedogapi.com/v1/breeds");
                Console.WriteLine("API Response: " + response); // Log the raw response
                var dogs = JsonConvert.DeserializeObject<List<Dog>>(response);

                if (dogs != null)
                {
                    foreach (var dog in dogs)
                    {
                        // Fetch and set image URL if ReferenceImageId is available
                        if (!string.IsNullOrEmpty(dog.ReferenceImageId))
                        {
                            dog.ImageUrl = await GetImageUrlAsync(dog.ReferenceImageId);
                        }

                        // Set defaults if necessary
                        dog.BredFor = string.IsNullOrEmpty(dog.BredFor) ? "No breed information" : dog.BredFor;
                        dog.Temperament = string.IsNullOrEmpty(dog.Temperament) ? "No temperament info available" : dog.Temperament;
                        dog.Lifespan = string.IsNullOrEmpty(dog.Lifespan) ? "Lifespan not provided" : dog.Lifespan;
                        dog.CountryOfOrigin = string.IsNullOrEmpty(dog.CountryOfOrigin) ? "No country information" : dog.CountryOfOrigin;

                        // Check if the dog exists, otherwise add it to the database
                        // Ensure the Id exists, and that it is not null or default
                        var dogExists = await _repository.ExistsAsync(dog.Id);
                        if (dogExists)
                        {
                            Console.WriteLine($"Dog with ID {dog.Id} already exists.");
                        }
                        else
                        {
                            await _repository.AddDogAsync(dog);
                        }

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
                var imageResponse = JsonConvert.DeserializeObject<ImageResponse>(response);
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
