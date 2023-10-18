namespace ChromaAnimation;

public readonly record struct Color(byte R, byte G, byte B, byte A)
{
    public byte R { get; } = R;
    public byte G { get; } = G;
    public byte B { get; } = B;
    public byte A { get; } = A;
}