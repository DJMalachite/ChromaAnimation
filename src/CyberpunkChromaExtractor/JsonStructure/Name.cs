using System.Text.Json.Serialization;

namespace CyberpunkChromaExtractor.JsonStructure;

public class Name
{
    [JsonPropertyName("$type")]
    public string Type { get; set; }

    [JsonPropertyName("$storage")]
    public string Storage { get; set; }

    [JsonPropertyName("$value")]
    public string Value { get; set; }
}