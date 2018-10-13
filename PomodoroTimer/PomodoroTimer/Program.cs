using System;

namespace PomodoroTimer
{
    class Program
    {
        static void Main(string[] args)
        {
            // After four pomodoros, take a longer break 
            int pomodoro = 1;
            // Work Interval = 25 minutes
            // Break Interval = 5 minutes
            // Rest Interval = 30 minutes
            DateTime startTime, workInterval, breakInterval, restInterval;
            bool focusTime, breakTime, restTime;

            while (pomodoro <= 8)
            {
                
                startTime = DateTime.Now;
                workInterval = startTime.AddSeconds(2);
                breakInterval = startTime;
                restInterval = startTime;
                focusTime = true;                
                breakTime = false;
                restTime = false;
                
                if (focusTime)
                {

                    while (DateTime.Now < workInterval)
                    {
                        //Tick 
                        Console.WriteLine("Focus : " + pomodoro);      
                    }

                    focusTime = false;

                    if (pomodoro == 4)
                    {
                        restTime = true;
                        restInterval = DateTime.Now.AddSeconds(10);
                    }

                    else
                    {
                        breakTime = true;
                        breakInterval = DateTime.Now.AddSeconds(5);
                    }          
                    
                }
               
                if (breakTime)
                {

                    while (DateTime.Now < breakInterval)
                    {
                        //Tick
                        Console.WriteLine("Break : " + pomodoro);

                    }                   
                    
                    breakTime = false;
                    focusTime = true;

                }

                if (restTime)
                {

                    while (DateTime.Now < restInterval)
                    {
                        //Tick
                        Console.WriteLine("Rest : " + pomodoro);
                    }

                    restTime = false;
                    focusTime = true;

                }

                pomodoro++;

            }

            Console.WriteLine("End");
            Console.ReadKey();

        }
    }
}
