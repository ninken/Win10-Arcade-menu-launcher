using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SharpDX.XInput;


namespace ArcadeMenu
{
    public partial class MainWindow : MetroWindow
    {

        public static int beforeRotate;
        public string path = AppDomain.CurrentDomain.BaseDirectory;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DispatcherTimer _timer = new DispatcherTimer();
        DispatcherTimer autocon = new DispatcherTimer();
        private Controller _controller;
        public event PropertyChangedEventHandler PropertyChanged;


        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        const int SW_MAXIMIZE = 3;


        public MainWindow()
        {
            InitializeComponent();

            this.WindowStyle = WindowStyle.None;
            this.BringIntoView();
            this.Topmost = true;

            //Get Current Rotation
            int i = ArcadeMenu.Display.GetCurrentRotate(1);

            //Sizing tiles to monitor
            if (i == 3)
            {
                var gridLengthConverter1 = new System.Windows.GridLengthConverter();
                string ww1 = (this.Width / 20).ToString();
                colwidth.Width = (GridLength)gridLengthConverter1.ConvertFromString(ww1);
                beforeRotate = 270;
            }
            if (i == 1)
            {
                var gridLengthConverter2 = new System.Windows.GridLengthConverter();
                string ww2 = (this.Width / 20).ToString();
                colwidth.Width = (GridLength)gridLengthConverter2.ConvertFromString(ww2);
                beforeRotate = 90;
            }
            if (i == 2)
            {
                var gridLengthConverter3 = new System.Windows.GridLengthConverter();
                string ww3 = (this.Width / 4).ToString();
                colwidth.Width = (GridLength)gridLengthConverter3.ConvertFromString(ww3);
                beforeRotate = 180;
            }
            if (i == 0)
            {
                var gridLengthConverter = new System.Windows.GridLengthConverter();
                string ww = (this.Width / 4).ToString();
                colwidth.Width = (GridLength)gridLengthConverter.ConvertFromString(ww);
                beforeRotate = 0;
            }


            //MessageBox.Show(i.ToString());
            //Defaults on startup
           // beforeRotate = ArcadeMenu.Display.GetCurrentRotate(1);
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            this.WindowState = WindowState.Maximized;

        }

        public void _timer_Tick(object sender, EventArgs e)
        {

            // Lost controller connection...
            if (_controller.IsConnected == false)
            {
                _timer.Stop();
                autocon.Start();
                return;
            }

            // My Button Translation to menu...
            var state = _controller.GetState();
            if (state.Gamepad.Buttons.ToString() == "A")
            {
                KeyEventArgs ke = new KeyEventArgs(
                       Keyboard.PrimaryDevice,
                       Keyboard.PrimaryDevice.ActiveSource,
                       0,
                       Key.Enter)
                {
                    RoutedEvent = UIElement.KeyDownEvent
                };

                InputManager.Current.ProcessInput(ke);
            }

            if (state.Gamepad.Buttons.ToString() == "DPadDown")
            {
                KeyEventArgs ke = new KeyEventArgs(
                       Keyboard.PrimaryDevice,
                       Keyboard.PrimaryDevice.ActiveSource,
                       0,
                       Key.Down)
                {
                    RoutedEvent = UIElement.KeyDownEvent
                };

                InputManager.Current.ProcessInput(ke);
            }

            if (state.Gamepad.Buttons.ToString() == "DPadUp")
            {
                KeyEventArgs ke = new KeyEventArgs(
                       Keyboard.PrimaryDevice,
                       Keyboard.PrimaryDevice.ActiveSource,
                       0,
                       Key.Up)
                {
                    RoutedEvent = UIElement.KeyDownEvent
                };

                InputManager.Current.ProcessInput(ke);
            }


            if (state.Gamepad.Buttons.ToString() == "DPadLeft")
            {
                KeyEventArgs ke = new KeyEventArgs(
                       Keyboard.PrimaryDevice,
                       Keyboard.PrimaryDevice.ActiveSource,
                       0,
                       Key.Left)
                {
                    RoutedEvent = UIElement.KeyDownEvent
                };

                InputManager.Current.ProcessInput(ke);
            }

            if (state.Gamepad.Buttons.ToString() == "DPadRight")
            {
                KeyEventArgs ke = new KeyEventArgs(
                       Keyboard.PrimaryDevice,
                       Keyboard.PrimaryDevice.ActiveSource,
                       0,
                       Key.Right)
                {
                    RoutedEvent = UIElement.KeyDownEvent
                };

                InputManager.Current.ProcessInput(ke);
            }
        }


        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Get first Controller
            _controller = new Controller(UserIndex.One);
            if (_controller.IsConnected)
            {
                //Game Controller input capture
                _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(75) };
                _timer.Tick += _timer_Tick;
                _timer.Start();
                return;
            }
            // No Controller found, we will run until one is connected.
            if (_controller.IsConnected == false)
            {
                _timer.Stop();
                autocon.Tick += new EventHandler(autocon_Tick);
                autocon.Interval = new TimeSpan(0, 0, 1);
                autocon.Start();
            }
        }

        private void autocon_Tick(object sender, EventArgs e)
        {
            // Get first Controller
            _controller = new Controller(UserIndex.One);
            if (_controller.IsConnected)
            {
                //Game Controller input capture
                _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(75) };
                _timer.Tick += _timer_Tick;
                _timer.Start();
                autocon.Stop();
                return;
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            //dispatcherTimer.Stop();
            tBigbox.BringIntoView();
            tBigbox.Focus();
            Keyboard.Focus(tBigbox);
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _controller = null;
        }


        // Monitor Rotation....
        private void r270_Click(object sender, RoutedEventArgs e)
        {
            if (beforeRotate != 270)
            {
                ArcadeMenu.Display.Rotate(1, Display.Orientations.DEGREES_CW_90);
                var gridLengthConverter = new System.Windows.GridLengthConverter();
                string ww = (this.Width / 20).ToString();
                colwidth.Width = (GridLength)gridLengthConverter.ConvertFromString(ww);
                beforeRotate = 270;
            }
        }

        private void r0_Click(object sender, RoutedEventArgs e)
        {
            if (beforeRotate != 0)
            {
                ArcadeMenu.Display.Rotate(1, Display.Orientations.DEGREES_CW_0);
                var gridLengthConverter = new System.Windows.GridLengthConverter();
                string ww = (this.Width / 4).ToString();
                colwidth.Width = (GridLength)gridLengthConverter.ConvertFromString(ww);
                beforeRotate = 0;
            }
        }

        private void r90_Click(object sender, RoutedEventArgs e)
        {

            if (beforeRotate != 90)
            {
                ArcadeMenu.Display.Rotate(1, Display.Orientations.DEGREES_CW_270);
                var gridLengthConverter = new System.Windows.GridLengthConverter();
                string ww = (this.Width / 20).ToString();
                colwidth.Width = (GridLength)gridLengthConverter.ConvertFromString(ww);
                beforeRotate = 90;
            }
        }

        private void r180_Click(object sender, RoutedEventArgs e)
        {
            if (beforeRotate != 180)
            {
                ArcadeMenu.Display.Rotate(1, Display.Orientations.DEGREES_CW_180);
                var gridLengthConverter = new System.Windows.GridLengthConverter();
                string ww = (this.Width / 4).ToString();
                colwidth.Width = (GridLength)gridLengthConverter.ConvertFromString(ww);
                beforeRotate = 180;
            }
        }

        // Computer Options
        private async void trestart_Click(object sender, RoutedEventArgs e)
        {

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Restart",
                NegativeButtonText = "Cancel",
                AnimateShow = true,
                AnimateHide = false
            };
            var result = await this.ShowMessageAsync(
                "Restart Computer",
                "Sure you want to restart?",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if(result == MessageDialogResult.Affirmative)
            {
                System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0");
            }
            else
            {
                return;
            }

        }

        private async void tshutdown_Click(object sender, RoutedEventArgs e)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Shutdown",
                NegativeButtonText = "Cancel",
                AnimateShow = true,
                AnimateHide = false
            };
            var result = await this.ShowMessageAsync(
                "Shutdown Computer",
                "Sure you want to Shutdown?",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result == MessageDialogResult.Affirmative)
            {
                System.Diagnostics.Process.Start("shutdown.exe", "-s -t 0");
            }
            else
            {
                return;
            }
        }

        private async void tsleep_Click(object sender, RoutedEventArgs e)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Sleep",
                NegativeButtonText = "Cancel",
                AnimateShow = true,
                AnimateHide = false
            };
            var result = await this.ShowMessageAsync(
                "Sleep",
                "Send this computer to sleep?",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result == MessageDialogResult.Affirmative)
            {
                // System.Diagnostics.Process.Start("shutdown.exe", "-s -hybrid -t 0");
                System.Diagnostics.Process.Start("RUNDLL32.EXE powrprof.dll,SetSuspendState 0,1,0");
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                return;
            }
        }


        // Other Main functions

        private void tcontroller_Click(object sender, RoutedEventArgs e)
        {
            //Stop Controllers for another window
            autocon.Stop();
            _timer.Stop();
            dispatcherTimer.Stop();

            Controls cwindow = new Controls();
            cwindow.ShowDialog();

            //Restart Timers
            autocon.Start();
            dispatcherTimer.Start();
        }

        private void texit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Autohotkey Executes Function
        private void tBigbox_Click(object sender, RoutedEventArgs e)
        {
            if (beforeRotate != 0)
            {
                try
                {
                    System.Diagnostics.Process.Start(path + @"\AHK_BigBoxPort.ahk");
                }
                catch (Exception)
                {
                    return;
                }

            }
            else
            {
                try
                {
                    System.Diagnostics.Process.Start(path + @"\AHK_BigBoxLand.ahk");
                }
                catch (Exception)
                {
                    return;
                }
            }
            System.Windows.Application.Current.Shutdown();

        }

        private void tkodi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(path + @"\AHK_Kodi.ahk");
                System.Windows.Application.Current.Shutdown();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void tdroid_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(path + @"\AHK_Andriod.ahk");
                System.Windows.Application.Current.Shutdown();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void tdesk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(path + @"\AHK_Desktop.ahk");
                System.Windows.Application.Current.Shutdown();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void ttask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(path + @"\AHK_Task.ahk");
            }
            catch (Exception)
            {
                return;
            }
        }

        private void tpinball_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(path + @"\AHK_Pinball.ahk");
                System.Windows.Application.Current.Shutdown();
            }
            catch (Exception)
            {
                return;
            }
        }


        // Global Tile Execution 
        private void Tile_GotFocus(object sender, RoutedEventArgs e)
        {
            MahApps.Metro.Controls.Tile source = e.Source as MahApps.Metro.Controls.Tile;
    

            if (source != null)
            {
                source.BorderThickness = new Thickness(5, 5, 5, 5);
                source.BorderBrush = Brushes.White;
            }
        }

        private void Tile_LostFocus(object sender, RoutedEventArgs e)
        {
            MahApps.Metro.Controls.Tile source = e.Source as MahApps.Metro.Controls.Tile;

            if (source != null)
            {
                source.BorderThickness = new Thickness(0, 0, 0, 0);
                source.BorderBrush = Brushes.Transparent;
            }
        }

        private void Tile_KeyDown(object sender, KeyEventArgs e)
        {
            MahApps.Metro.Controls.Tile source = e.Source as MahApps.Metro.Controls.Tile;

            if (source != null)
            {

                switch (e.Key)
                {
                    case Key.Enter:
                        RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);
                        source.RaiseEvent(newEventArgs);

                        break;

                }
            }
        }


    }
}
