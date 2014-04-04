using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulator6502.Components.OpcodeExecuters
{
    internal class Inc : OpcodeExecuterBase
    {
        public Inc(ICpu cpu) : base(cpu)
        {
        }

        public override void Register(CpuInstruction[] opcodeHandlers)
        {
            opcodeHandlers[Mnemonic.IncZeroPage] = new CpuInstruction(ZeroPage, 5);
            opcodeHandlers[Mnemonic.IncZeroPageX] = new CpuInstruction(ZeroPageX, 6);
            opcodeHandlers[Mnemonic.IncAbsolute] = new CpuInstruction(Absolute, 6);
            opcodeHandlers[Mnemonic.IncAbsoluteX] = new CpuInstruction(AbsoluteX, 7);
        }

        private void ZeroPage()
        {
            byte zeroPageAddress = Cpu.GetNextByte();
            byte value = Cpu.Ram.ReadByte(zeroPageAddress);
            value++;
            Cpu.Ram.WriteByte(zeroPageAddress, value);

            SetNegativeZeroFlags(value);
        }

        private void ZeroPageX()
        {
            Int16 address = GetZeroPageXAddress(Cpu.GetNextByte(), Cpu.X);
            byte value = Cpu.Ram.ReadByte(address);
            value++;
            Cpu.Ram.WriteByte(address, value);

            SetNegativeZeroFlags(value);
        }

        private void Absolute()
        {
            Int16 address = Cpu.GetNextWord();
            byte value = Cpu.Ram.ReadByte(address);
            value++;
            Cpu.Ram.WriteByte(address, value);

            SetNegativeZeroFlags(value);
        }

        private void AbsoluteX()
        {
            Int16 address = GetAbsoluteXAddress(Cpu.GetNextWord(), Cpu.X);
            byte value = Cpu.Ram.ReadByte(address);
            value++;
            Cpu.Ram.WriteByte(address, value);

            SetNegativeZeroFlags(value);
        }
    }
}
