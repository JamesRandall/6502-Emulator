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
            _opcodeActions[Opcode.LdaAbsolute] = new CpuInstruction(LdaAbsolute, 4);
            _opcodeActions[Opcode.LdaAbsoluteX] = new CpuInstruction(LdaAbsoluteX, 4);
            _opcodeActions[Opcode.LdaAbsoluteY] = new CpuInstruction(LdaAbsoluteY, 4);
            _opcodeActions[Opcode.LdaIndirectX] = new CpuInstruction(LdaIndirectX, 6);
            _opcodeActions[Opcode.LdaIndirectY] = new CpuInstruction(LdaIndirectY, 5);

            _opcodeActions[Opcode.LdxImmediate] = new CpuInstruction(LdxImmediate, 2);
            _opcodeActions[Opcode.LdxZeroPage] = new CpuInstruction(LdxZeroPage, 3);
            _opcodeActions[Opcode.LdxZeroPageY] = new CpuInstruction(LdxZeroPageY, 4);
            _opcodeActions[Opcode.LdxAbsolute] = new CpuInstruction(LdxAbsolute, 4);
            _opcodeActions[Opcode.LdxAbsoluteY] = new CpuInstruction(LdxAbsoluteY, 4);

            _opcodeActions[Opcode.LdyImmediate] = new CpuInstruction(LdyImmediate, 2);
            _opcodeActions[Opcode.LdyZeroPage] = new CpuInstruction(LdyZeroPage, 3);
            _opcodeActions[Opcode.LdyZeroPageX] = new CpuInstruction(LdyZeroPageX, 4);
            _opcodeActions[Opcode.LdyAbsolute] = new CpuInstruction(LdyAbsolute, 4);
            _opcodeActions[Opcode.LdyAbsoluteX] = new CpuInstruction(LdyAbsoluteX, 4);
            
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

        private void SetNegativeZeroFlags(byte register)
        {
            byte value = (byte)(((register == 0) ? ZeroFlagBit : 0x0) | (register & NegativeFlagBit));
            Status = (byte)((Status & ZeroNegativeMask) | value);
        }

        private byte GetRegisterZeroPage()
        {
            byte location = GetNextByte();
            byte register = Ram.ReadByte(location);
            SetNegativeZeroFlags(register);
            return register;
        }

        private byte GetRegisterZeroPageX(byte x)
        {
            Int16 location = (Int16)(GetNextByte() + x);
            if (location > 0xFF) location -= 0xFF;
            byte register = Ram.ReadByte(location);
            SetNegativeZeroFlags(register);
            return register;
        }

        private byte GetRegisterAbsolute()
        {
            Int16 location = GetNextWord();
            byte value = Ram.ReadByte(location);
            SetNegativeZeroFlags(value);
            return value;
        }

        private byte GetRegisterAbsoluteX(byte x)
        {
            Int16 location = GetNextWord();
            location += x;
            byte value = Ram.ReadByte(location);
            SetNegativeZeroFlags(value);
            return value;
        }

        #region LDX opcode handlers

        private void LdxImmediate()
        {
            X = GetNextByte();
            SetNegativeZeroFlags(X);
        }

        private void LdxZeroPage()
        {
            X = GetRegisterZeroPage();
        }

        private void LdxZeroPageY()
        {
            X = GetRegisterZeroPageX(Y);
        }

        private void LdxAbsolute()
        {
            X = GetRegisterAbsolute();
        }

        private void LdxAbsoluteY()
        {
            X = GetRegisterAbsoluteX(Y);
        }

        #endregion

        #region LDY opcode handlers

        private void LdyImmediate()
        {
            Y = GetNextByte();
            SetNegativeZeroFlags(Y);
        }

        private void LdyZeroPage()
        {
            Y = GetRegisterZeroPage();
        }

        private void LdyZeroPageX()
        {
            Y = GetRegisterZeroPageX(X);
        }

        private void LdyAbsolute()
        {
            Y = GetRegisterAbsolute();
        }

        private void LdyAbsoluteX()
        {
            Y = GetRegisterAbsoluteX(X);
        }

        #endregion

        #region LDA Opcode Handlers

        private void LdaImmediate()
        {
            A = GetNextByte();
            SetNegativeZeroFlags(A);
        }

        private void LdaZeroPage()
        {
            A = GetRegisterZeroPage();
        }

        private void LdaZeroPageX()
        {
            A = GetRegisterZeroPageX(X);
        }

        private void LdaAbsolute()
        {
            A = GetRegisterAbsolute();
        }

        private void LdaAbsoluteX()
        {
            A = GetRegisterAbsoluteX(X);
        }

        private void LdaAbsoluteY()
        {
            A = GetRegisterAbsoluteX(Y);
        }

        private void LdaIndirectX()
        {
            // what happens on oxff boundary
            Int16 zeroPageLocation = (Int16)(GetNextByte() + X); 
            Int16 location = Ram.ReadWord(zeroPageLocation);
            A = Ram.ReadByte(location);
            SetNegativeZeroFlags(A);
        }

        private void LdaIndirectY()
        {
            byte zeroPageLocation = GetNextByte();
            Int16 location = (Int16)(Ram.ReadWord(zeroPageLocation) + Y);
            A = Ram.ReadByte(location);
            SetNegativeZeroFlags(A);
        }

#endregion

        #region Store opcodes

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
