using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Threading;

namespace Clock_Widget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            btnClose.IsEnabled = false;
            btnClose.Opacity = 0;

            btnTheme.IsEnabled = false;
            btnTheme.Opacity = 0;

            //FIRSTRUN
            if (Properties.Settings.Default.FirstRun == true)
            {
                //app was run the first time + this code won't be executed anymore!

                Properties.Settings.Default.FirstRun = false;
                Properties.Settings.Default.Save(); //!!!

                rk.SetValue("Clock Widget", System.Windows.Forms.Application.ExecutablePath.ToString());//because the Checkbox is default True!
            }

            Top = Properties.Settings.Default.Top;
            Left = Properties.Settings.Default.Left;

            lblTime.Content = DateTime.Now.ToLongTimeString();
            if (Properties.Settings.Default.Theme == 0)
            {
                lblTime.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            }
            else
            {
                lblTime.Foreground = new SolidColorBrush(Color.FromArgb(255, 57, 57, 57));
            }
            

            DispatcherTimer objDispatcherTimer = new DispatcherTimer();
            objDispatcherTimer.Tick += ObjDispatcherTimer_Tick;
            objDispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            objDispatcherTimer.Start();

        }
        private void ObjDispatcherTimer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Top = Top;
            Properties.Settings.Default.Left = Left;
            Properties.Settings.Default.Save();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnClose.IsEnabled = true;
            btnClose.Opacity = 100;

            btnTheme.IsEnabled = true;
            btnTheme.Opacity = 100;
        }

        private void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnClose.IsEnabled = false;
            btnClose.Opacity = 0;

            btnTheme.IsEnabled = false;
            btnTheme.Opacity = 0;
        }

        private void btnTheme_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Theme == 0)
            {
                Properties.Settings.Default.Theme = 1;
                Properties.Settings.Default.Save();

                lblTime.Foreground = new SolidColorBrush(Color.FromArgb(255, 57, 57, 57));
            }
            else
            {
                Properties.Settings.Default.Theme = 0;
                Properties.Settings.Default.Save();

                lblTime.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            }
        }
    }
}
