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
    public partial class MainWindow : Window
    {
        SettingViewModel setting;
        CloudViewModel cloud;
        public PhotoCollection photos;
        public string photos_path;
        public MainWindow()
        {
            photos_path = "E:\\photo\\somePhotoes";
            photos = new PhotoCollection(new DirectoryInfo(photos_path));
            Binding binding = new Binding();
            binding.Source = photos;
            binding.Path = new PropertyPath("Directory");
            binding.Mode = BindingMode.TwoWay;
            InitializeComponent();
            setting = new SettingViewModel();
            cloud = new CloudViewModel();
            DataContext = setting;
            //BindingOperations.SetBinding(this.test, TextBlock.TextProperty, binding);
            //BindingOperations.SetBinding(this.lstImg, ListBox.ItemsSourceProperty, binding);
            this.lstImg.ItemsSource = photos;
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

    public class Photo
    {
        private readonly Uri source;

        public string path;
        public BitmapFrame image;

        public Photo(string path)
        {
            this.path = path;
            this.source = new Uri(path);
            this.image = BitmapFrame.Create(source);
        }

        public override string ToString()
        {
            return source.ToString();
        }
    }

    public class PhotoCollection : ObservableCollection<Photo>
    {
        private DirectoryInfo directory;
        public string Path
        {
            set
            {
                directory = new DirectoryInfo(value);
            }
            get { return directory.FullName; }
        }

        public DirectoryInfo Directory
        {
            set
            {
                directory = value;
                Update();
            }
            get
            {
                return directory;
            }
        }

        public PhotoCollection()
        {

        }
        public PhotoCollection(string Path)
        {
            this.directory = new DirectoryInfo(Path);
        }

        public PhotoCollection(DirectoryInfo directory)
        {
            this.directory = directory;
            Update();
        }

        private void Update()
        {
            Clear();
            try
            {
                foreach (var f in directory.GetFiles("*.*"))
                    if (f.Extension == ".jpg" || f.Extension == ".png")
                    {
                        Add(new Photo(f.FullName));
                    }
            }
            catch(DirectoryNotFoundException)
            {
                MessageBox.Show("No Such Directory");
            }
        }
    }
}
