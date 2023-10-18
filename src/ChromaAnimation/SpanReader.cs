namespace ChromaAnimation;

internal ref struct SpanReader
{
    public ReadOnlySpan<byte> Span { get; }
    public int Position { get; private set; }

    public SpanReader(in ReadOnlySpan<byte> span)
    {
        Span = span;
        Position = 0;
    }
    
    public int ReadInt32()
    {
        var value = BitConverter.ToInt32(Span[Position..]);
        Position += sizeof(int);
        return value;
    }

    public byte ReadByte()
    {
        var value = Span[Position];
        Position += sizeof(byte);
        return value;
    }

    public float ReadSingle()
    {
        var value = BitConverter.ToSingle(Span[Position..]);
        Position += sizeof(float);
        return value;
    }
    
    public Color ReadColor()
    {
        return new Color(ReadByte(), ReadByte(), ReadByte(), ReadByte());
    }
}