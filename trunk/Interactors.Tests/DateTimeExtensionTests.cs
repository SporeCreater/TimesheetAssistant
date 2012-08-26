using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Interactors.Tests
{
    [TestClass]
    public class DateTimeExtensionTests
    {
        [TestMethod]
        public void yields_next_saturday_when_today_in_the_middle_of_the_week()
        {
            var today = new DateTime(2012, 8, 21);

            Assert.AreEqual(new DateTime(2012, 8, 25), today.CalculateNextSaturday());
        }

        [TestMethod]
        public void yields_next_saturday_when_today_is_previous_sunday()
        {
            var today = new DateTime(2012, 8, 12);

            Assert.AreEqual(new DateTime(2012, 8, 18), today.CalculateNextSaturday());
        }

        [TestMethod]
        public void yields_same_day_when_today_is_saturday()
        {
            var today = new DateTime(2012, 8, 25);

            Assert.AreEqual(today, today.CalculateNextSaturday());
        }

        [TestMethod]
        public void next_saturday_can_be_in_the_following_month()
        {
            var today = new DateTime(2012, 8, 29);

            Assert.AreEqual(new DateTime(2012, 9, 1), today.CalculateNextSaturday());
        }

    }
}
