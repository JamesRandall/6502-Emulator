namespace Emulator6502.Components.OpcodeExecuters
{
    internal class Ldx : OpcodeExecuterBase
    {
        public Ldx(ICpu cpu) : base(cpu)
        {
        }

        public override void Register(CpuInstruction[] opcodeHandlers)
        {
            opcodeHandlers[Mnemonic.LdxImmediate] = new CpuInstruction(Immediate, 2);
            opcodeHandlers[Mnemonic.LdxZeroPage] = new CpuInstruction(ZeroPage, 3);
            opcodeHandlers[Mnemonic.LdxZeroPageY] = new CpuInstruction(ZeroPageY, 4);
            opcodeHandlers[Mnemonic.LdxAbsolute] = new CpuInstruction(Absolute, 4);
            opcodeHandlers[Mnemonic.LdxAbsoluteY] = new CpuInstruction(AbsoluteY, 4);
        }

        private void Immediate()
        {
            Cpu.X = Cpu.GetNextByte();
            SetNegativeZeroFlags(Cpu.X);
        }

        private void ZeroPage()
        {
            Cpu.X = GetRegisterZeroPage();
        }

        private void ZeroPageY()
        {
            Cpu.X = GetRegisterZeroPageX(Cpu.Y);
        }

        private void Absolute()
        {
            Cpu.X = GetRegisterAbsolute();
        }

        private void AbsoluteY()
        {
            Cpu.X = GetRegisterAbsoluteX(Cpu.Y);
        }
    }
}
