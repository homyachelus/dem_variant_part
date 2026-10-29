using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoeStore.Logic;

namespace ShoeStore.Tests
{
    [TestClass]
    public class ProductCalculatorTests
    {
        // Случай 1: заказов в предыдущем месяце нет - цена со скидкой
        [TestMethod]
        public void CalculateDiscountedPrice_NoOrdersInPreviousMonth_ReturnsPriceWithDiscount()
        {
            decimal price = 2000m;
            List<DateTime> orderDates = new List<DateTime>();
            DateTime calculationDate = new DateTime(2027, 6, 15);

            decimal result = ProductCalculator.CalculateDiscountedPrice(price, orderDates, calculationDate);

            Assert.AreEqual(1500m, result);
        }

        // Случай 2: есть заказ в предыдущем месяце - цена без скидки
        [TestMethod]
        public void CalculateDiscountedPrice_OrderInPreviousMonth_ReturnsPriceWithoutDiscount()
        {
            decimal price = 2000m;
            List<DateTime> orderDates = new List<DateTime> { new DateTime(2027, 5, 10) };
            DateTime calculationDate = new DateTime(2027, 6, 15);

            decimal result = ProductCalculator.CalculateDiscountedPrice(price, orderDates, calculationDate);

            Assert.AreEqual(2000m, result);
        }

        // Случай 3: граничное значение - «мало»
        [TestMethod]
        public void GetQuantityLabel_FivePairs_ReturnsFew()
        {
            string result = ProductCalculator.GetQuantityLabel(5);

            Assert.AreEqual("мало", result);
        }

        // Случай 4: больше порогового - «много»
        [TestMethod]
        public void GetQuantityLabel_SixPairs_ReturnsMany()
        {
            string result = ProductCalculator.GetQuantityLabel(6);

            Assert.AreEqual("много", result);
        }
    }
}
