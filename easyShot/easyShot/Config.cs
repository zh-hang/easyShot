

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
//simple is perfect!
namespace EasyShot
{
    //用于操作App.config文件，读取和修改其中配置属性
    enum StartMode
    {
        StartAutomaticallty,
        StartManually
    }
    enum ShotMode
    {
        ShotWindow,
        ShotSquare
    }
    class ConfigManager
    {
        private const string startautomaticallty = "startautomaticallty";
        private const string startmanually = "startmanually";
        private const string shotsquare = "shotsquare";
        private const string shotwindow = "shotwindow";


        private const string shotfilepathlabel = "shotfilepath";
        private const string startmodelabel = "startmodellabel";
        private const string shotmodelabel = "shootmodellabel";

        private string shotFilePath;
        private StartMode startMode;
        private ShotMode shotMode;
        private Configuration config;
        public ConfigManager()
        {
            this.config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            loadShotFilePath();
            loadStartMode();
            loadShotMode();
        }
        public void loadShotFilePath()
        {
            this.shotFilePath = config.AppSettings.Settings[shotfilepathlabel].Value;
        }
        public string getShotFilePath() 
        {
            return this.shotFilePath;
        }
        public void setShotFilePath(string newFilePath)
        {
            this.shotFilePath = newFilePath;
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
        public void loadShotMode()
        {
            this.shotMode = EasyShot.ShotMode.ShotSquare;
        }
        public ShotMode getShotMode()
        {
            return this.shotMode;
        }
        public void setShotMode(ShotMode newShotMode)
        {
            this.shotMode = newShotMode;
            if (newShotMode == EasyShot.ShotMode.ShotSquare)
                this.config.AppSettings.Settings[shotmodelabel].Value = ConfigManager.shotsquare;
            if (newShotMode == EasyShot.ShotMode.ShotWindow)
                this.config.AppSettings.Settings[shotmodelabel].Value = ConfigManager.shotwindow;
        }
    }
}
