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
            Assert.Fail();
        }

        [TestMethod()]
        public void setShotFilePathTest()
        {
            string newfilepath = "asdfg";
            ConfigManager configManager = new ConfigManager();
            configManager.setShotFilePath(newfilepath);
            Assert.AreEqual(newfilepath, configManager.getshotfilepath());
        }

        [TestMethod()]
        public void loadStartModeTest()
        {
            ConfigManager configManager = new ConfigManager();
            Assert.AreEqual(EasyShot.StartMode.StartAutomaticallty, configManager.getStartMode());
        }

        [TestMethod()]
        public void loadShotFilePathTest()
        {
            ConfigManager configManager = new ConfigManager();
            Assert.AreEqual("abcd", configManager.getshotfilepath());
        }

        [TestMethod()]
        public void loadShotModeTest()
        {
            ConfigManager configManager = new ConfigManager();
            Assert.AreEqual(EasyShot.ShotMode.ShotSquare, configManager.getShotMode());
        }

        [TestMethod()]
        public void setShotModeTest()
        {
            EasyShot.ShotMode newshotmode = EasyShot.ShotMode.ShotSquare;
            ConfigManager configManager = new ConfigManager();
            configManager.setShotMode(newshotmode);
            Assert.AreEqual(newshotmode, configManager.getShotMode());
        }
    }
}