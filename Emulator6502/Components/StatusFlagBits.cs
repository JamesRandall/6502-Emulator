using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulator6502.Components
{
    public static class StatusFlagBits
    {
        public const byte CarryFlagBit = 1;
        public const byte ZeroFlagBit = 2;
        public const byte InterruptFlagBit = 4;
        public const byte DecimalModeFlagBit = 8;
        public const byte BreakFlagBit = 16;
        public const byte OverflowFlagBit = 64;
        public const byte NegativeFlagBit = 128;
        public const byte ZeroNegativeMask = CarryFlagBit | InterruptFlagBit | DecimalModeFlagBit | BreakFlagBit | OverflowFlagBit | NegativeFlagBit;
    }
}
