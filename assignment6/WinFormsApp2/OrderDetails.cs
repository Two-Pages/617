using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    public class OrderDetails
    {
        public Goods goods;
        public int price;
        public int num;
        public int ID;
        public int Id { get { return ID; } set { ID = value; } }
        public int Num { get { return num; } set { num = value; } }
        public string Name { get { return goods.name; } }
        public int Price { get { return goods.price; } }
        public int tolPrice{get { return price; } }

        public OrderDetails(Goods g, int n, int iD)
        {
            goods = g;
            num = n;
            price = goods.price * num;
            ID = iD;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("购买");
            sb.Append(goods.name);
            sb.Append(num);
            sb.Append("件，");
            sb.Append("花费金额：");
            sb.Append(price);
            return sb.ToString();
        }
        public override bool Equals(object? obj)
        {
            OrderDetails od = obj as OrderDetails;
            return od.ID == ID;
        }
    }
}
