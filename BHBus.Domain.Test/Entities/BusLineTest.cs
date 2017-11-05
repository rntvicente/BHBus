using BHBus.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BHBus.Domain.Test.Entities
{
    [TestClass]
    public class BusLineTest
    {
        private string NumberLine { get; set; }

        public BusLineTest()
        {
            NumberLine = "9191-L";
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BusLineTest_New_NumeroDaLinha_Required()
        {
            new BusLine(null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BusLineTest_New_NumeroDaLinha_MaxLength()
        {
            for (int i = 0, j = BusLine.BusLineMaxLength + 1; i < j; i++)
            {
                NumberLine = NumberLine + "Z";
            }

            new BusLine(NumberLine);
        }

        [TestMethod]
        public void BusLineTest_New_NumberLine()
        {
            var busLine = new BusLine(NumberLine);

            Assert.AreEqual(NumberLine,busLine.Line);
        }
    }
}
