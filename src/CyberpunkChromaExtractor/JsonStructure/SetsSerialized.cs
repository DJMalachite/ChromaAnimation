using System.Text.Json.Serialization;

namespace CyberpunkChromaExtractor.JsonStructure;

public class SetsSerialized
{
    [JsonPropertyName("$type")]
    public string Type { get; set; }

    [JsonPropertyName("animations")]
    public List<Animation> Animations { get; set; }

    [JsonPropertyName("name")]
    public Name Name { get; set; }
}