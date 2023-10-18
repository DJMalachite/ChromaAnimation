using System.Text.Json;
using CyberpunkChromaExtractor.JsonStructure;

namespace CyberpunkChromaExtractor;

public static class Program
{
    public static async Task Main(string[] args)
    {
        //root.json is exported from the game using wolvenKit. It contains all the chroma animations encoded in base64.
        var root = JsonSerializer.Deserialize<Root>(await File.ReadAllTextAsync("root.json"));

        foreach (var set in root!.SetsSerialized)
        {
            var setName = set.Name.Value;
            foreach (var animation in set.Animations)
            {
                var animationFileName = animation.Name.Value.Split('/').Last();
                var bytes = Convert.FromBase64String(animation.Buffer.Bytes);
                var animationFile = Path.Combine("animations", setName, animationFileName);
                Directory.CreateDirectory(Path.GetDirectoryName(animationFile)!);
                await File.WriteAllBytesAsync(animationFile, bytes);
            }
        }
    }
}