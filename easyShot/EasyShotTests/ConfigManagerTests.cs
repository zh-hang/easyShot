using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyShot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShot.Tests
{
    [TestClass()]
    public class ConfigManagerTests
    {
        [TestMethod()]
        public void ConfigManagerTest()
        {
            

        }

        [TestMethod()]
        public void unitTestTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void loadShotFilePathTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void getShotFilePathTest()
        {
            ConfigManager configManager = new ConfigManager();
            EasyShot.ShotMode shotMode = configManager.getShotMode();
            //System.Console.WriteLine("the shot mode is " + shotMode);

        }

        [TestMethod()]
        public void setShotFilePathTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void loadStartModeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void getStartModeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void setStartModeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void loadShotModeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void getShotModeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void setShotModeTest()
        {
            Assert.Fail();
        }
    }
}