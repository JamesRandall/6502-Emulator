using System;

namespace Emulator6502.Components.OpcodeExecuters
{
    internal class Stx : OpcodeExecuterBase
    {
        public Stx(ICpu cpu) : base(cpu)
        {
        }

        public override void Register(CpuInstruction[] opcodeHandlers)
        {
            opcodeHandlers[Mnemonic.StxAbsolute] = new CpuInstruction(Absolute, 2);
            opcodeHandlers[Mnemonic.StxZeroPage] = new CpuInstruction(ZeroPage, 3);
            opcodeHandlers[Mnemonic.StxZeroPageY] = new CpuInstruction(ZeroPageY, 4);
        }

        private void Absolute()
        {
            Int16 location = Cpu.GetNextWord();
            Cpu.Ram.WriteByte(location, Cpu.X);
        }

        private void ZeroPage()
        {
            byte zeroPageAddress = Cpu.GetNextByte();
            Cpu.Ram.WriteByte(zeroPageAddress, Cpu.X);
        }

        private void ZeroPageY()
        {
            Int16 location = GetZeroPageXAddress(Cpu.GetNextByte(), Cpu.Y);
            Cpu.Ram.WriteByte(location, Cpu.X);
        }
    }
}
