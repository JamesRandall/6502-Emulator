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
    public class StyTests
    {
        [TestMethod]
        public void StyZeroPage()
        {
            Cpu cpu = new Cpu();
            cpu.Y = 0x10;
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.StyZeroPage);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0xF);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0x10, cpu.Ram.ReadByte(0xF));
        }

        [TestMethod]
        public void StyZeroPageX()
        {
            Cpu cpu = new Cpu();
            cpu.Y = 0xF0;
            cpu.X = 0x1;
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.StyZeroPageX);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0xF);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0xF0, cpu.Ram.ReadByte(0x10));
        }

        [TestMethod]
        public void StyAbsolute()
        {
            Cpu cpu = new Cpu();
            cpu.Y = 0xF0;
            cpu.Ram.WriteByte(cpu.ProgramCounter, Mnemonic.StyAbsolute);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 1), 0x20);
            cpu.Ram.WriteByte((Int16)(cpu.ProgramCounter + 2), 0x30);

            // Act
            cpu.Execute();

            // Assert
            Assert.AreEqual(0xF0, cpu.Ram.ReadByte(0x3020));
        }
    }
}
