using System;
using System.Windows;
using System.Windows.Threading;

namespace PomodoroTimer.WPFApp
{
    public partial class MainWindow : Window
    {
        // After four pomodoros, take a longer break 
        // Work Interval  = 25 minutes
        // Break Interval = 5 minutes
        // Rest Interval  = 30 minutes
        DispatcherTimer dispatcherTimer;
        TimeSpan timeSpan;

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
            Pomodoro(5);
            Pomodoro(25);
            Pomodoro(5);
            Pomodoro(25);
            Pomodoro(5);
            Pomodoro(25);
            Pomodoro(30);
        }

        private void Pomodoro(int minutes)
        {
            timeSpan = TimeSpan.FromMinutes(minutes);

            dispatcherTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                TimerLabel.Content = timeSpan.ToString(@"mm\:ss");
                if (timeSpan == TimeSpan.Zero) dispatcherTimer.Stop();
                timeSpan = timeSpan.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            dispatcherTimer.Start();
        }
    }
}
