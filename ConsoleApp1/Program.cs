using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string str = "";
            double num1;
            Console.Write("请输入第一个数字");
            str = Console.ReadLine();
            num1 = Convert.ToDouble(str);
            double num2;
            Console.Write("请输入第二个数字: ");
            str = Console.ReadLine();
            num2 = Convert.ToDouble(str);

            Console.WriteLine($"num1: {num1} ,num2: {num2}");
            Console.WriteLine($"num1+num2: {num1 + num2} ");
        }
    }
}
