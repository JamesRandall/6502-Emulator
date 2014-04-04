namespace Emulator6502
{
    public class Mnemonic
    {
        public const byte LdaImmediate = 0xA9;
        public const byte LdaZeroPage = 0xA5;
        public const byte LdaZeroPageX = 0xB5;
        public const byte LdaAbsolute = 0xAD;
        public const byte LdaAbsoluteX = 0xBD;
        public const byte LdaAbsoluteY = 0xB9;
        public const byte LdaIndirectX = 0xA1;
        public const byte LdaIndirectY = 0xB1;

        public const byte LdxImmediate = 0xA2;
        public const byte LdxZeroPage = 0xA6;
        public const byte LdxZeroPageY = 0xB6;
        public const byte LdxAbsolute = 0xAE;
        public const byte LdxAbsoluteY = 0xBE;

        public const byte LdyImmediate = 0xA0;
        public const byte LdyZeroPage = 0xA4;
        public const byte LdyZeroPageX = 0xB4;
        public const byte LdyAbsolute = 0xAC;
        public const byte LdyAbsoluteX = 0xBC;

        public const byte StaZeroPage = 0x85;
        public const byte StaZeroPageX = 0x95;
        public const byte StaAbsolute = 0x8D;
        public const byte StaAbsoluteX = 0x9D;
        public const byte StaAbsoluteY = 0x99;
        public const byte StaIndirectX = 0x81;
        public const byte StaIndirectY = 0x91;

        public const byte StxAbsolute = 0x8E;
        public const byte StxZeroPage = 0x86;
        public const byte StxZeroPageY = 0x96;

        public const byte StyAbsolute = 0x8F;
        public const byte StyZeroPage = 0x84;
        public const byte StyZeroPageX = 0x94;

        public const byte IncZeroPage = 0xE6;
        public const byte IncZeroPageX = 0xF6;
        public const byte IncAbsolute = 0xEE;
        public const byte IncAbsoluteX = 0xFE;
    }
}
