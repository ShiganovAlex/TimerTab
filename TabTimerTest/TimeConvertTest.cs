using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimerTab.ViewModel;

namespace TabTimerTest
{
    [TestClass]
    public class TimeConvertTest0
    {
        [TestMethod]
        public void MilisecondsConvert()
        {
            TimeControlViewModel timeControlViewModel = new TimeControlViewModel();
            timeControlViewModel.Time = 5402123;
            Assert.AreEqual(123, timeControlViewModel.Milliseconds);         
        }

        [TestMethod]
        public void SecondsConvert()
        {
            TimeControlViewModel timeControlViewModel = new TimeControlViewModel();
            timeControlViewModel.Time = 5402123;
            Assert.AreEqual(2, timeControlViewModel.Seconds);         
        }

        [TestMethod]
        public void MinutesConvert()
        {
            TimeControlViewModel timeControlViewModel = new TimeControlViewModel();
            timeControlViewModel.Time = 5402123;
            Assert.AreEqual(30, timeControlViewModel.Minutes);           
        }

        [TestMethod]
        public void HoursConvert()
        {
            TimeControlViewModel timeControlViewModel = new TimeControlViewModel();
            timeControlViewModel.Time = 5402123;
            Assert.AreEqual(1, timeControlViewModel.Hours);
        }
    }
}
