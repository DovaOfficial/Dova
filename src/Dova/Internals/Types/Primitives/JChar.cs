using System.Runtime.InteropServices;

namespace Dova.Internals.Types.Primitives;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct JChar
{
    [FieldOffset(0)] 
    private readonly char value;
    
    [FieldOffset(0)] 
    private readonly ushort us;

    private JChar(char c)
    {
        this.value = c;
        this.us = c;
    }

    private JChar(ushort us)
    {
        this.value = (char)us;
        this.us = us;
    }

    public override string ToString() => us.ToString();

    public static implicit operator JChar(char value) => new (value);
    
    public static implicit operator JChar(ushort value) => new (value);

    public static implicit operator char(JChar value) => value.value;
    
    public static implicit operator ushort(JChar value) => value.us;
}