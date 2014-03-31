using System;

namespace Emulator6502.Components
{
    public class Ram : IRam
    {
        public Ram(int size)
        {
            Memory = new byte[size];
        }

        public event ReadMemoryEventHandler ReadMemoryEvent;

        public event WriteMemoryEventHandler WriteMemoryEvent;

        private byte[] Memory { get; set; }

        public void WriteByte(Int16 location, byte value)
        {
            byte oldValue = Memory[location];
            Memory[location] = value;
            if (WriteMemoryEvent != null) WriteMemoryEvent(this, location, oldValue, value);
        }

        public byte ReadByte(Int16 location)
        {
            if (ReadMemoryEvent != null) ReadMemoryEvent(this, location, Memory[location]);
            return Memory[location];
        }
    }
}
