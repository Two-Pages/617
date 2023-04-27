using System;
namespace project1
{
    public class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Data { get; set; }
        public Node(T t)
        {
            Next = null;
            Data = t;
        }
    }
    public class GenericList<T>
    {
        private Node<T> head;
        private Node<T> tail;
        public GenericList()
        {
            tail = head = null;
        }
        public Node<T> Head { get => head; }
        public void Add(T t)
        {
            Node<T> n = new Node<T>(t);
            if (tail == null)
            {
                head = tail = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
        }
        public void Foreach(Action<T> action)
        {
            Node<T> n = head;
            while (n != null)
            {
                action(n.Data);
                n = n.Next;
            }
        }
        public void sort(Func<T,T,bool> function)
        {
            Node<T> n1, n2;
            n1 =head;
            while (n1 != null)
            {
                n2 = n1.Next;
                while(n2 != null)
                {
                    if (function(n1.Data, n2.Data))
                    {
                        T tem = n1.Data;
                        n1.Data = n2.Data;
                        n2.Data = tem;
                    }
                    n2 = n2.Next;
                }
                n1 = n1.Next;
            }
        }
    }
    public class program
    {
        public static void Main(string[] args)
        {
            GenericList<int> list = new GenericList<int>();
            for(int i = 1; i <= 10; i++)
            {
                list.Add(i);
            }
            int max = 0, min = 99999, sum = 0;
            list.Foreach(x=> { 
                max = (max < x) ? x : max;
                min = (min > x) ? x : min;
                sum += x;
            });
            Console.WriteLine("最小值为" + min);
            Console.WriteLine("最大值为" + max);
            Console.WriteLine("总和为" + sum);

            Console.WriteLine("排序前序列：");
            list.Foreach(x => Console.Write(x + " "));
            list.sort((x, y) => x <y);
            Console.WriteLine();
            Console.WriteLine("排序后序列：");
            list.Foreach(x => Console.Write(x + " "));
        }
    }
}

