using System.Text.Json.Serialization;

namespace CyberpunkChromaExtractor.JsonStructure;

public class Animation
{
    [JsonPropertyName("$type")]
    public string Type { get; set; }

    [JsonPropertyName("buffer")]
    public Buffer Buffer { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public Name Name { get; set; }
}