using System;

namespace Emulator6502.Components.OpcodeExecuters
{
    internal class Sty : OpcodeExecuterBase
    {
        public Sty(ICpu cpu) : base(cpu)
        {
        }

        public override void Register(CpuInstruction[] opcodeHandlers)
        {
            opcodeHandlers[Mnemonic.StyAbsolute] = new CpuInstruction(Absolute, 2);
            opcodeHandlers[Mnemonic.StyZeroPage] = new CpuInstruction(ZeroPage, 3);
            opcodeHandlers[Mnemonic.StyZeroPageX] = new CpuInstruction(ZeroPageX, 4);
        }

        private void Absolute()
        {
            Int16 location = Cpu.GetNextWord();
            Cpu.Ram.WriteByte(location, Cpu.Y);
        }

        private void ZeroPage()
        {
            byte zeroPageAddress = Cpu.GetNextByte();
            Cpu.Ram.WriteByte(zeroPageAddress, Cpu.Y);
        }

        private void ZeroPageX()
        {
            Int16 location = GetZeroPageXAddress(Cpu.GetNextByte(), Cpu.X);
            Cpu.Ram.WriteByte(location, Cpu.Y);
        }
    }
}
