using System;
using System.Collections.Generic;
using System.Linq;
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

namespace easyShot
{
    /// <summary>
    /// Shot.xaml 的交互逻辑
    /// </summary>
    /// 

    public partial class Shot : Window
    {
        private Point _downPoint, _upPoint;
        private string path;
        private bool ifShot;

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _started = true;

            _downPoint = e.GetPosition(shotScreen);
        }

        private bool _started;


        private void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _started = false;
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (_started)
            {
                _upPoint = e.GetPosition(shotScreen);
                var rect = new Rect(_downPoint, _upPoint);
                Rectangle.Margin = new Thickness(rect.Left, rect.Top, 0, 0);
                Rectangle.Width = rect.Width;
                Rectangle.Height = rect.Height;
                ifShot = true;
            }
        }

        public Shot(System.Drawing.Image image, string imgPath)
        {
            ifShot = false;
            path = imgPath;
            var bitmap = new System.Drawing.Bitmap(image);
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            bitmap.Dispose();
            Background = new ImageBrush(bitmapSource);
            InitializeComponent();
            MouseDown += MainWindow_MouseDown;
            MouseMove += MainWindow_MouseMove;
            MouseUp += MainWindow_MouseUp;
        }

        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ifShot)
            {
                CaptureWindow captureWindow = new CaptureWindow();
                captureWindow.GetPic_Retangle(_downPoint,_upPoint).Save(path);
            }
            Close();
        }


        //    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        //    {
        //        IsLeftMouseDown = true;
        //        if (th != null)
        //        {
        //            if (th.ThreadState == ThreadState.Running || th.ThreadState == ThreadState.WaitSleepJoin)
        //                th.Abort();
        //        }
        //        th = new Thread(new ThreadStart(() =>
        //        {

        //            if (IsLeftMouseDown)
        //            {
        //                EntryTouch = true;
        //                IsLeftMouseDown = false;
        //                _downPoint = e.GetPosition(shotScreen);
        //            }

        //            EntryTouch = false;
        //        }));
        //        th.Start();
        //    }

        //    private void DataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //    {
        //        //if (EntryTouch)
        //        //{
        //        //}
        //        //_upPoint = e.GetPosition(shotScreen);

        //        //IsLeftMouseDown = false;
        //    }

    }
}