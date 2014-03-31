using System;

namespace Emulator6502
{
    public class UnsupportedOpcodeException : Exception
    {
        public UnsupportedOpcodeException(byte opcode) : base(String.Format("Unsupported opcode {0}", opcode))
        {
            
        }
    }
}
