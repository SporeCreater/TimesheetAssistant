using System;
using FluentAssertions;
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

            today.CalculateNextSaturday().Should().Be(new DateTime(2012, 8, 25));
        }

        [TestMethod]
        public void yields_next_saturday_when_today_is_previous_sunday()
        {
            var today = new DateTime(2012, 8, 12);

            today.CalculateNextSaturday().Should().Be(new DateTime(2012, 8, 18));
        }

        [TestMethod]
        public void yields_same_day_when_today_is_saturday()
        {
            var today = new DateTime(2012, 8, 25);

            today.CalculateNextSaturday().Should().Be(today);
        }

        [TestMethod]
        public void next_saturday_can_be_in_the_following_month()
        {
            var today = new DateTime(2012, 8, 29);

            today.CalculateNextSaturday().Should().Be(new DateTime(2012, 9, 1));
        }

    }
}
