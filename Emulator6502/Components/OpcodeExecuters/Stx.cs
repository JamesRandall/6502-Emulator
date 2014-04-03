using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulator6502.Components.OpcodeExecuters
{
    internal class Stx : OpcodeExecuterBase
    {
        public Stx(ICpu cpu) : base(cpu)
        {
        }

        public override void Register(CpuInstruction[] opcodeHandlers)
        {
            opcodeHandlers[Mnemonic.StxAbsolute] = new CpuInstruction(StxAbsolute, 2);
        }

        private void StxAbsolute()
        {
            Int16 location = Cpu.GetNextWord();
            Cpu.Ram.WriteByte(location, Cpu.X);
        }
    }
}
