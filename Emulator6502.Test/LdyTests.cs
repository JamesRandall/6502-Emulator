using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emulator6502.Test
{
    [TestClass]
    public class LdyTests
    {
        [TestMethod]
        public void Immediate()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(cpu.ProgramCounter, Opcode.LdyImmediate);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 41);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(41, cpu.Y);
        }

        [TestMethod]
        public void ZeroPage()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(0xF, 41);
            cpu.Ram.WriteByte(cpu.ProgramCounter, Opcode.LdyZeroPage);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0xF);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(41, cpu.Y);
        }

        [TestMethod]
        public void ZeroPageX()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(0x1F, 0x5);
            cpu.Ram.WriteByte(cpu.ProgramCounter, Opcode.LdyZeroPageX);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x1E);
            cpu.X = 0x1;

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0x5, cpu.Y);
        }

        [TestMethod]
        public void Absolute()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(0x2310, 0x5);
            cpu.Ram.WriteByte(cpu.ProgramCounter, Opcode.LdyAbsolute);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x10);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 2), 0x23);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0x5, cpu.Y);
        }


        [TestMethod]
        public void AbsoluteX()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(0x2311, 0x5);
            cpu.Ram.WriteByte(cpu.ProgramCounter, Opcode.LdyAbsoluteX);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x10);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 2), 0x23);
            cpu.X = 0x1;

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0x5, cpu.Y);
        }

        #region Status flag updates - all tested in immediate mode

        [TestMethod]
        public void ZeroFlagNotSet()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(cpu.ProgramCounter, Opcode.LdyImmediate);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 1);

            // Act
            cpu.Execute();

            // Assert
            Assert.IsFalse(cpu.ZeroFlag);
        }

        [TestMethod]
        public void ZeroFlagSet()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(cpu.ProgramCounter, Opcode.LdyImmediate);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0);

            // Act
            cpu.Execute();

            // Assert
            Assert.IsTrue(cpu.ZeroFlag);
        }

        [TestMethod]
        public void NegativeFlagNotSet()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(cpu.ProgramCounter, Opcode.LdyImmediate);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 127);

            // Act
            cpu.Execute();

            // Assert
            Assert.IsFalse(cpu.NegativeFlag);
        }

        [TestMethod]
        public void NegativeFlagSet()
        {
            // Arrange
            Cpu cpu = new Cpu();
            cpu.Ram.WriteByte(cpu.ProgramCounter, Opcode.LdyImmediate);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 255);

            // Act
            cpu.Execute();

            // Assert
            Assert.IsTrue(cpu.NegativeFlag);
        }

        #endregion
    }
}
