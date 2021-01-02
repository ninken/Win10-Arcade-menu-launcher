using MahApps.Metro.Controls;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using SharpDX.XInput;

namespace ArcadeMenu
{
    /// <summary>
    /// Interaction logic for Controls.xaml
    /// </summary>
    public partial class Controls : MetroWindow
    {

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DispatcherTimer _timer = new DispatcherTimer();
        DispatcherTimer autocon = new DispatcherTimer();
        private Controller _controller;

        public Controls()
        {
            InitializeComponent();

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
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
            dispatcherTimer.Stop();
            tback.BringIntoView();
            tback.Focus();
            Keyboard.Focus(tback);
        }



    //Buttons...

        private void tback_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
