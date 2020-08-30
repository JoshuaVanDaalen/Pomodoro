using System;
using System.Windows;
using System.Windows.Threading;

namespace PomodoroTimer
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
            if (count > 1) timeSpan = timeSpan.Add(TimeSpan.FromSeconds(1));

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
                            SetWindowState(true);
                            Pomodoro(30);
                            break;
                        case 7:
                        case 5:
                        case 3:
                            SetWindowState(false);
                            Pomodoro(25);
                            break;
                        case 6:
                        case 4:
                        case 2:
                            SetWindowState(true);
                            Pomodoro(5);
                            break;
                        default:
                            SetWindowState(false);
                            TimerLabel.Content = "Done";
                            break;
                    }
                }
                timeSpan = timeSpan.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            dispatcherTimer.Start();
        }

        private void SetWindowState(bool maximized)
        {
            if (maximized)
            {
                WindowState = WindowState.Maximized;
                TimerLabel.FontSize = 128;
            }
            else
            {
                WindowState = WindowState.Normal;
                TimerLabel.FontSize = 32;
            }
        }
    }
}
