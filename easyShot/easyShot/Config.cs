
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

//simple is perfect!
namespace easyShot
{
    class CaptureWindow
    {


        private Point Point_push;
        private Point Point_out;
        private int width;//源图像的宽度
        private int height;//源图像的长度
        private Image image;//图片
        private string name;//图片名字


        public double getWidth() { return this.width; }
        public double getHeight() { return this.height; }

        public void setImage(Image image) { this.image = image; }
        public Image getImage() { return this.image; }
        public void setName(string name) { this.name = name; }
        public string getName() { return this.name; }

        //获取图片
        public void setPicture(Image img) { this.image = img; }

        public Image getPicture() { return this.image; }

        //默认构造函数
        public CaptureWindow() { }


        //通过句柄来获取图片
        public Image GetPic_ByHwnd(IntPtr hWnd, User32.RECT rect)
        {


            // 根据句柄获取设备上下文句柄
            IntPtr hdcSrc = User32.GetWindowDC(hWnd);
            // 创建与指定设备兼容的存储器设备上下文(DC)
            IntPtr hdcDest = Gdi32.CreateCompatibleDC(hdcSrc);

            //设置长宽
            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;

            //图片长宽和起点赋值
            this.width = width;
            this.height = height;


            // 使用bitmap对象来存设备上下文数据
            IntPtr hBitmap = Gdi32.CreateCompatibleBitmap(hdcSrc, width, height);
            // 选择bitmap对象到指定设备上下文环境中
            IntPtr hOld = Gdi32.SelectObject(hdcDest, hBitmap);
            // 执行与指定源设备上下文的像素矩形对应的颜色数据的位块传输到目标设备上下文。
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

        //通过鼠标前后两个位置来获取图片
        public Image GetPic_ByMouse(IntPtr hWnd)
        {
            Image img;


            // 获取设备上下文环境句柄
            IntPtr hscrdc = User32.GetWindowDC(hWnd);

            // 创建一个与指定设备兼容的内存设备上下文环境（DC）
            IntPtr hmemdc = Gdi32.CreateCompatibleDC(hscrdc);
            IntPtr myMemdc = Gdi32.CreateCompatibleDC(hscrdc);

            // 返回指定窗体的矩形尺寸
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(hWnd, ref windowRect);

            // 返回指定设备环境句柄对应的位图区域句柄
            IntPtr myBitmap = Gdi32.CreateCompatibleBitmap(hscrdc, width, height);
            IntPtr hbitmap = Gdi32.CreateCompatibleBitmap(hscrdc, windowRect.right - windowRect.left, windowRect.bottom - windowRect.top);

            //把位图选进内存DC 

            Gdi32.SelectObject(hmemdc, hbitmap);
            Gdi32.SelectObject(myMemdc, myBitmap);


            Gdi32.BitBlt(myMemdc, 0, 0, width, height, hscrdc, Point_push.X, Point_push.Y, Gdi32.SRCCOPY);
            img = Image.FromHbitmap(myBitmap);
            Gdi32.DeleteDC(hscrdc);
            Gdi32.DeleteDC(hmemdc);
            Gdi32.DeleteDC(myMemdc);
            return img;
        }
        public Image GetPic_Desktop()//获取整个屏幕的图片
        {
            Image img;
            // 获取窗口大小
            IntPtr hWnd = User32.GetDesktopWindow();
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(hWnd, ref windowRect);
            img = GetPic_ByHwnd(hWnd, windowRect);
            return img;
        }

        public Image GetPic_Window(IntPtr hWnd)//获取某个窗口的图片
        {
            Image img;

            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(hWnd, ref windowRect);
            img = GetPic_ByHwnd(hWnd, windowRect);
            return img;
        }//根据鼠标位置获取对应窗口的图片

        public Image GetPic_Retangle(Point p1, Point p2)//获取鼠标点击和松开后的图片
        {

            Image img;
            //参数获取
            this.Point_push = p1;
            this.Point_out = p2;
            this.width = System.Math.Abs(Point_out.X - Point_push.X);

            this.height = System.Math.Abs(Point_out.Y - Point_push.Y);

            //使用全屏窗口的句柄
            IntPtr hWnd = User32.GetDesktopWindow();
            img = GetPic_ByMouse(hWnd);//通过鼠标
            return img;

        }//根据鼠标移动生成的矩形获取对应的图片


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
            //设置前置窗口
            [DllImport("user32.dll")]
            public static extern bool SetForegroundWindow(IntPtr hWnd);

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

            internal static void BitBlt(IntPtr hdcDest, int v1, int v2, double width, double height, IntPtr hdcSrc, int x, int y, int sRCCOPY)
            {
                throw new NotImplementedException();
            }


        }


    }
}
