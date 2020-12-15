﻿using System.Windows;
using System;
using easyShot.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Input;

namespace easyShot
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 

    // 图片类，用于展示图片
    public class Photo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string fullPath;

        //图片的路径
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


    // 界面文本框的内容
    public class TextShow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string account, password, address;
        private class Hide
        {
            public ShotMode hide;
            public Hide(ShotMode hide)
            {
                this.hide = hide;
            }
            public bool ifHide()
            {
                return hide == ShotMode.ShotSquare ? true : false;
            }
        }
        Hide hide;

        public TextShow(string account, string password, string address, ShotMode hide)
        {
            this.account = account;
            this.password = password;
            this.address = address;
            this.hide = new Hide(hide);
        }

        //账号
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

        //密码
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

        //服务器地址
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
        private Setting setting;//设置窗口
        public BindingList<Photo> photos = new BindingList<Photo>();//图片列表
        public string photosPath;//图片文件夹路径
        public TextShow textShow;
        private ConfigManager serverData;//配置文件数据
        private string photoName;
        private int counter;

        public MainWindow()
        {
            InitializeComponent();
            dataInit();
            photosInit();
            KeyDown += FullKeyDown;
            KeyDown += RectKeyDown;
            KeyDown += WindowKeyDown;
            //try
            //{
            //    InitializeComponent();
            //    dataInit();
            //    photosInit();
            //    KeyDown += FullKeyDown;
            //    KeyDown += RectKeyDown;
            //    KeyDown += WindowKeyDown;
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //}
        }

        //所有数据初始化
        private void dataInit()
        {
            serverData = new ConfigManager();
            serverData.getShotMode();
            photosPath = "\\images";
            counter = int.Parse(serverData.getCounter());
        }


        /// <summary>
        /// 以下为图片相关函数
        /// </summary>


        //得到所有图片的路径
        private void getAllImagePath()
        {
            DirectoryInfo di = new DirectoryInfo(photosPath);
            if (!di.Exists)
            {
                Directory.CreateDirectory(photosPath);
            }
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

        //检测图片名称是否重合
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

        //图片列表初始化
        private void photosInit()
        {
            setPhotoPath();
            DirectoryInfo di = new DirectoryInfo(photosPath);
            if (!di.Exists)
            {
                Directory.CreateDirectory(photosPath);
            }
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

        //更新图片列表
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



        /// <summary>
        /// 以下为云端相关函数
        /// </summary>
        /// 



        //云端连接测试
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("连接成功");
        }

        private void Sync_Click(object sender, RoutedEventArgs e)
        {
            Cloud cloud = new Cloud();
            try
            {
                cloud.upLoadNewLocalFile();
                cloud.downLoadNewCloudFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveImg(System.Drawing.Image image)
        {
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(image);
            try
            {
                bitmap.Save(photosPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //打开设置界面
        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            setting = new Setting();
            setting.ShowDialog();
            updatePhotoes();
        }



        /// <summary>
        /// /以下为截图相关函数
        /// </summary>
        /// 


        /// 快捷键
        /// 全屏截图快捷键
        private void FullKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.F)
            {
                fullShot();

            }
        }

        private void RectKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.R)
            {
                openShot("field");
            }
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.W)
            {
                openShot("window");
            }
        }

        //打开shot界面
        private void openShot(string kind)
        {
            CaptureWindow captureWindow = new CaptureWindow();
            photoName = "\\" + counter.ToString() + ".jpg";
            counter += 1;
            Shot shot = new Shot(captureWindow.GetPic_Desktop(), photosPath + photoName, kind);
            shot.Topmost = true;
            shot.WindowStyle = System.Windows.WindowStyle.None;
            shot.WindowState = System.Windows.WindowState.Maximized;
            shot.ShowDialog();
            updatePhotoes();
            serverData.setCounter(counter.ToString());
        }
        //全屏截图
        private void fullShot()
        {
            CaptureWindow captureWindow = new CaptureWindow();
            photoName = "\\" + counter.ToString() + ".jpg";
            counter += 1;
            try
            {
                captureWindow.GetPic_Desktop().Save(photosPath + photoName);

            }
            catch (Exception e)
            {
                fullShot();
            }
            updatePhotoes();
            serverData.setCounter(counter.ToString());
        }

        private void FullShot_Click(object sender, RoutedEventArgs e)
        {
            fullShot();
        }
        //区域截图
        private void FieldShot_Click(object sender, RoutedEventArgs e)
        {
            openShot("field");
        }
        //窗口截图
        private void Window_Click(object sender, RoutedEventArgs e)
        {
            openShot("window");
        }
    }
}
