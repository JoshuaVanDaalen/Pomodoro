using System;
using System.Windows;
using System.Windows.Threading;

namespace PomodoroTimer.WPFApp
{
    public partial class MainWindow : Window
    {
        // After 4 pomodoros, take a longer break 
        // Work Interval  = 25 minutes
        // Break Interval = 5 minutes
        // Rest Interval  = 30 minutes
        DispatcherTimer dispatcherTimer;
        TimeSpan timeSpan;
        int count = 0;
        public MainWindow()
        {
            InitializeComponent();
            Left = SystemParameters.WorkArea.Width - Width;
            Top = SystemParameters.WorkArea.Height - Height;
            Topmost = true;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            count++; // Start the first round
            Pomodoro(25);
        }

        private void Pomodoro(int minutes)
        {
            timeSpan = TimeSpan.FromMinutes(minutes);
            if (minutes != 25) timeSpan = timeSpan.Add(TimeSpan.FromSeconds(1));

            dispatcherTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                TimerLabel.Content = timeSpan.ToString(@"mm\:ss");
                if (timeSpan == TimeSpan.Zero)
                {
                    count++;
                    dispatcherTimer.Stop();
                    switch (count)
                    {
                        case 8:
                            RestInterval();
                            break;
                        case 7:
                            WorkInterval();
                            break;
                        case 6:
                            BreakInterval();
                            break;
                        case 5:
                            WorkInterval();
                            break;
                        case 4:
                            BreakInterval();
                            break;
                        case 3:
                            WorkInterval();
                            break;
                        case 2:
                            BreakInterval();
                            break;
                        case 1: // Shouldn't ever get to this case
                            WorkInterval();
                            break;
                        default:
                            SetNormalWindowState();
                            TimerLabel.Content = "DONE";
                            break;
                    }
                }
                timeSpan = timeSpan.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            dispatcherTimer.Start();
        }

        private void WorkInterval()
        {
            SetNormalWindowState();
            Pomodoro(25);
        }
        private void BreakInterval()
        {
            SetRestAndBreakWindowState();
            Pomodoro(5);
        }
        private void RestInterval()
        {
            SetRestAndBreakWindowState();
            Pomodoro(30);
        }
        private void SetNormalWindowState()
        {
            WindowState = WindowState.Normal;
            TimerLabel.FontSize = 32;
        }
        private void SetRestAndBreakWindowState()
        {
            WindowState = WindowState.Maximized;
            TimerLabel.FontSize = 128;

        }
    }
}
