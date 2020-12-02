using System.Windows;

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
