using System;
using Auto.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Auto.UnitTests
{
    [TestFixture]
    public class RentPriceToolTest
    {
        [Test]
        public void NotReturnedTest()
        {
            var rentTime = DateTime.Now.AddDays(-1);
            var returnTime = (DateTime?)null;
            var price = RentPriceTool.CalculatePrice(rentTime, returnTime);
            Assert.AreEqual(null, price);
        }

        [Test]
        public void ExactHourTest()
        {
            var rentTime = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            var returnTime = new DateTime(2000, 1, 1, 1, 0, 0, 0);
            var price = RentPriceTool.CalculatePrice(rentTime, returnTime);
            var expectedPrice = (int)Math.Round(3600 * RentPriceTool.Tariff1PerSecond);
            Assert.AreEqual(expectedPrice, price);
        }

        [Test]
        public void Tariff1WindowsTest()
        {
            TariffWindowsTest(new DateTime(2000, 1, 1, 5, 59, 0, 0), new DateTime(2000, 1, 1, 7, 0, 0, 0), RentPriceTool.Tariff1PerSecond);
            TariffWindowsTest(new DateTime(2000, 1, 1, 0, 0, 0, 0), new DateTime(2000, 1, 1, 7, 0, 0, 0), RentPriceTool.Tariff1PerSecond);
            TariffWindowsTest(new DateTime(2000, 1, 1, 11, 0, 0, 0), new DateTime(2000, 1, 1, 17, 0, 0, 0), RentPriceTool.Tariff1PerSecond);
            TariffWindowsTest(new DateTime(2000, 1, 1, 20, 0, 0, 0), new DateTime(2000, 1, 2, 0, 0, 0, 0), RentPriceTool.Tariff1PerSecond);
        }

        [Test]
        public void Tariff2WindowsTest()
        {
            TariffWindowsTest(new DateTime(2000, 1, 1, 7, 0, 0, 0), new DateTime(2000, 1, 1, 11, 0, 0, 0), RentPriceTool.Tariff2PerSecond);
            TariffWindowsTest(new DateTime(2000, 1, 1, 17, 0, 0, 0), new DateTime(2000, 1, 1, 20, 0, 0, 0), RentPriceTool.Tariff2PerSecond);
        }

        [Test]
        public void MultipleTariffsSpanTest()
        {
            var rentTime = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            var returnTime = new DateTime(2000, 1, 1, 8, 0, 0, 0);
            var price = RentPriceTool.CalculatePrice(rentTime, returnTime);
            var expectedPrice = (int)Math.Round(7 * 3600 * RentPriceTool.Tariff1PerSecond) 
                + (int)Math.Round(3600 * RentPriceTool.Tariff2PerSecond);
            Assert.AreEqual(expectedPrice, price);
        }

        [Test]
        public void PartHourTest()
        {
            var rentTime = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            var returnTime = new DateTime(2000, 1, 1, 1, 0, 1, 0);
            var price = RentPriceTool.CalculatePrice(rentTime, returnTime);
            var expectedPrice = (int)Math.Round(2 * 3600 * RentPriceTool.Tariff1PerSecond);
            Assert.AreEqual(expectedPrice, price);
        }

        [Test]
        public void NextDaySpanTest()
        {
            var rentTime = new DateTime(2000, 1, 1, 23, 0, 0, 0);
            var returnTime = new DateTime(2000, 1, 2, 8, 0, 0, 0);
            var price = RentPriceTool.CalculatePrice(rentTime, returnTime);
            var expectedPrice = (int)Math.Round(8 * 3600 * RentPriceTool.Tariff1PerSecond)
                + (int)Math.Round(3600 * RentPriceTool.Tariff2PerSecond);
            Assert.AreEqual(expectedPrice, price);
        }

        private void TariffWindowsTest(DateTime rentTime, DateTime returnTime, decimal tariff)
        {
            var price = RentPriceTool.CalculatePrice(rentTime, returnTime);
            var expectedPrice = (int)Math.Round((decimal) (returnTime - rentTime).TotalSeconds * tariff);
            Assert.AreEqual(expectedPrice, price);
        }
    }
}
