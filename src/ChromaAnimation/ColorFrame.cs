namespace ChromaAnimation;

public class ColorFrame
{
    public float Duration { get; }
    public Color[][] Colors { get; }
    
    public ColorFrame(float duration, Color[][] colors)
    {
        Duration = duration;
        Colors = colors;
    }
}