using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("请输入一个整数:");
                int num = Convert.ToInt32(Console.ReadLine());
                List<int> factors = Factorize(num);
                Console.Write("素因子有:");
                factors.ForEach(f => Console.Write("\t" + f));
            }
            catch (Exception e)
            {
                Console.WriteLine($"错误:{e.Message}");
            }
        }

        //因数分解(连除法)
        private static List<int> Factorize(int num)
        {
            if (num <= 1)
            {
                throw new ArgumentException("num必须大于1");
            }

            List<int> factors = new List<int>();

            //i是因子，n是迭代除得到的商
            int n = num;
            for (int i = 2; i * i <= n; i++)
            {
                while (n % i == 0)
                {
                    factors.Add(i);
                    n = n / i;  //重复除以factor
                }
            }
            //添加剩余的商为素因子
            if (n != 1)
            {
                factors.Add(n);
            }

            return factors;
        }
    }
}
