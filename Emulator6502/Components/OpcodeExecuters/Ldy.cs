namespace Emulator6502.Components.OpcodeExecuters
{
    internal class Ldy : OpcodeExecuterBase
    {
        public Ldy(ICpu cpu) : base(cpu)
        {
        }

        public override void Register(CpuInstruction[] opcodeHandlers)
        {
            opcodeHandlers[Mnemonic.LdyImmediate] = new CpuInstruction(Immediate, 2);
            opcodeHandlers[Mnemonic.LdyZeroPage] = new CpuInstruction(ZeroPage, 3);
            opcodeHandlers[Mnemonic.LdyZeroPageX] = new CpuInstruction(ZeroPageX, 4);
            opcodeHandlers[Mnemonic.LdyAbsolute] = new CpuInstruction(Absolute, 4);
            opcodeHandlers[Mnemonic.LdyAbsoluteX] = new CpuInstruction(AbsoluteX, 4);
        }

        private void Immediate()
        {
            Cpu.Y = Cpu.GetNextByte();
            SetNegativeZeroFlags(Cpu.Y);
        }

        private void ZeroPage()
        {
            Cpu.Y = GetRegisterZeroPage();
        }

        private void ZeroPageX()
        {
            Cpu.Y = GetRegisterZeroPageX(Cpu.X);
        }

        private void Absolute()
        {
            Cpu.Y = GetRegisterAbsolute();
        }

        private void AbsoluteX()
        {
            Cpu.Y = GetRegisterAbsoluteX(Cpu.X);
        }
    }
}
