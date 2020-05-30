using System;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace PomodoroTimer.WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Left = SystemParameters.WorkArea.Width - Width;
            Top = SystemParameters.WorkArea.Height - Height;
            Topmost = true;
            WindowStyle = WindowStyle.None;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Pomodoro(25);
        }

        private void Pomodoro(int startingMinutes)
        {
            //
            TimerLabel.Content = $"{startingMinutes}:00";
            Countdown(startingMinutes, cur => TimerLabel.Content = cur.ToString());
        }

        void Countdown(int startTime, Action<string> clock)
        {

            int minutes = startTime;
            int seconds = 60;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);

            timer.Tick += (_, a) =>
            {
                seconds--;

                // If 0 seconds skip and just show minutes:00
                if (minutes <= 0 && seconds == 0) // If the clock is at 0 stop counting down
                {
                    timer.Stop();
                    clock($"00:00");
                    return;
                }

                else if (seconds == 0) // If the clock has minutes left and has reached 0 seconds
                {
                    seconds = 60;
                    minutes--;
                }

                else { } // Otherwise the clock is ticking

                // Show displayed clock
                if (seconds < 10 && minutes < 10)
                {
                    // Append 0 at start of both minutes & seconds
                    clock($"0{minutes}:0{seconds}");
                }

                else if (minutes < 10 && minutes == 60)
                {
                    // Append 0 at start of minutes 
                    clock($"0{minutes}:00");
                }
                else if (minutes < 10)
                {
                    // Append 0 at start of minutes 
                    clock($"0{minutes}:{(seconds == 60 ? seconds - 1 : seconds)}");
                }
                else if (seconds < 10)
                {
                    // Append 0 at start of seconds
                    clock($"{(minutes == startTime ? minutes - 1 : minutes)}:0{seconds}");
                }
                else
                {
                    if (minutes == startTime)
                    {
                        // Clock is ticking
                        clock($"{minutes - 1}:{seconds}");
                    }
                    else if (seconds == 60)
                    {
                        // Minutes have decreased and seconds are on 60
                        clock($"{minutes}:00");
                    }
                    else if (seconds == 59)
                    {
                        minutes--;
                        clock($"{minutes}:{seconds}");
                    }
                    else
                    {
                        clock($"{minutes}:{seconds}");
                    }
                }
            };
            timer.Start();
        }
    }
}
