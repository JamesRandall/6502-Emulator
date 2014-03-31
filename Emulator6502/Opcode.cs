namespace Emulator6502
{
    public class Opcode
    {
        public const byte LdaImmediate = 0xA9;
        public const byte LdaZeroPage = 0xA5;
        public const byte LdaZeroPageX = 0xB5;
        public const byte LdaAbsolute = 0xAD;
        public const byte LdaAbsoluteX = 0xBD;
        public const byte LdaAbsoluteY = 0xB9;
        public const byte LdaIndirectX = 0xA1;
        public const byte LdaIndirectY = 0xB1;

        public const byte StaAbsolute = 0x8D;
        public const byte StxAbsolute = 0x8E;
        public const byte StyAbsolute = 0x8F;
    }
}
