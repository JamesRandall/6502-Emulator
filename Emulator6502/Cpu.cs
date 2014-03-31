using System;
using Emulator6502.Components;

namespace Emulator6502
{
    public class Cpu
    {
        private const byte CarryFlagBit = 1;
        private const byte ZeroFlagBit = 2;
        private const byte InterruptFlagBit = 4;
        private const byte DecimalModeFlagBit = 8;
        private const byte BreakFlagBit = 16;
        private const byte OverflowFlagBit = 64;
        private const byte NegativeFlagBit = 128;
        private const byte ZeroNegativeMask = CarryFlagBit | InterruptFlagBit | DecimalModeFlagBit | BreakFlagBit | OverflowFlagBit | NegativeFlagBit;

        private readonly CpuInstruction[] _opcodeActions = new CpuInstruction[0xFF];

        public Cpu()
        {
            InitializeOpcodes();

            Ram = new Ram(32 * 1024);
            StackPointer = 0x1FF;
            ProgramCounter = 0xE00;
        }

        public IRam Ram { get; set; }
        public bool CarryFlag { get { return (Status & CarryFlagBit) > 0; } }
        public bool ZeroFlag { get { return (Status & ZeroFlagBit) > 0; } }
        public bool InterruptFlag { get { return (Status & InterruptFlagBit) > 0; } }
        public bool DecimalModeFlag { get { return (Status & DecimalModeFlagBit) > 0; } }
        public bool BreakFlag { get { return (Status & BreakFlagBit) > 0; } }
        public bool OverflowFlag { get { return (Status & OverflowFlagBit) > 0; } }
        public bool NegativeFlag { get { return (Status & NegativeFlagBit) > 0; } }

        private void InitializeOpcodes()
        {
            _opcodeActions[Opcode.LdaImmediate] = new CpuInstruction(LdaImmediate, 2);
            _opcodeActions[Opcode.LdaZeroPage] = new CpuInstruction(LdaZeroPage, 3);
            _opcodeActions[Opcode.LdaZeroPageX] = new CpuInstruction(LdaZeroPageX, 3);
            
            _opcodeActions[Opcode.StaAbsolute] = new CpuInstruction(StaAbsolute, 2);
            _opcodeActions[Opcode.StxAbsolute] = new CpuInstruction(StxAbsolute, 2);
            _opcodeActions[Opcode.StyAbsolute] = new CpuInstruction(StyAbsolute, 2);
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
                if (opcode == 0x00) break; // brk

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

        private byte GetNextByte()
        {
            byte value = Ram.ReadByte(ProgramCounter);
            ProgramCounter++;
            return value;
        }

        private Int16 GetNextWord()
        {
            byte low = GetNextByte();
            byte high = GetNextByte();
            return (Int16)((high << 8) | low);
        }

        private void SetNegativeZeroFlags()
        {
            byte value = (byte)(((A == 0) ? ZeroFlagBit : 0x0) | (A & NegativeFlagBit));
            Status = (byte)((Status & ZeroNegativeMask) | value);
        }

        #region Opcode Handlers

        private void LdaImmediate()
        {
            A = GetNextByte();
            SetNegativeZeroFlags();
        }

        private void LdaZeroPage()
        {
            byte location = GetNextByte();
            A = Ram.ReadByte(location);
            SetNegativeZeroFlags();
        }

        private void LdaZeroPageX()
        {
            Int16 location = GetNextByte();
            location += X;
            A = Ram.ReadByte(location);
            SetNegativeZeroFlags();
        }

        private void StaAbsolute()
        {
            Int16 location = GetNextWord();
            Ram.WriteByte(location, A);
        }

        private void StxAbsolute()
        {
            Int16 location = GetNextWord();
            Ram.WriteByte(location, X);
        }

        private void StyAbsolute()
        {
            Int16 location = GetNextWord();
            Ram.WriteByte(location, Y);
        }

        #endregion
    }
}
