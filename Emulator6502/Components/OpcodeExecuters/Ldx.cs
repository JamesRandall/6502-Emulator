namespace Emulator6502.Components.OpcodeExecuters
{
    internal class Ldx : OpcodeExecuterBase
    {
        public Ldx(ICpu cpu) : base(cpu)
        {
        }

        public override void Register(CpuInstruction[] opcodeHandlers)
        {
            opcodeHandlers[Mnemonic.LdxImmediate] = new CpuInstruction(LdxImmediate, 2);
            opcodeHandlers[Mnemonic.LdxZeroPage] = new CpuInstruction(LdxZeroPage, 3);
            opcodeHandlers[Mnemonic.LdxZeroPageY] = new CpuInstruction(LdxZeroPageY, 4);
            opcodeHandlers[Mnemonic.LdxAbsolute] = new CpuInstruction(LdxAbsolute, 4);
            opcodeHandlers[Mnemonic.LdxAbsoluteY] = new CpuInstruction(LdxAbsoluteY, 4);
        }

        private void LdxImmediate()
        {
            Cpu.X = Cpu.GetNextByte();
            SetNegativeZeroFlags(Cpu.X);
        }

        private void LdxZeroPage()
        {
            Cpu.X = GetRegisterZeroPage();
        }

        private void LdxZeroPageY()
        {
            Cpu.X = GetRegisterZeroPageX(Cpu.Y);
        }

        private void LdxAbsolute()
        {
            Cpu.X = GetRegisterAbsolute();
        }

        private void LdxAbsoluteY()
        {
            Cpu.X = GetRegisterAbsoluteX(Cpu.Y);
        }
    }
}
