using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{

    class exercise1
    {
        static void Main(string[] args)
        {
            // 从控制台读取用户输入的数据
            Console.WriteLine("请输入一个正整数：");
            int n = int.Parse(Console.ReadLine());

            // 调用函数获取所有素数因子
            List<int> factors = GetPrimeFactors(n);

            // 输出结果
            Console.WriteLine("该数的所有素数因子为：");
            foreach (int factor in factors)
            {
                Console.Write(factor + " ");
            }
            Console.WriteLine();
        }

        // 定义一个函数，返回一个数的所有素数因子
        static List<int> GetPrimeFactors(int n)
        {
            // 创建一个列表存储结果
            List<int> factors = new List<int>();

            // 从2开始遍历所有可能的因子
            for (int i = 2; i <= n; i++)
            {
                // 如果i是n的因子，且i是素数，则添加到列表中
                if (n % i == 0 && IsPrime(i))
                {
                    factors.Add(i);
                }
            }

            // 返回结果列表
            return factors;
        }

        // 定义一个函数，判断一个数是否是素数
        static bool IsPrime(int n)
        {
            // 如果n小于等于1，返回false
            if (n <= 1)
            {
                return false;
            }

            // 从2开始遍历到n的平方根，如果有任何一个数能整除n，返回false
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            // 否则返回true
            return true;
        }
    }



    class exercise2
    {
        static void Main(string[] args)
        {
            // 定义一个整数数组，可以根据需要修改
            int[] arr = { 1, 2, 3, 4, 5 };

            // 调用函数求最大值、最小值、平均值和和
            int max = GetMax(arr);
            int min = GetMin(arr);
            double avg = GetAverage(arr);
            int sum = GetSum(arr);

            // 输出结果
            Console.WriteLine("数组的最大值是：" + max);
            Console.WriteLine("数组的最小值是：" + min);
            Console.WriteLine("数组的平均值是：" + avg);
            Console.WriteLine("数组的和是：" + sum);
        }

        // 定义一个函数，返回一个数组的最大值
        static int GetMax(int[] arr)
        {
            // 假设数组的第一个元素是最大值
            int max = arr[0];

            // 遍历数组，如果有比max更大的元素，更新max
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }

            // 返回max
            return max;
        }

        // 定义一个函数，返回一个数组的最小值
        static int GetMin(int[] arr)
        {
            // 假设数组的第一个元素是最小值
            int min = arr[0];

            // 遍历数组，如果有比min更小的元素，更新min
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }

            // 返回min
            return min;
        }

        // 定义一个函数，返回一个数组的平均值
        static double GetAverage(int[] arr)
        {
            // 调用函数求和
            int sum = GetSum(arr);

            // 计算平均值，注意要用double类型来避免整数除法的误差
            double avg = (double)sum / arr.Length;

            // 返回avg
            return avg;
        }

        // 定义一个函数，返回一个数组的和
        static int GetSum(int[] arr)
        {
            // 定义一个变量存储和，初始为0
            int sum = 0;

            // 遍历数组，把每个元素加到sum上
            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }

            // 返回sum
            return sum;
        }
    }



    class exercise3
    {
        static void Main(string[] args)
        {
            // 定义一个常量，表示范围的上限
            const int N = 100;

            // 创建一个布尔数组，表示每个数是否是素数，初始都为true
            bool[] isPrime = new bool[N + 1];
            for (int i = 0; i <= N; i++)
            {
                isPrime[i] = true;
            }

            // 从2开始遍历到N的平方根，如果某个数是素数，就把它的倍数都标记为false
            for (int i = 2; i * i <= N; i++)
            {
                if (isPrime[i])
                {
                    for (int j = i * i; j <= N; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }

            // 输出结果
            Console.WriteLine("2~" + N + "以内的素数有：");
            for (int i = 2; i <= N; i++)
            {
                if (isPrime[i])
                {
                    Console.Write(i + " ");
                }
            }
            Console.WriteLine();
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            // 定义一个矩阵，可以根据需要修改
            int[,] matrix = {
            { 1, 2, 3, 4 },
            { 5, 1, 2, 3 },
            { 9, 5, 1, 2 }
        };

            // 调用函数判断是否是托普利茨矩阵
            bool result = IsToeplitzMatrix(matrix);

            // 输出结果
            Console.WriteLine("该矩阵" + (result ? "是" : "不是") + "托普利茨矩阵");
        }

        // 定义一个函数，判断一个矩阵是否是托普利茨矩阵
        static bool IsToeplitzMatrix(int[,] matrix)
        {
            // 获取矩阵的行数和列数
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);

            // 遍历每一条由左上到右下的对角线，从第一列开始
            for (int i = 0; i < m; i++)
            {
                // 获取当前对角线的第一个元素
                int val = matrix[i, 0];

                // 遍历当前对角线的其他元素，如果有不同的，返回false
                for (int j = 1; j < n && i + j < m; j++)
                {
                    if (matrix[i + j, j] != val)
                    {
                        return false;
                    }
                }
            }

            // 遍历每一条由左上到右下的对角线，从第一行开始
            for (int j = 1; j < n; j++)
            {
                // 获取当前对角线的第一个元素
                int val = matrix[0, j];

                // 遍历当前对角线的其他元素，如果有不同的，返回false
                for (int i = 1; i < m && j + i < n; i++)
                {
                    if (matrix[i, i + j] != val)
                    {
                        return false;
                    }
                }
            }

            // 如果都没有不同的，返回true
            return true;
        }
    }
}

