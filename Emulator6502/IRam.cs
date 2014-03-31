using System;

namespace Emulator6502
{
    public delegate void ReadByteEventHandler(IRam sender, Int16 location, byte value);
    public delegate void WriteByteEventHandler(IRam sender, Int16 location, byte oldValue, byte newValue);
    public delegate void ReadWordEventHandler(IRam sender, Int16 location, Int16 value);
    
    public interface IRam
    {
        event ReadByteEventHandler ReadByteEvent;
        event WriteByteEventHandler WriteByteEvent;
        event ReadWordEventHandler ReadWordEvent;

        void WriteByte(Int16 location, byte value);

        byte ReadByte(Int16 location);

        Int16 ReadWord(Int16 location);
    }
}
