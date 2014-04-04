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

        protected void SetNegativeZeroFlags(byte changedValue)
        {
            byte value = (byte)(((changedValue == 0) ? StatusFlagBits.ZeroFlagBit : 0x0) | (changedValue & StatusFlagBits.NegativeFlagBit));
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
            Int16 location = GetZeroPageXAddress(Cpu.GetNextByte(), x);
            byte register = Cpu.Ram.ReadByte(location);
            SetNegativeZeroFlags(register);
            return register;
        }

        protected Int16 GetZeroPageXAddress(byte zeroPage, byte x)
        {
            Int16 location = (Int16) ((zeroPage + x) & 0xFF);
            return location;
        }

        protected byte GetRegisterAbsolute()
        {
            Int16 location = Cpu.GetNextWord();
            byte value = Cpu.Ram.ReadByte(location);
            SetNegativeZeroFlags(value);
            return value;
        }

        protected Int16 GetAbsoluteXAddress(Int16 absoluteLocation, byte x)
        {
            return (Int16) (absoluteLocation + x);
        }

        protected byte GetRegisterAbsoluteX(byte x)
        {
            Int16 location = GetAbsoluteXAddress(Cpu.GetNextWord(), x);
            byte value = Cpu.Ram.ReadByte(location);
            SetNegativeZeroFlags(value);
            return value;
        }

        protected Int16 GetIndirectXAddress(byte zeroPageAddress, byte x)
        {
            Int16 zeroPageLocation = (Int16)(zeroPageAddress + x);
            Int16 location = Cpu.Ram.ReadWord(zeroPageLocation);
            return location;
        }

        protected Int16 GetIndirectYAddress(byte zeroPageAddress, byte y)
        {
            Int16 location = (Int16)(Cpu.Ram.ReadWord(zeroPageAddress) + Cpu.Y);
            return location;
        }
    }
}
