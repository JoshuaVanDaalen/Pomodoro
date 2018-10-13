using System;
using System.Windows;

using System.Windows.Threading;

namespace PomodoroTimer_WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Left = System.Windows.SystemParameters.WorkArea.Width - Width;
            Top = System.Windows.SystemParameters.WorkArea.Height - Height;
        }

        void Countdown(int Pomodoro, Action<string> clock)
        {
            int minutes = 25;
            int secondsduration = 60;
            DispatcherTimer seconds = new DispatcherTimer();
            seconds.Interval = TimeSpan.FromSeconds(1);

            seconds.Tick += (_, a) =>
            {                
                if ((minutes <= 0) && (secondsduration == 0))
                {
                    seconds.Stop();
                    //clock("00:00");
                }
                else if (secondsduration-- == 0)
                {
                    secondsduration = 60;
                    minutes--;
                }
                else
                {
                    clock($"{(minutes - 1).ToString()}:00");
                }                
                //Show displayed clock
                if ((secondsduration.ToString().Length == 1) && (minutes.ToString().Length == 1))
                    clock($"0{ minutes.ToString()}:0{secondsduration.ToString()}");
                else if (minutes.ToString().Length == 1)
                    clock($"0{ minutes.ToString()}:{secondsduration.ToString()}");
                else if (secondsduration.ToString().Length == 1)
                    clock($"{ minutes.ToString()}:0{secondsduration.ToString()}");
                else
                    clock($"{ minutes.ToString()}:{secondsduration.ToString()}");
            };
            seconds.Start();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //TimerLabel.Content = "Hello World!";
            Countdown(8, cur => TimerLabel.Content = cur.ToString());
        }
    }
}
