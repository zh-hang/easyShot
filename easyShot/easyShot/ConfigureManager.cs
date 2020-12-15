
//simple is perfect!
namespace easyShot
{
    //���ڲ���config.xml�ļ�����ȡ���޸�������������
    public enum StartMode
    {
        StartAutomaticallty,
        StartManually
    }
    public enum ShotMode
    {
        ShotWindow,
        ShotSquare
    }
    //Ϊ�˱��ڽ��е�Ԫ���ԣ���ʱ����Щ����Ϊ������
    public class ConfigManager
    {
        /*
        ��Ҫ�����������ԣ�
        �ļ�·��
        ��ͼģʽ
        ����ģʽ
        ��������ַ
        �˻�
        ����
        ��������Կ
        */


        //��xml����ת��Ϊ������ʹ�õ�ö�ٱ���
        private const string startautomaticallty = "startautomaticallty";
        private const string startmanually = "startmanually";
        private const string shotsquare = "shotsquare";
        private const string shotwindow = "shotwindow";

        //label��ʾ��Ӧ����xml�е�xpath
        private const string xmlfilerootlabellabel = "configuration";
        private const string shotfilepathlabel = "//configuration/shotfilepath";
        private const string startmodelabel = "//configuration/startmode";
        private const string shotmodelabel = "//configuration/shotmode";
        private const string serveraddresslabel = "//configuration/serveraddress";
        private const string serveraccountlabel = "//configuration/serveraccount";
        private const string serverpasswordlabel = "//configuration/serverpassword";
        private const string counterlabel = "//configuration/counter";

        private System.Xml.XmlDocument configXmlFile;
        private System.Xml.XmlNode root;
        private string shotFilePath;
        private string configXmlFilePath;
        private StartMode startMode;
        private ShotMode shotMode;
        private string serverAddress;
        private string serverAccount;
        private string serverPassword;
        private string counter;

        public ConfigManager()
        {
            loadconfigXmlFilePath();
            //��ʧ���쳣
            this.configXmlFile = new System.Xml.XmlDocument();
            this.configXmlFile.Load(this.configXmlFilePath);
            loadShotFilePath();
            loadCounter();
            //loadStartMode();
            //loadShotMode();
            this.shotMode = easyShot.ShotMode.ShotSquare;
        }

        public void loadconfigXmlFilePath()
        {
            this.configXmlFilePath = @".\Config.xml";

        }
        public void loadShotFilePath()
        {
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(shotfilepathlabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;
            this.shotFilePath = element.GetAttribute("content").ToString();
            //this.shotFilePath=element.Value;
        }
        public string getshotfilepath()
        {
            return this.shotFilePath;
        }
        public void setShotFilePath(string newFilePath)
        {
            this.shotFilePath = newFilePath;
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(shotfilepathlabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;
            element.SetAttribute("content", newFilePath);
            configXmlFile.Save(configXmlFilePath);
        }
        public void loadStartMode()
        {
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(startmodelabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;
            string temp = element.GetAttribute("content").ToString();
            if (temp == ConfigManager.startautomaticallty)
                this.startMode = easyShot.StartMode.StartAutomaticallty;
            if (temp == ConfigManager.startmanually)
                this.startMode = easyShot.StartMode.StartManually;
        }
        public StartMode getStartMode()
        {
            return this.startMode;
        }
        public void setStartMode(StartMode NewStartMode)
        {
            this.startMode = NewStartMode;
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(shotfilepathlabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;

            if (NewStartMode == easyShot.StartMode.StartAutomaticallty)
                element.SetAttribute("content", ConfigManager.startautomaticallty);
            if (NewStartMode == easyShot.StartMode.StartManually)
                element.SetAttribute("content", ConfigManager.startmanually);
            else
                element.SetAttribute("content", ConfigManager.startmanually);
            configXmlFile.Save(configXmlFilePath);
        }
        public void loadShotMode()
        {
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(shotmodelabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;
            string temp = element.GetAttribute("content").ToString();
            if (temp == ConfigManager.startautomaticallty)
                this.startMode = easyShot.StartMode.StartAutomaticallty;
            if (temp == ConfigManager.startmanually)
                this.startMode = easyShot.StartMode.StartManually;
        }
        public ShotMode getShotMode()
        {
            return this.shotMode;
        }
        public void setShotMode(ShotMode newShotMode)
        {
            this.shotMode = newShotMode;
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(shotfilepathlabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;

            if (newShotMode == easyShot.ShotMode.ShotSquare)
                element.SetAttribute("content", ConfigManager.shotsquare);
            if (newShotMode == easyShot.ShotMode.ShotWindow)
                element.SetAttribute("content", ConfigManager.shotwindow);
            else
                element.SetAttribute("content", ConfigManager.shotsquare);
            configXmlFile.Save(configXmlFilePath);
        }
        public void loadServerAddress()
        {
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(serveraddresslabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;
            this.serverAddress = element.GetAttribute("content").ToString();
        }
        public string getServerAddress()
        {
            return this.serverAddress;
        }
        public void setServerAddress(string newServerAddress)
        {
            this.serverAddress = newServerAddress;
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(serveraddresslabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;
            element.SetAttribute("content", newServerAddress);
            configXmlFile.Save(configXmlFilePath);
        }
        public void loadServerAccount()
        {
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(serveraccountlabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;
            this.serverAccount = element.GetAttribute("content").ToString();
        }
        public string getServerAccount()
        {
            return this.serverAccount;
        }
        public void setServerAccount(string newServerAccount)
        {
            this.serverAccount = newServerAccount;
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(serveraccountlabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;
            element.SetAttribute("content", newServerAccount);
            configXmlFile.Save(configXmlFilePath);
        }
        public void loadServerPassword()
        {
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(serverpasswordlabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;
            this.serverPassword = element.GetAttribute("content").ToString();
        }
        public string getServerPassword()
        {
            return this.serverPassword;
        }
        public void setServerPassword(string newServerpassword)
        {
            this.serverPassword = newServerpassword;
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(serverpasswordlabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;
            element.SetAttribute("content", newServerpassword);
            configXmlFile.Save(configXmlFilePath);
        }
        public void loadCounter()
        {
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(counterlabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;
            this.counter = element.GetAttribute("content").ToString();
        }
        public string getCounter()
        {
            return this.counter;
        }
        public void setCounter(string newCounter)
        {
            this.counter = newCounter;
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(counterlabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;
            element.SetAttribute("content", newCounter.ToString());
            configXmlFile.Save(configXmlFilePath);
        }
    }
}
