using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class ReservationID
    {
        [TestMethod]
        public void TestMethod1()
        {
            DateTime dateTime = new DateTime();
            int year = dateTime.Year;
            int doy = dateTime.DayOfYear;
            int ms = dateTime.Millisecond;
            int reservationID = int.Parse(year.ToString() + doy.ToString() + ms.ToString());
            Debug.WriteLine(reservationID);
        }
    }
}
