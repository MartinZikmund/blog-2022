using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Uno.Extensions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Gaming.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UnoGamepad
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer _timer = new DispatcherTimer();

        public MainPage()
        {
            this.InitializeComponent();
            
            Gamepad.GamepadAdded += GamepadsChanged;
            Gamepad.GamepadRemoved += GamepadsChanged;
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += OnGamepadReadingUpdate;
            _timer.Start();
            UpdateGamepadsAsync();
        }

        private void OnGamepadReadingUpdate(object sender, object e)
        {
            await UpdateGamepadsAsync();
            foreach(var gamepad in Gamepads)
            {
                gamepad.Update();
            }
        }

        public ObservableCollection<GamepadViewModel> Gamepads { get; } = new ObservableCollection<GamepadViewModel>();

        private async void GamepadsChanged(object sender, Gamepad e)
        {
            await UpdateGamepadsAsync();
        }

        private async Task UpdateGamepadsAsync()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var gamepads = Gamepad.Gamepads.ToArray();

                var existingGamepads = new HashSet<Gamepad>(Gamepads.Select(g => g.Gamepad));

                var toRemove = existingGamepads.Except(gamepads).ToArray();
                var toAdd = gamepads.Except(existingGamepads).ToArray();
                
                foreach (var gamepad in toRemove)
                {
                    var vmToRemove = Gamepads.FirstOrDefault(g => g.Gamepad == gamepad);
                    Gamepads.Remove(vmToRemove);
                }

                foreach (var gamepad in toAdd)
                {
                    var vmToAdd = new GamepadViewModel(gamepad);
                    Gamepads.Add(vmToAdd);
                }
            });
        }
    }
}
