using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulator6502.Components.OpcodeExecuters
{
    internal class Sta : OpcodeExecuterBase
    {
        public Sta(ICpu cpu) : base(cpu)
        {
        }

        public override void Register(CpuInstruction[] opcodeHandlers)
        {
            opcodeHandlers[Mnemonic.StaAbsolute] = new CpuInstruction(StaAbsolute, 2);
        }

        private void StaAbsolute()
        {
            Int16 location = Cpu.GetNextWord();
            Cpu.Ram.WriteByte(location, Cpu.A);
        }
    }
}
