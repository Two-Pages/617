using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4
{
    using System;
    using System.Threading;

    // 闹钟类
    public class Clock
    {
        // 嘀嗒事件的委托类型
        public delegate void TickHandler(object sender, DateTime time);
        // 响铃事件的委托类型
        public delegate void AlarmHandler(object sender, DateTime time);

        // 嘀嗒事件
        public event TickHandler OnTick;
        // 响铃事件
        public event AlarmHandler OnAlarm;

        // 闹钟的当前时间
        public DateTime CurrentTime { get; set; }
        // 闹钟的响铃时间
        public DateTime AlarmTime { get; set; }

        // 构造函数，传入当前时间和响铃时间作为参数
        public Clock(DateTime currentTime, DateTime alarmTime)
        {
            CurrentTime = currentTime;
            AlarmTime = alarmTime;
        }

        // 开始运行闹钟的方法
        public void Run()
        {
            while (true)
            {
                // 每隔一秒更新一次当前时间，并触发嘀嗒事件
                Thread.Sleep(1000);
                CurrentTime = CurrentTime.AddSeconds(1);
                OnTick(this, CurrentTime);

                // 如果当前时间等于响铃时间，则触发响铃事件，并退出循环
                if (CurrentTime == AlarmTime)
                {
                    OnAlarm(this, CurrentTime);
                    break;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 创建一个闹钟对象，传入当前时间和响铃时间作为参数
            Clock clock = new Clock(DateTime.Now, DateTime.Now.AddSeconds(10));

            // 为闹钟对象的嘀嗒事件和响铃事件添加处理方法
            clock.OnTick += ShowTick;
            clock.OnAlarm += ShowAlarm;

            // 开始运行闹钟
            clock.Run();
        }

        // 显示嘀嗒信息的方法，参数为发送者和当前时间
        static void ShowTick(object sender, DateTime time)
        {
            Console.WriteLine($"Tick: {time}");
        }

        // 显示响铃信息的方法，参数为发送者和当前时间
        static void ShowAlarm(object sender, DateTime time)
        {
            Console.WriteLine($"Alarm: {time}");
            Console.WriteLine("Wake up!");
        }
    }

}

