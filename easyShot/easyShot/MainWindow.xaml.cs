using System.Windows;
using System;
using easyShot.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Controls;
using System.ComponentModel;

namespace easyShot
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 

    public class Photo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string fullPath;
        public string FullPath
        {
            get { return fullPath; }
            set
            {
                fullPath = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("FullPath"));
                }
            }
        }
    }

    public class TextShow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string account, password, address;
        public string Account
        {
            get { return account; }
            set
            {
                account = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Path"));
                }
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Password"));
                }
            }
        }
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Address"));
                }
            }
        }
    }


    public partial class MainWindow : Window
    {
        private Setting setting;
        public BindingList<Photo> photos = new BindingList<Photo>();
        public string photos_path;
        public TextShow textShow;

        public MainWindow()
        {
            photos_path = "\\images";
            InitializeComponent();
            textShow = new TextShow();
            textShow.Account = "123456";
            textShow.Address = "123456";
            textShow.Password = "123456";
            photosInit();
        }

        //private void Cloud_Click(object sender, RoutedEventArgs e)
        //{
        //    DataContext = cloud;
        //}

        

        private void getAllImagePath()
        {
            DirectoryInfo di = new DirectoryInfo(photos_path);
            FileInfo[] files = di.GetFiles("*.*", SearchOption.AllDirectories);
            if (files != null && files.Length > 0)
            {
                foreach (var file in files)
                {
                    if ((file.Extension == (".jpg") ||
                        file.Extension == (".png") ||
                        file.Extension == (".bmp") ||
                        file.Extension == (".gif")) && !photosContain(file.FullName))
                    {
                        photos.Insert(0, new Photo()
                        {
                            FullPath = file.FullName
                        });
                        //photos.Add();
                    }
                }
            }
        }

        private bool photosContain(string fullName)
        {
            foreach (var photo in photos)
            {
                if (photo.FullPath == fullName)
                    return true;
            }
            return false;
        }

        private void setPhotoPath()
        {

        }

        private void photosInit()
        {
            setPhotoPath();
            DirectoryInfo di = new DirectoryInfo(photos_path);
            FileInfo[] files = di.GetFiles("*.*", SearchOption.AllDirectories);
            if (files != null && files.Length > 0)
            {
                foreach (var file in files)
                {
                    if ((file.Extension == (".jpg") ||
                        file.Extension == (".png") ||
                        file.Extension == (".bmp") ||
                        file.Extension == (".gif")))
                    {
                        photos.Add(new Photo()
                        {
                            FullPath = file.FullName
                        });
                        //photos.Add();
                    }
                }
            }
            try
            {
                this.lstImg.ItemsSource = photos;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            };
        }

        private void updatePhotoes()
        {
            setPhotoPath();
            getAllImagePath();
            try
            {
                this.lstImg.ItemsSource = photos;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            };
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("连接成功");
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            setting = new Setting();
            setting.ShowDialog();
            updatePhotoes();
        }

        private void FullShot_Click(object sender, RoutedEventArgs e)
        {
            //CaptureWindow captureWindow = new CaptureWindow();

        }

        private void FieldShot_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Circle_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
