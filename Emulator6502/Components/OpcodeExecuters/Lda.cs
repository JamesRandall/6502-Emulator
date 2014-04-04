using System;

namespace Emulator6502.Components.OpcodeExecuters
{
    internal class Lda : OpcodeExecuterBase
    {
        public Lda(ICpu cpu) : base(cpu)
        {
           
        }

        public override void Register(CpuInstruction[] opcodeHandlers)
        {
            opcodeHandlers[Mnemonic.LdaImmediate] = new CpuInstruction(Immediate, 2);
            opcodeHandlers[Mnemonic.LdaZeroPage] = new CpuInstruction(ZeroPage, 3);
            opcodeHandlers[Mnemonic.LdaZeroPageX] = new CpuInstruction(ZeroPageX, 3);
            opcodeHandlers[Mnemonic.LdaAbsolute] = new CpuInstruction(Absolute, 4);
            opcodeHandlers[Mnemonic.LdaAbsoluteX] = new CpuInstruction(AbsoluteX, 4);
            opcodeHandlers[Mnemonic.LdaAbsoluteY] = new CpuInstruction(AbsoluteY, 4);
            opcodeHandlers[Mnemonic.LdaIndirectX] = new CpuInstruction(IndirectX, 6);
            opcodeHandlers[Mnemonic.LdaIndirectY] = new CpuInstruction(IndirectY, 5);
        }

        private void Immediate()
        {
            Cpu.A = Cpu.GetNextByte();
            SetNegativeZeroFlags(Cpu.A);
        }

        private void ZeroPage()
        {
            Cpu.A = GetRegisterZeroPage();
        }

        private void ZeroPageX()
        {
            Cpu.A = GetRegisterZeroPageX(Cpu.X);
        }

        private void Absolute()
        {
            Cpu.A = GetRegisterAbsolute();
        }

        private void AbsoluteX()
        {
            Cpu.A = GetRegisterAbsoluteX(Cpu.X);
        }

        private void AbsoluteY()
        {
            Cpu.A = GetRegisterAbsoluteX(Cpu.Y);
        }

        private void IndirectX()
        {
            Int16 location = GetIndirectXAddress(Cpu.GetNextByte(), Cpu.X);
            Cpu.A = Cpu.Ram.ReadByte(location);
            SetNegativeZeroFlags(Cpu.A);
        }

        private void IndirectY()
        {
            Int16 location = GetIndirectYAddress(Cpu.GetNextByte(), Cpu.Y);
            Cpu.A = Cpu.Ram.ReadByte(location);
            SetNegativeZeroFlags(Cpu.A);
        }
    }
}
