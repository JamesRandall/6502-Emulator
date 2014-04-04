using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emulator6502.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emulator6502.Test
{
    [TestClass]
    public class IncTests
    {
        [TestMethod]
        public void ZeroPage()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(0xF, 0x40);
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.IncZeroPage);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0xF);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0x41, cpu.Ram.ReadByte(0xF));
        }

        [TestMethod]
        public void ZeroPageX()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.X = 0x1;
            cpu.Ram.WriteByte(0x10, 0x40);
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.IncZeroPageX);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0xF);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0x41, cpu.Ram.ReadByte(0x10));
        }

        [TestMethod]
        public void Absolute()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(0x2030, 0x40);
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.IncAbsolute);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x30);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 2), 0x20);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0x41, cpu.Ram.ReadByte(0x2030));
        }

        [TestMethod]
        public void AbsoluteX()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.X = 0x1;
            cpu.Ram.WriteByte(0x2031, 0x40);
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.IncAbsoluteX);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x30);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 2), 0x20);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0x41, cpu.Ram.ReadByte(0x2031));
        }
    }
}
