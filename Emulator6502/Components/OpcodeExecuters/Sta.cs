using System;

namespace Emulator6502.Components.OpcodeExecuters
{
    internal class Sta : OpcodeExecuterBase
    {
        public Sta(ICpu cpu) : base(cpu)
        {
        }

        public override void Register(CpuInstruction[] opcodeHandlers)
        {
            opcodeHandlers[Mnemonic.StaZeroPage] = new CpuInstruction(ZeroPage, 3);
            opcodeHandlers[Mnemonic.StaZeroPageX] = new CpuInstruction(ZeroPageX, 4);
            opcodeHandlers[Mnemonic.StaAbsolute] = new CpuInstruction(Absolute, 4);
            opcodeHandlers[Mnemonic.StaAbsoluteX] = new CpuInstruction(AbsoluteX, 5);
            opcodeHandlers[Mnemonic.StaAbsoluteY] = new CpuInstruction(AbsoluteY, 5);
            opcodeHandlers[Mnemonic.StaIndirectX] = new CpuInstruction(IndirectX, 6);
            opcodeHandlers[Mnemonic.StaIndirectY] = new CpuInstruction(IndirectY, 6);
        }

        private void ZeroPage()
        {
            Int16 location = Cpu.GetNextByte();
            Cpu.Ram.WriteByte(location, Cpu.A);
        }

        private void ZeroPageX()
        {
            Int16 location = GetZeroPageXAddress(Cpu.GetNextByte(), Cpu.X);
            Cpu.Ram.WriteByte(location, Cpu.A);
        }

        private void Absolute()
        {
            Int16 location = Cpu.GetNextWord();
            Cpu.Ram.WriteByte(location, Cpu.A);
        }

        private void AbsoluteX()
        {
            Int16 location = GetAbsoluteXAddress(Cpu.GetNextWord(), Cpu.X);
            Cpu.Ram.WriteByte(location, Cpu.A);
        }

        private void AbsoluteY()
        {
            Int16 location = GetAbsoluteXAddress(Cpu.GetNextWord(), Cpu.Y);
            Cpu.Ram.WriteByte(location, Cpu.A);
        }

        private void IndirectX()
        {
            Int16 location = GetIndirectXAddress(Cpu.GetNextByte(), Cpu.X);
            Cpu.Ram.WriteByte(location, Cpu.A);
        }

        private void IndirectY()
        {
            Int16 location = GetIndirectYAddress(Cpu.GetNextByte(), Cpu.Y);
            Cpu.Ram.WriteByte(location, Cpu.A);
        }
    }
}
