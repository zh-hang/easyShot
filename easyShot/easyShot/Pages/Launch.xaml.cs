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

        private class startCheck
        {
            private StartMode start;
            public StartMode Start { set { start = value; } get { return start; } }


            public startCheck(ConfigManager config)
            {
                start = config.getStartMode();
            }

            public bool ifStart()
            {
                if (start == StartMode.StartManually)
                    return false;
                return true;
            }
        }
        private ConfigManager configManager;

        public Launch()
        {
            InitializeComponent();
            configManager = new ConfigManager();
            startCheck startCheck = new startCheck(configManager);
            startModeSet.IsChecked = startCheck.ifStart();
        }

        

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
