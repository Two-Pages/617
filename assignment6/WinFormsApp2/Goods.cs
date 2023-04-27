using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    public class Goods
    {
        public string name { get; set; }
        public int price;
        public Goods(string n, int p)
        {
            name = n;
            price = p;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("货物名称：");
            sb.Append(name);
            sb.Append("/t货物单价：");
            sb.Append(price);
            return sb.ToString();
        }
    }
}
