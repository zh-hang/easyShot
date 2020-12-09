
//simple is perfect!
namespace EasyShot
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

        private System.Xml.XmlDocument configXmlFile;
        private System.Xml.XmlNode root;
        private string shotFilePath;
        private string configXmlFilePath;
        private StartMode startMode;
        private ShotMode shotMode;
        private string serverAddress;
        private string serverAccount;
        private string serverPassword;

        public ConfigManager()
        {
            loadconfigXmlFilePath();
            //��ʧ���쳣
            this.configXmlFile = new System.Xml.XmlDocument();
            this.configXmlFile.Load(this.configXmlFilePath);
            loadShotFilePath();
            //loadStartMode();
            //loadShotMode();
            this.shotMode = EasyShot.ShotMode.ShotSquare;
        }

        public void loadconfigXmlFilePath()
        {
            this.configXmlFilePath = @"C:\Users\liaoj\source\repos\shot\easyShot\easyShot\Config.xml";

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
        }
        public void loadStartMode()
        {
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(startmodelabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;
            string temp = element.GetAttribute("content").ToString();
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
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(shotfilepathlabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;
            
            if (NewStartMode == EasyShot.StartMode.StartAutomaticallty)
                element.SetAttribute("content", ConfigManager.startautomaticallty);
            if (NewStartMode == EasyShot.StartMode.StartManually)
                element.SetAttribute("content", ConfigManager.startmanually);
            else
                element.SetAttribute("content", ConfigManager.startmanually);
        }
        public void loadShotMode()
        {
            System.Xml.XmlNode node = this.configXmlFile.SelectSingleNode(shotmodelabel);
            if (node == null) throw new System.Exception("the Configure file is illegal!");
            System.Xml.XmlElement element = (System.Xml.XmlElement)node;
            string temp = element.GetAttribute("content").ToString();
            if (temp == ConfigManager.startautomaticallty)
                this.startMode = EasyShot.StartMode.StartAutomaticallty;
            if (temp == ConfigManager.startmanually)
                this.startMode = EasyShot.StartMode.StartManually;
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

            if (newShotMode == EasyShot.ShotMode.ShotSquare)
                element.SetAttribute("content", ConfigManager.shotsquare);
            if (newShotMode == EasyShot.ShotMode.ShotWindow)
                element.SetAttribute("content", ConfigManager.shotwindow);
            else
                element.SetAttribute("content", ConfigManager.shotsquare);
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
        }
    }
}
