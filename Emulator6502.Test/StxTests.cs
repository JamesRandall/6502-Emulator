using System;
using Emulator6502.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emulator6502.Test
{
    [TestClass]
    public class StxTests
    {
        [TestMethod]
        public void StxZeroPage()
        {
            Cpu cpu = new Cpu();
            cpu.X = 0x10;
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.StxZeroPage);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0xF);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0x10, cpu.Ram.ReadByte(0xF));
        }

        [TestMethod]
        public void StxZeroPageY()
        {
            Cpu cpu = new Cpu();
            cpu.X = 0xF0;
            cpu.Y = 0x1;
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.StxZeroPageY);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0xF);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0xF0, cpu.Ram.ReadByte(0x10));
        }

        [TestMethod]
        public void StxAbsolute()
        {
            Cpu cpu = new Cpu();
            cpu.X = 0xF0;
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.StxAbsolute);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x20);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 2), 0x30);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0xF0, cpu.Ram.ReadByte(0x3020));
        }
    }
}
