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
            opcodeHandlers[Mnemonic.StyAbsolute] = new CpuInstruction(StyAbsolute, 2);
        }

        private void StyAbsolute()
        {
            Int16 location = Cpu.GetNextWord();
            Cpu.Ram.WriteByte(location, Cpu.Y);
        }
    }
}
