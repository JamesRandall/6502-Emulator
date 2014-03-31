using System;

namespace Emulator6502
{
    public delegate void ReadMemoryEventHandler(IRam sender, Int16 location, byte value);

    public delegate void WriteMemoryEventHandler(IRam sender, Int16 location, byte oldValue, byte newValue);

    public interface IRam
    {
        event ReadMemoryEventHandler ReadMemoryEvent;
        event WriteMemoryEventHandler WriteMemoryEvent;

        void WriteByte(Int16 location, byte value);

        byte ReadByte(Int16 location);
    }
}
