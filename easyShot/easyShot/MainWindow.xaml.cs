using System.Windows;

using easyShot.ViewModels;

namespace easyShot
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new SettingViewModel();
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            
         

        }

        private void Cloud_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new CloudViewModel();
        }
    }
}
