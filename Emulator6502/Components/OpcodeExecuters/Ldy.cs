namespace Emulator6502.Components.OpcodeExecuters
{
    internal class Ldy : OpcodeExecuterBase
    {
        public Ldy(ICpu cpu) : base(cpu)
        {
        }

        public override void Register(CpuInstruction[] opcodeHandlers)
        {
            opcodeHandlers[Mnemonic.LdyImmediate] = new CpuInstruction(LdyImmediate, 2);
            opcodeHandlers[Mnemonic.LdyZeroPage] = new CpuInstruction(LdyZeroPage, 3);
            opcodeHandlers[Mnemonic.LdyZeroPageX] = new CpuInstruction(LdyZeroPageX, 4);
            opcodeHandlers[Mnemonic.LdyAbsolute] = new CpuInstruction(LdyAbsolute, 4);
            opcodeHandlers[Mnemonic.LdyAbsoluteX] = new CpuInstruction(LdyAbsoluteX, 4);
        }

        private void LdyImmediate()
        {
            Cpu.Y = Cpu.GetNextByte();
            SetNegativeZeroFlags(Cpu.Y);
        }

        private void LdyZeroPage()
        {
            Cpu.Y = GetRegisterZeroPage();
        }

        private void LdyZeroPageX()
        {
            Cpu.Y = GetRegisterZeroPageX(Cpu.X);
        }

        private void LdyAbsolute()
        {
            Cpu.Y = GetRegisterAbsolute();
        }

        private void LdyAbsoluteX()
        {
            Cpu.Y = GetRegisterAbsoluteX(Cpu.X);
        }
    }
}
