using System;
using Emulator6502.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emulator6502.Test
{
    [TestClass]
    public class StaTests
    {
        [TestMethod]
        public void StaZeroPage()
        {
            Cpu cpu = new Cpu();
            cpu.A = 0x10;
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.StaZeroPage);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0xF);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0x10, cpu.Ram.ReadByte(0xF));
        }

        [TestMethod]
        public void StaZeroPageX()
        {
            Cpu cpu = new Cpu();
            cpu.A = 0xF0;
            cpu.X = 0x1;
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.StaZeroPageX);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0xF);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0xF0, cpu.Ram.ReadByte(0x10));
        }

        [TestMethod]
        public void StaAbsolute()
        {
            Cpu cpu = new Cpu();
            cpu.A = 0xF0;
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.StaAbsolute);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x20);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 2), 0x30);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0xF0, cpu.Ram.ReadByte(0x3020));
        }

        [TestMethod]
        public void StaAbsoluteX()
        {
            Cpu cpu = new Cpu();
            cpu.A = 0xF0;
            cpu.X = 0x1;
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.StaAbsoluteX);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x20);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 2), 0x30);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0xF0, cpu.Ram.ReadByte(0x3021));
        }

        [TestMethod]
        public void StaAbsoluteY()
        {
            Cpu cpu = new Cpu();
            cpu.A = 0xF0;
            cpu.Y = 0x1;
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.StaAbsoluteY);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x20);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 2), 0x30);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0xF0, cpu.Ram.ReadByte(0x3021));
        }

        [TestMethod]
        public void StaIndirectX()
        {
            Cpu cpu = new Cpu();
            cpu.A = 0xF0;
            cpu.X = 0x1;
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.StaIndirectX);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x10);
            cpu.Ram.WriteByte(0x11, 0x20);
            cpu.Ram.WriteByte(0x12, 0x30);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0xF0, cpu.Ram.ReadByte(0x3020));
        }

        [TestMethod]
        public void StaIndirectY()
        {
            Cpu cpu = new Cpu();
            cpu.A = 0xF0;
            cpu.Y = 0x1;
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.StaIndirectY);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x10);
            cpu.Ram.WriteByte(0x10, 0x20);
            cpu.Ram.WriteByte(0x11, 0x30);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0xF0, cpu.Ram.ReadByte(0x3021));
        }
    }
}
