using System;
using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Threading;

namespace _EP4__MobAll_2.WindowWPF
{
    /// <summary>
    /// Логика взаимодействия для WindowInfo.xaml
    /// </summary>
    public partial class WindowInfo : MetroWindow
    {
        public enum TypeWindow
        {
            InfoContent,
            InfoYesNo,
            InfoProgress
        }
        public WindowInfo(TypeWindow window, Window win, string Content = "")
        {
            InitializeComponent();
            this.window = window;
            this.top = win.Top;
            this.left = win.Left;
            this.ParentHeight = win.Height;
            this.ParentWight = win.Width;
            this.Content = Content;
        }

        private DispatcherTimer timer = new DispatcherTimer();
        private double ParentWight { get; set; } = 941.5;
        private double ParentHeight { get; set; } = 597;
        private TypeWindow window;
        private double top;
        private double left;
        private string Content { get; set; } = "";

        private void SetPosition(double Top, double Left)
        {
            this.Top = (Top + ParentHeight) - this.Height;
            this.Left = (Left + ParentWight) - this.Width;
        }

        private void Timer()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0,0, 2);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Opacity -= 0.01;
            if (Opacity <= 0)
            {
                timer.Stop();
                Close();
            }
        }
        public void SetHWParent(double height, double width)
        {
            ParentHeight = height;
            ParentWight = width;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetPosition(top, left);
            switch (window)
            {
                case TypeWindow.InfoContent:
                    G_WindowContent.Visibility = Visibility.Visible;
                    L_info.Content = Content;
                    break;
                case TypeWindow.InfoYesNo:

                    break;
                case TypeWindow.InfoProgress:

                    break;
            }
            Timer();
        }
    }
}
