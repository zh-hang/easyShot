using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace easyShot.Pages
{
    /// <summary>
    /// Launch.xaml 的交互逻辑
    /// </summary>
    /// 



    public partial class Launch : Page
    {

        private class StartCheck
        {
            public StartMode Start { set; get; }


            public StartCheck(ConfigManager config)
            {
                Start = config.getStartMode();
            }

            public bool ifStart()
            {
                if (Start == StartMode.StartManually)
                    return false;
                return true;
            }

            public void setStartMode(bool ifStart)
            {
                if (ifStart)
                {
                    Start = StartMode.StartAutomaticallty;
                    return;
                }
                Start = StartMode.StartManually;
            }
        }


        private ConfigManager configManager;
        private StartCheck startCheck;


        public Launch()
        {
            InitializeComponent();
            configManager = new ConfigManager();
            startCheck = new StartCheck(configManager);
            startModeSet.IsChecked = startCheck.ifStart();
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            startCheck.setStartMode((bool)startModeSet.IsChecked);
            configManager.setStartMode(startCheck.Start);
            autoStart();
        }

        //设置开机自启
        private void autoStart()
        {
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);//打开注册表项
                if (key == null)//如果该项不存在的话，则创建该项
                {
                    key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                }
                if (startCheck.ifStart() == true)
                    key.SetValue("easyShot", AppDomain.CurrentDomain);//设置为开机启动
                else
                    key.DeleteValue("easyShot");//取消开机启动
                key.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
