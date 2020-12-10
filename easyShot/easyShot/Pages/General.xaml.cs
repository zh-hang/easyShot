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

    public class Hide
    {
        public WindowHideMode hide;
        public Hide(WindowHideMode hide)
        {
            this.hide = hide;
        }
        public void setHide(bool isHide)
        {
            hide =isHide? WindowHideMode.Hide:WindowHideMode.NotHide;
        }
        public bool ifHide()
        {
            return hide == WindowHideMode.Hide ? true : false;
        }
    }

    public class GeneralData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string path;
        public Hide hide;
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

        public GeneralData(string path,WindowHideMode hide)
        {
            this.path = path;
            this.hide = new Hide(hide);
        }
    }

    public partial class General : Page
    {
        
        private ConfigManager configManager;
        public GeneralData generalData;
        public string test;
        public General()
        {
            InitializeComponent();
            configManager = new ConfigManager();
            generalData = new GeneralData(configManager.getshotfilepath(),configManager.getWindowHideMode());
            generalData.Path = configManager.getshotfilepath();
            PathBox.Text = generalData.Path;
            HideCheck.IsChecked = generalData.hide.ifHide();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            generalData.Path = PathBox.Text;
            generalData.hide.setHide((bool)HideCheck.IsChecked);
            configManager.setShotFilePath(generalData.Path);
            configManager.setWindowHideMode(generalData.hide.hide);
            System.Console.WriteLine(configManager.getWindowHideMode().ToString());
            ConfigManager test = new ConfigManager();
            System.Console.WriteLine(test.getWindowHideMode().ToString());
            MessageBox.Show("保存成功!");
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
