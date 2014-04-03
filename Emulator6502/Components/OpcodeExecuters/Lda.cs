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
            opcodeHandlers[Mnemonic.LdaImmediate] = new CpuInstruction(LdaImmediate, 2);
            opcodeHandlers[Mnemonic.LdaZeroPage] = new CpuInstruction(LdaZeroPage, 3);
            opcodeHandlers[Mnemonic.LdaZeroPageX] = new CpuInstruction(LdaZeroPageX, 3);
            opcodeHandlers[Mnemonic.LdaAbsolute] = new CpuInstruction(LdaAbsolute, 4);
            opcodeHandlers[Mnemonic.LdaAbsoluteX] = new CpuInstruction(LdaAbsoluteX, 4);
            opcodeHandlers[Mnemonic.LdaAbsoluteY] = new CpuInstruction(LdaAbsoluteY, 4);
            opcodeHandlers[Mnemonic.LdaIndirectX] = new CpuInstruction(LdaIndirectX, 6);
            opcodeHandlers[Mnemonic.LdaIndirectY] = new CpuInstruction(LdaIndirectY, 5);
        }

        private void LdaImmediate()
        {
            Cpu.A = Cpu.GetNextByte();
            SetNegativeZeroFlags(Cpu.A);
        }

        private void LdaZeroPage()
        {
            Cpu.A = GetRegisterZeroPage();
        }

        private void LdaZeroPageX()
        {
            Cpu.A = GetRegisterZeroPageX(Cpu.X);
        }

        private void LdaAbsolute()
        {
            Cpu.A = GetRegisterAbsolute();
        }

        private void LdaAbsoluteX()
        {
            Cpu.A = GetRegisterAbsoluteX(Cpu.X);
        }

        private void LdaAbsoluteY()
        {
            Cpu.A = GetRegisterAbsoluteX(Cpu.Y);
        }

        private void LdaIndirectX()
        {
            // what happens on oxff boundary
            Int16 zeroPageLocation = (Int16)(Cpu.GetNextByte() + Cpu.X);
            Int16 location = Cpu.Ram.ReadWord(zeroPageLocation);
            Cpu.A = Cpu.Ram.ReadByte(location);
            SetNegativeZeroFlags(Cpu.A);
        }

        private void LdaIndirectY()
        {
            byte zeroPageLocation = Cpu.GetNextByte();
            Int16 location = (Int16)(Cpu.Ram.ReadWord(zeroPageLocation) + Cpu.Y);
            Cpu.A = Cpu.Ram.ReadByte(location);
            SetNegativeZeroFlags(Cpu.A);
        }
    }
}
