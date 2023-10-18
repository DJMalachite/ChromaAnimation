using System.Text.Json.Serialization;

namespace CyberpunkChromaExtractor.JsonStructure;

public class Buffer
{
    [JsonPropertyName("BufferId")]
    public string BufferId { get; set; }

    [JsonPropertyName("Flags")]
    public int Flags { get; set; }

    [JsonPropertyName("Bytes")]
    public string Bytes { get; set; }
}