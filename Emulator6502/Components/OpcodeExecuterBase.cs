using System;

namespace Emulator6502.Components
{
    internal abstract class OpcodeExecuterBase
    {
        private readonly ICpu _cpu;

        protected OpcodeExecuterBase(ICpu cpu)
        {
            _cpu = cpu;
        }

        public ICpu Cpu { get { return _cpu; } }

        public abstract void Register(CpuInstruction[] opcodeHandlers);

        protected void SetNegativeZeroFlags(byte register)
        {
            byte value = (byte)(((register == 0) ? StatusFlagBits.ZeroFlagBit : 0x0) | (register & StatusFlagBits.NegativeFlagBit));
            Cpu.Status = (byte)((Cpu.Status & StatusFlagBits.ZeroNegativeMask) | value);
        }

        protected byte GetRegisterZeroPage()
        {
            byte location = Cpu.GetNextByte();
            byte register = Cpu.Ram.ReadByte(location);
            SetNegativeZeroFlags(register);
            return register;
        }

        protected byte GetRegisterZeroPageX(byte x)
        {
            Int16 location = (Int16)(Cpu.GetNextByte() + x);
            if (location > 0xFF) location -= 0xFF;
            byte register = Cpu.Ram.ReadByte(location);
            SetNegativeZeroFlags(register);
            return register;
        }

        protected byte GetRegisterAbsolute()
        {
            Int16 location = Cpu.GetNextWord();
            byte value = Cpu.Ram.ReadByte(location);
            SetNegativeZeroFlags(value);
            return value;
        }

        protected byte GetRegisterAbsoluteX(byte x)
        {
            Int16 location = Cpu.GetNextWord();
            location += x;
            byte value = Cpu.Ram.ReadByte(location);
            SetNegativeZeroFlags(value);
            return value;
        }
    }
}
