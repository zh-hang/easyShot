

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EasyShot
{
    //用于操作App.config文件，读取和修改其中配置属性
    enum StartMode
    {
        StartAutomaticallty,
        StartManually
    }
    class ConfigManager
    {
        private StartMode startMode;
        private const string startautomaticallty = "startautomaticallty";
        private const string startmanually = "startmanually";
        private const string shotfilepathlabel = "shotfilepath";
        private const string startmodelabel = "startmodellabel";
        private string ShotFilePath;
        private int StartMode;
        private Configuration config;
        public ConfigManager()
        {
            this.config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            loadShotFilePath();
            loadStartMode();
        }
        public void loadShotFilePath()
        {
            this.ShotFilePath = config.AppSettings.Settings[shotfilepathlabel].Value;
        }
        public string getShotFilePath() 
        {
            return this.ShotFilePath;
        }
        public void setShotFilePath(string newFilePath)
        {
            this.ShotFilePath = newFilePath;
            this.config.AppSettings.Settings[shotfilepathlabel].Value = newFilePath;
        }
        public void loadStartMode()
        {
            string temp = config.AppSettings.Settings[startmodelabel].Value;
            if (temp == ConfigManager.startautomaticallty)
                this.startMode = EasyShot.StartMode.StartAutomaticallty;
            if (temp == ConfigManager.startmanually)
                this.startMode = EasyShot.StartMode.StartManually;
        }
        public StartMode getStartMode()
        {
            return this.startMode;
        }
        public void setStartMode(StartMode NewStartMode)
        {
            this.startMode = NewStartMode;
            if(NewStartMode== EasyShot.StartMode.StartAutomaticallty)
                this.config.AppSettings.Settings[startmodelabel].Value = ConfigManager.startautomaticallty;
            if (NewStartMode == EasyShot.StartMode.StartManually)
                this.config.AppSettings.Settings[startmodelabel].Value = ConfigManager.startmanually;
        }
    }
}
