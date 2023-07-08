using System.Text.Json.Serialization;

namespace CoreEstate.Dtos
{
    public class PropertyPhotoCollectionDto
    {
        [JsonPropertyName("propertyId")]
        public int PropertyId { get; set; }

        [JsonPropertyName("files")]
        public required IFormFileCollection Files { get; set; }
    }
}
