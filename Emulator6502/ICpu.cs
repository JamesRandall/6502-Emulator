using System;

namespace Emulator6502
{
    public interface ICpu
    {
        IRam Ram { get; set; }
        bool CarryFlag { get; }
        bool ZeroFlag { get; }
        bool InterruptFlag { get; }
        bool DecimalModeFlag { get; }
        bool BreakFlag { get; }
        bool OverflowFlag { get; }
        bool NegativeFlag { get; }
        byte A { get; set; }
        byte X { get; set; }
        byte Y { get; set; }
        byte Status { get; set; }
        Int16 ProgramCounter { get; set; }
        Int16 StackPointer { get; set; }
        void Execute();
        byte GetNextByte();
        Int16 GetNextWord();
    }
}