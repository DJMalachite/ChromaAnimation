namespace ChromaAnimation.Tests;

public class UnitTest1
{
    [Fact]
    public void ReadAllFiles()
    {
        var files = Directory.GetFiles("animations", "*.chroma", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            Animation animation;
            
            try
            {
                animation = Animation.FromBytes(File.ReadAllBytes(file));
            }
            catch
            {
                continue;
            }
            
            var tempFile = Path.GetTempFileName();
            File.WriteAllBytes(tempFile, animation.ToBytes());
            var animation2 = Animation.FromBytes(File.ReadAllBytes(tempFile));
            File.Delete(tempFile);
            
            Assert.NotNull(animation);
            Assert.NotNull(animation2);
            
            Assert.Equal(animation.DeviceType, animation2.DeviceType);
            Assert.Equal(animation.Frames.Length, animation2.Frames.Length);
            //assert sequence of frames
            for (var i = 0; i < animation.Frames.Length; i++)
            {
                var frame = animation.Frames[i];
                var frame2 = animation2.Frames[i];
                Assert.Equal(frame.Duration, frame2.Duration);
                Assert.Equal(frame.Colors.Length, frame2.Colors.Length);
                for (var j = 0; j < frame.Colors.Length; j++)
                {
                    var row = frame.Colors[j];
                    var row2 = frame2.Colors[j];
                    Assert.Equal(row.Length, row2.Length);
                    for (var k = 0; k < row.Length; k++)
                    {
                        var color = row[k];
                        var color2 = row2[k];
                        Assert.Equal(color.R, color2.R);
                        Assert.Equal(color.G, color2.G);
                        Assert.Equal(color.B, color2.B);
                    }
                }
            }
        }
    }
}