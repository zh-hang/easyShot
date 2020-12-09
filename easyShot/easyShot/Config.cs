
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
//simple is perfect!
namespace EasyShot
{
    //用于操作App.config文件，读取和修改其中配置属性
    enum StartMode
    {
        StartAutomaticallty,
        StartManually
    }
    enum ShotMode
    {
        ShotWindow,
        ShotSquare
    }
    class ConfigManager
    {
        private const string startautomaticallty = "startautomaticallty";
        private const string startmanually = "startmanually";
        private const string shotsquare = "shotsquare";
        private const string shotwindow = "shotwindow";


        private const string shotfilepathlabel = "shotfilepath";
        private const string startmodelabel = "startmodellabel";
        private const string shotmodelabel = "shootmodellabel";

        private string shotFilePath;
        private StartMode startMode;
        private ShotMode shotMode;
        private Configuration config;
        public ConfigManager()
        {
            this.config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            loadShotFilePath();
            loadStartMode();
            loadShotMode();
        }
        public void loadShotFilePath()
        {
            this.shotFilePath = config.AppSettings.Settings[shotfilepathlabel].Value;
        }
        public string getShotFilePath()
        {
            return this.shotFilePath;
        }
        public void setShotFilePath(string newFilePath)
        {
            this.shotFilePath = newFilePath;
            this.config.AppSettings.Settings[shotfilepathlabel].Value = newFilePath;
        }
        public void loadStartMode()
        {
            string temp = config.AppSettings.Settings[startmodelabel].Value;
            if (temp == ConfigManager.startautomaticallty)
                this.startMode = EasyShot.StartMode.StartAutomaticallty;
            if (temp == ConfigManager.startmanually)
                this.startMode = EasyShot.StartMode.StartManually;
        }
        public StartMode getStartMode()
        {
            return this.startMode;
        }
        public void setStartMode(StartMode NewStartMode)
        {
            this.startMode = NewStartMode;
            if (NewStartMode == EasyShot.StartMode.StartAutomaticallty)
                this.config.AppSettings.Settings[startmodelabel].Value = ConfigManager.startautomaticallty;
            if (NewStartMode == EasyShot.StartMode.StartManually)
                this.config.AppSettings.Settings[startmodelabel].Value = ConfigManager.startmanually;
        }
        public void loadShotMode()
        {
            this.shotMode = EasyShot.ShotMode.ShotSquare;
        }
        public ShotMode getShotMode()
        {
            return this.shotMode;
        }
        public void setShotMode(ShotMode newShotMode)
        {
            this.shotMode = newShotMode;
            if (newShotMode == EasyShot.ShotMode.ShotSquare)
                this.config.AppSettings.Settings[shotmodelabel].Value = ConfigManager.shotsquare;
            if (newShotMode == EasyShot.ShotMode.ShotWindow)
                this.config.AppSettings.Settings[shotmodelabel].Value = ConfigManager.shotwindow;
        }
    }

    class CaptureWindow
    {

        private int x;//源图像的x逻辑坐标（在整个屏幕中）
        private int y;//源图像的y逻辑左部
        private int width;//源图像的宽度
        private int height;//源图像的长度
        private Image image;
        private string name;

        public void setImage(Image image) { this.image = image; }
        public Image getImage() { return this.image; }
        public void setName(string name) { this.name = name; }
        public string getName() { return this.name; }

        public CaptureWindow(int x, int y, int width, int height)//获取在两次鼠标移动时的图片大小
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        /// 

        public Image GetPic_ByHwnd(IntPtr hWnd)
        {
            // 根据句柄获取设备上下文句柄
            IntPtr hdcSrc = User32.GetWindowDC(hWnd);
            // 创建与指定设备兼容的存储器设备上下文(DC)
            IntPtr hdcDest = Gdi32.CreateCompatibleDC(hdcSrc);
            // 获取大小
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(hWnd, ref windowRect);
            //设置长宽
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;

            // 使用bitmap对象来存设备上下文数据
            IntPtr hBitmap = Gdi32.CreateCompatibleBitmap(hdcSrc, width, height);
            // 选择bitmap对象到指定设备上下文环境中
            IntPtr hOld = Gdi32.SelectObject(hdcDest, hBitmap);
            // 获取数据流
            Gdi32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, Gdi32.SRCCOPY);
            // 恢复设备上下文环境
            Gdi32.SelectObject(hdcDest, hOld);
            // 释放句柄
            Gdi32.DeleteDC(hdcDest);
            User32.ReleaseDC(hWnd, hdcSrc);
            // 将数据流转换成图
            Image img = Image.FromHbitmap(hBitmap);
            // 释放bitmap对象
            Gdi32.DeleteObject(hBitmap);
            return img;
        }

        public Image GetPic_Desktop()//获取整个屏幕的图片
        {
            Image img;
            img = GetPic_ByHwnd(User32.GetDesktopWindow());
            return img;
        }

        public Image GetPic_Window()
        {
            Image img;
            //一个GetWindowBymouse中的POINTAPI对象
            GetWindowBymouse.POINTAPI point = new GetWindowBymouse.POINTAPI();
            //获取当前鼠标的位置
            GetWindowBymouse.GetCursorPos(ref point);
            //获取当前鼠标所在的句柄
            IntPtr hWnd = GetWindowBymouse.WindowFromPoint(point.X, point.Y);
            img = GetPic_ByHwnd(hWnd);
            return img;
        }//根据鼠标位置获取对应窗口的图片

        public Image GetPic_Retangle(IntPtr hWnd)
        {
            Image img;
            // 根据句柄获取设备上下文句柄
            IntPtr hdcSrc = User32.GetWindowDC(hWnd);
            //创建与当前窗口DC兼容的DC
            IntPtr hdcDest = Gdi32.CreateCompatibleDC(hdcSrc);


            //创建具有相应尺寸的位图
            IntPtr hBitmap = Gdi32.CreateCompatibleBitmap(hdcSrc, width, height);//根据鼠标捕捉的图片宽度

            // 选择bitmap对象到指定设备上下文环境中
            IntPtr hOld = Gdi32.SelectObject(hdcDest, hBitmap);

            // 捕获图像，从指定的源句柄到目的句柄
            Gdi32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, x, y, Gdi32.SRCCOPY);//根据鼠标捕捉的图片大小

            // 恢复设备上下文环境
            Gdi32.SelectObject(hdcDest, hOld);

            // 释放目的句柄
            Gdi32.DeleteDC(hdcDest);

            User32.ReleaseDC(hWnd, hdcSrc);

            // 将数据流转换成图
            img = Image.FromHbitmap(hBitmap);

            // 释放bitmap对象
            Gdi32.DeleteObject(hBitmap);
            return img;

        }//根据鼠标移动生成的矩形获取对应的图片

        //public static Image GetPic_AnyShape()
        //{

        //}

        public class User32//用来窗口与用户交互
        {

            //用来保存句柄信息
            public struct WindowInfo
            {
                public IntPtr hWnd;//句柄
                public string Window_Name;//窗口名称
                public string Class_Name;//窗口类名称
            }

            //委托函数用于从Win32API-EnumWindows的回调函数
            public delegate bool WNDENUMPROC(IntPtr hWnd, int lParam);

            //用来遍历所有窗口 
            [DllImport("user32.dll")]
            public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, int lParam);

            //获取窗口Text 
            [DllImport("user32.dll")]
            public static extern int GetWindowText(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, int nMaxCount);

            //获取窗口类名 
            [DllImport("user32.dll")]
            public static extern int GetClassName(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, int nMaxCount);



            //获取所有的窗口对象
            public WindowInfo[] GetAllDesktopWindows()
            {
                List<WindowInfo> wndList = new List<WindowInfo>();
                EnumWindows(delegate (IntPtr hWnd, int lParam)
                {
                    WindowInfo wnd = new WindowInfo();//窗口信息
                    StringBuilder str = new StringBuilder(256);//字符缓存区
                    wnd.hWnd = hWnd;//获取句柄

                    //获取窗口名称
                    GetWindowText(hWnd, str, str.Capacity);
                    wnd.Window_Name = str.ToString();
                    //获取窗口类名称
                    GetClassName(hWnd, str, str.Capacity);
                    wnd.Class_Name = str.ToString();

                    wndList.Add(wnd);//把窗口句柄加到句柄列表里面
                    return true;
                }, 0);
                return wndList.ToArray();//返回
            }


            /*窗口的边框大小
             * 结构体与字节流相互转换
             */
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            //返回桌面窗口（整个屏幕）的句柄
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();

            //返回对应的窗口的句柄
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);

            //获取当前窗口句柄
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr GetForegroundWindow();

            //释放目前的句柄
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

            //返回对应窗口的边框矩形
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

            //根据窗口名字来找到窗口
            [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Unicode)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        }

        public class GetWindowBymouse//根据鼠标位置获取截图
        {
            //定义与API相兼容结构体，实际上是一种内存转换
            [StructLayout(LayoutKind.Sequential)]
            public struct POINTAPI
            {
                public int X;
                public int Y;
            }
            //获取鼠标坐标
            [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
            public static extern int GetCursorPos(
                ref POINTAPI lpPoint
            );

            //指定坐标处窗体句柄
            [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]
            public static extern IntPtr WindowFromPoint(int xPoint, int yPoint);

            //获取坐标处窗口名称
            [DllImport("user32.dll", EntryPoint = "GetWindowText")]
            public static extern int GetWindowText(int hWnd, StringBuilder lpString, int nMaxCount);

            //获取窗口类的名称
            [DllImport("user32.dll", EntryPoint = "GetClassName")]
            public static extern int GetClassName(int hWnd, StringBuilder lpString, int nMaxCont);
        }

        public class Gdi32//用来绘制图片
        {
            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
                              int nWidth, int nHeight, IntPtr hObjectSource,
                              int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
                              int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }

        //public void ShowPicture()//展示图片
        //{

        //}


    }

    //class PictureModify 
    //{
    //   private Image image;

        //public show
        //public Image cut()
        //{
            
    //    //}
    //}

}



