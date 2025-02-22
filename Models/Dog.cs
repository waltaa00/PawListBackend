
using Newtonsoft.Json;

namespace PawListBackend.Models
{
    // Dog model representing a dog entity in the database.
    public class Dog
    {
        public int Id { get; set; }

        // Map 'name' from JSON to 'Breed' in C#
        [JsonProperty("name")]
        public string Breed { get; set; } = string.Empty;

        [JsonProperty("url")]
        public string ImageUrl { get; set; } = string.Empty;
        
        [JsonProperty("bred_for")] // Maps the JSON property "life_span" to C# "Lifespan"
        public string BredFor { get; set; } = string.Empty;
        
        [JsonProperty("reference_image_id")] // Maps the JSON property "reference_image_id" to C# "ReferenceImageId"
        public string ReferenceImageId { get; set; } = string.Empty;
        public string Temperament { get; set; } = string.Empty;
        
        [JsonProperty("life_span")] // Maps the JSON property "life_span" to C# "Lifespan"
        public string Lifespan { get; set; } = string.Empty;
        
        [JsonProperty("origin")] // Maps the JSON property "life_span" to C# "Lifespan"
        public string CountryOfOrigin { get; set; } = string.Empty;
    }
   
}

