using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace easyShot
{
    /// <summary>
    /// Shot.xaml 的交互逻辑
    /// </summary>
    /// 

    public partial class Shot : Window
    {
        private System.Drawing.Point _downPoint, _upPoint;
        private Point rectPoint;
        private string path;
        private bool ifShot;
        private System.Drawing.Image img;
        private int i = 0;
        private IntPtr hWnd;
        private string kind;
        private bool _started;


        /// <summary>
        /// 以下为几个接口的调用
        /// </summary>
        ///


        //设置窗口优先级
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
        //获取窗口句柄
        [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]
        public static extern IntPtr WindowFromPoint(int xPoint, int yPoint);
        //获取窗口的范围
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);


        /// <summary>
        /// 以下为各种事件
        /// </summary>
        ///



        //鼠标双击事件
        private void MainWindow_Double_MouseDown(object sender, MouseButtonEventArgs e)
        {
            i += 1;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += (s, e1) => { timer.IsEnabled = false; i = 0; };
            timer.IsEnabled = true;
            if (i % 2 == 0)
            {
                timer.IsEnabled = false;
                i = 0;
                savePhoto();
            }
        }

        //鼠标按下事件
        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _started = true;
            rectPoint = e.GetPosition(shotScreen);
            _downPoint = System.Windows.Forms.Control.MousePosition;
        }

        //鼠标释放事件
        private void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _started = false;
        }

        //鼠标移动事件
        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (_started)
            {
                _upPoint = System.Windows.Forms.Control.MousePosition;
                var point = e.GetPosition(shotScreen);
                var rect = new Rect(rectPoint, point);
                Rectangle.Margin = new Thickness(rect.Left, rect.Top, 0, 0);
                Rectangle.Width = rect.Width;
                Rectangle.Height = rect.Height;
                ifShot = true;
            }
        }

        //鼠标选择窗口事件
        private void MainWindow_MouseDown_Hide(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
            Thread.Sleep(50);
            _downPoint = System.Windows.Forms.Control.MousePosition;
            hWnd = WindowFromPoint(_downPoint.X, _downPoint.Y);
            SwitchToThisWindow(hWnd, true);
            Thread.Sleep(50);
            CaptureWindow captureWindow = new CaptureWindow();
            img = captureWindow.GetPic_Desktop();
            setBackground();
            WindowState = WindowState.Maximized;
            ifShot = true;
        }

        //按下Esc键事件
        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }


        public Shot(System.Drawing.Image image, string imgPath, string kind)
        {
            img = image;
            ifShot = false;
            path = imgPath;
            this.kind = kind;
            setBackground();
            InitializeComponent();
            if (kind == "field")
            {
                fieldShot();
            }
            else if (kind == "window")
            {
                windowShot();
            }
            else
            {
                Close();
            }
        }

        //设置背景图
        private void setBackground()
        {
            var bitmap = new System.Drawing.Bitmap(img);
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            bitmap.Dispose();
            Background = new ImageBrush(bitmapSource);
        }


        /// <summary>
        /// 以下为截图相关函数
        /// </summary>
        /// 


        //区域截图
        private void fieldShot()
        {
            MouseDown += MainWindow_MouseDown;
            MouseMove += MainWindow_MouseMove;
            MouseUp += MainWindow_MouseUp;
            MouseDown += MainWindow_Double_MouseDown;
        }

        //窗口截图
        private void windowShot()
        {
            MouseDown += MainWindow_MouseDown_Hide;
            MouseDown += MainWindow_Double_MouseDown;
        }

        private void EscKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void savePhoto()
        {
            if (ifShot)
            {
                CaptureWindow captureWindow = new CaptureWindow();
                if (kind == "field")
                {
                    Thread.Sleep(50);
                    System.Console.WriteLine(_downPoint);
                    System.Console.WriteLine(_upPoint);
                    captureWindow.GetPic_Retangle(_downPoint, _upPoint).Save(path);

                }
                else if (kind == "window")
                {
                    captureWindow.GetPic_ByHwnd(hWnd).Save(path);
                }
            }
            else
            {
                img.Save(path);
            }
            Close();
        }


        /// <summary>
        /// 以下为按钮点击事件
        /// </summary>
        /// 


        //取消按钮
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //保存按钮
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            savePhoto();
        }
    }
}