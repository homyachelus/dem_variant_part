using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoeStore.Logic
{
    /// <summary>
    /// Расчет показателей товара для каталога.
    /// </summary>
    public static class ProductCalculator
    {
        public const decimal DiscountPercent = 25m;
        public const int ManyQuantityThreshold = 5;

        /// <summary>
        /// Возвращает цену товара с учетом скидки.
        /// Скидка 25% предоставляется, если в предыдущем календарном месяце
        /// (относительно даты расчета) по товару не было заказов.
        /// </summary>
        /// <param name="price">Цена товара в рублях.</param>
        /// <param name="orderDates">Даты заказов товара (может быть пустым).</param>
        /// <param name="calculationDate">Дата расчета.</param>
        public static decimal CalculateDiscountedPrice(decimal price, IEnumerable<DateTime> orderDates, DateTime calculationDate)
        {
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Цена не может быть отрицательной.");
            }
            if (orderDates == null)
            {
                throw new ArgumentNullException(nameof(orderDates));
            }

            DateTime currentMonthStart = new DateTime(calculationDate.Year, calculationDate.Month, 1);
            DateTime previousMonthStart = currentMonthStart.AddMonths(-1);
            bool hasOrdersInPreviousMonth = orderDates.Any(date => date >= previousMonthStart && date < currentMonthStart);

            if (hasOrdersInPreviousMonth)
            {
                return price;
            }
            return Math.Round(price * (100m - DiscountPercent) / 100m, 2);
        }

        /// <summary>
        /// Возвращает признак количества: «много», если пар больше пяти, иначе «мало».
        /// </summary>
        /// <param name="totalQuantity">Количество пар суммарно по всем размерам.</param>
        public static string GetQuantityLabel(int totalQuantity)
        {
            if (totalQuantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(totalQuantity), "Количество не может быть отрицательным.");
            }
            return totalQuantity > ManyQuantityThreshold ? "много" : "мало";
        }
    }
}
