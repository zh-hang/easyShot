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
        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        public Shot(System.Drawing.Image image)
        {
            var bitmap = new System.Drawing.Bitmap(image);
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(),IntPtr.Zero,Int32Rect.Empty,BitmapSizeOptions.FromEmptyOptions());
            bitmap.Dispose();
            Background = new ImageBrush(bitmapSource);
            InitializeComponent();
            MousePos mousePos = new MousePos();
            mousePos.MouseClickEvent += mousePos.mouseDown;
            mousePos.MouseMoveEvent += mousePos.mouseMove;
            mousePos.MouseClickEvent += mousePos.mouseUp;
            CaptureWindow captureWindow = new CaptureWindow();

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

}
