namespace ChromaAnimation;

public class Animation
{
    private const int HeaderSize = sizeof(int) //version
                       + sizeof(byte) //dimension
                       + sizeof(byte) //type
                       + sizeof(int); //frame count
    
    private const int SupportedVersion = 1;
    public DeviceType DeviceType { get; }
    public ColorFrame[] Frames { get; }
    
    public Animation(DeviceType deviceType, ColorFrame[] frames)
    {
        DeviceType = deviceType;
        Frames = frames;
    }
    
    public int GetByteSize()
    {
        if (Frames.Length == 0)
            return HeaderSize;
        
        var frameSize = sizeof(float) + sizeof(int) * Frames[0].Colors.Length * Frames[0].Colors[0].Length;
        return HeaderSize + Frames.Length * frameSize;
    }
    
    public static Animation FromBytes(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length < HeaderSize)
            throw new InvalidDataException("The file is too small");

        var reader = new SpanReader(bytes);
        var version = reader.ReadInt32();

        if (version != SupportedVersion)
            throw new NotSupportedException($"Version {version} is not supported");
        
        var deviceDimension = reader.ReadByte();
        var tempDeviceType = reader.ReadByte();
        var deviceType = GetDeviceType(deviceDimension, tempDeviceType);
        var frameCount = reader.ReadInt32();
        var (rows, cols) = GetDeviceDimensions(deviceType);
        
        var frames = new ColorFrame[frameCount];
        for (var i = 0; i < frameCount; i++)
        {
            var duration = reader.ReadSingle();
            var colors = new Color[rows][];
            for (var j = 0; j < rows; j++)
            {
                colors[j] = new Color[cols];
                for (var k = 0; k < cols; k++)
                {
                    colors[j][k] = reader.ReadColor();
                }
            }

            frames[i] = new ColorFrame(duration, colors);
        }
        
        if (reader.Position != reader.Span.Length)
            throw new InvalidDataException("The file was not read completely");
        
        return new Animation(deviceType, frames);
    }
    
    public void WriteTo(in Span<byte> bytes)
    {
        var size = GetByteSize();

        if (bytes.Length != size)
        {
            throw new ArgumentException("The span is not the correct size. It must be the same size as GetByteSize()", nameof(bytes));
        }
        
        var writer = new SpanWriter(bytes);
        writer.Write(SupportedVersion);
        var (dimension, type) = GetDeviceType(DeviceType);
        writer.Write(dimension);
        writer.Write(type); 
        writer.Write(Frames.Length);
        
        foreach (var frame in Frames)
        {
            writer.Write(frame.Duration);
            foreach (var row in frame.Colors)
            {
                foreach (var color in row)
                {
                    writer.Write(color);
                }
            }
        }
    }

    public byte[] ToBytes()
    {
        var bytes = new byte[GetByteSize()];
        
        WriteTo(bytes);
        
        return bytes;
    }

    private static DeviceType GetDeviceType(byte dim, byte type) => (dim,  type) switch
    {
        (0,0) => DeviceType.ChromaLink,
        (0,1) => DeviceType.Headset,
        (0,2) => DeviceType.Mousepad,
        (1,0) => DeviceType.Keyboard,
        (1,1) => DeviceType.Keypad,
        (1,2) => DeviceType.Mouse,
        (1,3) => DeviceType.KeyboardExtended,
        _ => throw new InvalidOperationException()
    };
    
    public static (byte dimension, byte type) GetDeviceType(DeviceType deviceType) => deviceType switch
    {
        DeviceType.ChromaLink => (0, 0),
        DeviceType.Headset => (0, 1),
        DeviceType.Mousepad => (0, 2),
        DeviceType.Keyboard => (1, 0),
        DeviceType.Keypad => (1, 1),
        DeviceType.Mouse => (1, 2),
        DeviceType.KeyboardExtended => (1, 3),
        _ => throw new ArgumentOutOfRangeException(nameof(deviceType), deviceType, null)
    };
    
    public static (int rows, int columns) GetDeviceDimensions(DeviceType deviceType) => deviceType switch
    {
        DeviceType.Mousepad => (1, 15),
        DeviceType.Headset => (1, 5),
        DeviceType.ChromaLink => (1, 5),
        
        DeviceType.Mouse => (9, 7),
        DeviceType.Keypad => (4, 5),
        DeviceType.Keyboard => (6, 22),
        DeviceType.KeyboardExtended => (8, 24),
        _ => throw new ArgumentOutOfRangeException(nameof(deviceType), deviceType, null)
    };
}