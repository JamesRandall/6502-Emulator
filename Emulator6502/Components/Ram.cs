using System;

namespace Emulator6502.Components
{
    public class Ram : IRam
    {
        public Ram(int size)
        {
            Memory = new byte[size];
        }

        public event ReadByteEventHandler ReadByteEvent;
        public event WriteByteEventHandler WriteByteEvent;
        public event ReadWordEventHandler ReadWordEvent;
        
        private byte[] Memory { get; set; }

        public void WriteByte(Int16 location, byte value)
        {
            byte oldValue = Memory[location];
            Memory[location] = value;
            if (WriteByteEvent != null) WriteByteEvent(this, location, oldValue, value);
        }

        public byte ReadByte(Int16 location)
        {
            if (ReadByteEvent != null) ReadByteEvent(this, location, Memory[location]);
            return Memory[location];
        }

        public Int16 ReadWord(Int16 location)
        {
            byte low = Memory[location];
            byte high = Memory[location + 1];
            Int16 value = (Int16) ((high << 8) | low);
            if (ReadWordEvent != null) ReadWordEvent(this, location, value);
            return value;
        }
    }
}
