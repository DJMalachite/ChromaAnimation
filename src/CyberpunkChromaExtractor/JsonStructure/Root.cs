using System.Text.Json.Serialization;

namespace CyberpunkChromaExtractor.JsonStructure;

public class Root
{
    [JsonPropertyName("$type")]
    public string Type { get; set; }

    [JsonPropertyName("cookingPlatform")]
    public string CookingPlatform { get; set; }

    [JsonPropertyName("setsSerialized")]
    public List<SetsSerialized> SetsSerialized { get; set; }
}