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

using easyShot.ViewModels;

namespace easyShot
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        SettingViewModel setting;
        CloudViewModel cloud;
        public MainWindow()
        {
            InitializeComponent();
            setting = new SettingViewModel();
            cloud = new CloudViewModel();
            DataContext = setting;
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            DataContext = setting;
        }

        private void Cloud_Click(object sender, RoutedEventArgs e)
        {
            DataContext = cloud;
        }
    }
}
