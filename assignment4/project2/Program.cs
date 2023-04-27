using System;
using System.Xml.Serialization;

namespace project2
{
    public class Clock
    {
        private int hour, minute, second;
        private int now_Hour, now_Minute, now_Second;
        public event EventHandler Tick ;
        public event EventHandler Alarm ;
     
        public Clock(int h, int m, int s)
        {
            if (h >= 0 && h < 24 && m >= 0 && m < 60 && s >= 0 && s < 60)
            {
                hour = h;
                minute = m;
                second = s;
            } 
            else{
                hour = minute = second = 0; ;
            }
            now_Hour = now_Minute = now_Second = 0; 
        }
        
        public void start()
        {
            while (true)
            {
                Tick.Invoke(this, EventArgs.Empty);
                Thread.Sleep(1000);
                second = (second == 59) ? 0 : second + 1;
                if(second==0)minute = (minute == 59) ? 0 : minute + 1;
                if(second==0&&minute==0)hour = (hour == 23) ? 0 : hour + 1;
                if (now_Hour == hour && now_Minute == minute && now_Second == second)
                {
                    Alarm.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void setTime(int h, int m, int s)
        {
            if (h >= 0 && h < 24 && m >= 0 && m < 60 && s >= 0 && s < 60)
            {
                now_Hour = h;
                now_Minute = m;
                now_Second = s;
            }
            else
            {
                throw new Exception();
            }
        }

        class program
        {
            public static void Main(string[] args)
            {
                Clock c;
                c = new Clock(0, 0, 3);

                try
                {
                    c.setTime(0, 0, 5);
                }
                catch
                {
                    Console.WriteLine("time error");
                }
                c.Tick += Clock_Tick;
                c.Alarm+=Clock_Alarm;
                c.start();
            }
            private static void Clock_Tick(object sender, EventArgs e)
            {
                Console.WriteLine("tiktok");
            }
            private static void Clock_Alarm(object sender, EventArgs e)
            {
                Console.WriteLine(" ring");
            }
        }
    }
}