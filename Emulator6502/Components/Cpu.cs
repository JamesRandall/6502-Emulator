using System;
using Emulator6502.Components.OpcodeExecuters;

namespace Emulator6502.Components
{
    public class Cpu : ICpu
    {
        

        private readonly CpuInstruction[] _opcodeActions = new CpuInstruction[0xFF];

        public Cpu()
        {
            InitializeOpcodes();

            Ram = new Ram(32 * 1024);
            StackPointer = 0x1FF;
            ProgramCounter = 0xE00;
        }

        public IRam Ram { get; set; }
        public bool CarryFlag { get { return (Status & StatusFlagBits.CarryFlagBit) > 0; } }
        public bool ZeroFlag { get { return (Status & StatusFlagBits.ZeroFlagBit) > 0; } }
        public bool InterruptFlag { get { return (Status & StatusFlagBits.InterruptFlagBit) > 0; } }
        public bool DecimalModeFlag { get { return (Status & StatusFlagBits.DecimalModeFlagBit) > 0; } }
        public bool BreakFlag { get { return (Status & StatusFlagBits.BreakFlagBit) > 0; } }
        public bool OverflowFlag { get { return (Status & StatusFlagBits.OverflowFlagBit) > 0; } }
        public bool NegativeFlag { get { return (Status & StatusFlagBits.NegativeFlagBit) > 0; } }

        private void InitializeOpcodes()
        {
            new Lda(this).Register(_opcodeActions);
            new Ldx(this).Register(_opcodeActions);
            new Ldy(this).Register(_opcodeActions);
            
            new Sta(this).Register(_opcodeActions);
            new Stx(this).Register(_opcodeActions);
            new Sty(this).Register(_opcodeActions);
        }

        public byte A { get; set; }
        public byte X { get; set; }
        public byte Y { get; set; }
        // C - bit 0 (carry flag)
        // Z - bit 1 (zero flag)
        // I - bit 2 (interrupt flag)
        // D - bit 3 (decimal mode flag)
        // B - bit 4 (break flag)
        // V - bit 6 (overflow flag)
        // N - bit 7 (negative flag)
        public byte Status { get; set; }
        public Int16 ProgramCounter { get; set; }
        public Int16 StackPointer { get; set; }

        public void Execute()
        {
            while (true)
            {
                byte opcode = GetNextByte();
                if (opcode == 0x00) break; // brk, temp

                CpuInstruction instruction = _opcodeActions[opcode];
                if (instruction != null)
                {
                    instruction.Action();
                }
                else
                {
                    throw new UnsupportedOpcodeException(opcode);
                }
            }
            
        }

        public byte GetNextByte()
        {
            byte value = Ram.ReadByte(ProgramCounter);
            ProgramCounter++;
            return value;
        }

        public Int16 GetNextWord()
        {
            byte low = GetNextByte();
            byte high = GetNextByte();
            return (Int16)((high << 8) | low);
        }
    }
}
