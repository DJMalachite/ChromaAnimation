namespace ChromaAnimation;

internal ref struct SpanWriter
{
    public Span<byte> Span { get; }
    public int Position { get; private set; }
    
    public SpanWriter(in Span<byte> span)
    {
        Span = span;
        Position = 0;
    }

    public void Write(int value)
    {
        BitConverter.TryWriteBytes(Span[Position..], value);
        Position += sizeof(int);
    }

    public void Write(byte value)
    {
        Span[Position] = value;
        Position += sizeof(byte);
    }

    public void Write(float value)
    {
        BitConverter.TryWriteBytes(Span[Position..], value);
        Position += sizeof(float);
    }
    
    public void Write(Color color)
    {
        Write(color.R);
        Write(color.G);
        Write(color.B);
        Write(color.A);
    }
}