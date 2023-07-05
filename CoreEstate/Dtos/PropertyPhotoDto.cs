using System.Text.Json.Serialization;

namespace CoreEstate.Dtos
{
    public class PropertyPhotoDto
    {
        [JsonPropertyName("propertyId")]
        public int PropertyId { get; set; }

        [JsonPropertyName("file")]
        public required IFormFile File { get; set; }
    }
}
