using System.Windows;
using System;
using easyShot.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Controls;

namespace easyShot
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 

    public class Photo
    {
        public string FullPath { get; set; }
    }


    public partial class MainWindow : Window
    {
        SettingViewModel setting;
        CloudViewModel cloud;
        public List<Photo> photos = new List<Photo>();
        public string photos_path;
        public MainWindow()
        {
            photos_path = "\\images";
            getAllImagePath(photos_path);
            setting = new SettingViewModel();
            cloud = new CloudViewModel();
            InitializeComponent();
            DataContext = setting;
            try
            {
                this.lstImg.ItemsSource = photos;
            }
            catch (Exception e){ MessageBox.Show(e.Message); };

        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            DataContext = setting;
        }

        private void Cloud_Click(object sender, RoutedEventArgs e)
        {
            DataContext = cloud;
        }

        public void getAllImagePath(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] files = di.GetFiles("*.*", SearchOption.AllDirectories);
            if (files != null && files.Length > 0)
            {
                foreach (var file in files)
                {
                    if (file.Extension == (".jpg") ||
                        file.Extension == (".png") ||
                        file.Extension == (".bmp") ||
                        file.Extension == (".gif"))
                    {
                        photos.Add(new Photo()
                        {
                            FullPath = file.FullName
                        });
                    }
                }
            }
        }

    }

    
    
}
