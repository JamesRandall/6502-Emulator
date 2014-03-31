using System;

namespace Emulator6502.Components
{
    public class CpuInstruction
    {
        public CpuInstruction(Action action, int timing)
        {
            Action = action;
            Timing = timing;
        }

        public Action Action { get; private set; }

        public int Timing { get; private set; }
    }
}
