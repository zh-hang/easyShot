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
using System.Windows.Shapes;

namespace easyShot
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    /// 

    public partial class Setting : Window
    {

        private string path;

        public Setting()
        {
            InitializeComponent();
            this.frmMain.Navigate(new Uri("./Pages/General.xaml", UriKind.Relative));
        }

        public void setPath(string path)
        {
            this.path = path;
        }

        private void BtnNav_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            this.frmMain.Navigate(new Uri("./Pages/" + btn.Tag.ToString() + ".xaml", UriKind.Relative));
        }

    }
}
