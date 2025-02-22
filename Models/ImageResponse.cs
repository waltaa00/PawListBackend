namespace PawListBackend.Models
{
    public class ImageResponse
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public List<Breed> Breeds { get; set; }
    }

    public class Breed
    {
        public string Name { get; set; }
        public string Temperament { get; set; }
        // Add other fields as needed from the API response
    }
}