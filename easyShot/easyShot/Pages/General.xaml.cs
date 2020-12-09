using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace easyShot.Pages
{
    /// <summary>
    /// General.xaml 的交互逻辑
    /// </summary>
    /// 

    public class GeneralData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string path, name;
        private bool hide;
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Path"));
                }
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        public bool Hide
        {
            get { return hide; }
            set
            {
                hide = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Hide"));
                }
            }
        }

        public GeneralData()
        {
            path = "123";
            name = "123";
            hide = true;
        }
    }

    public partial class General : Page
    {
        public GeneralData generalData;
        public string test;
        public General()
        {
            test = "321";

            InitializeComponent();
            generalData = new GeneralData();
            PathBox.Text = generalData.Path;
            NameBox.Text = generalData.Name;
            HideCheck.IsChecked = generalData.Hide;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            generalData.Path = PathBox.Text;
            generalData.Name = NameBox.Text;
            generalData.Hide = HideCheck.IsEnabled;
            MessageBox.Show("保存成功!");
            PathBox.Text = generalData.Name;
            NameBox.Text = generalData.Path;
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            generalData.Path = m_Dialog.SelectedPath.Trim();
            PathBox.Text = generalData.Path;
        }
    }
}
