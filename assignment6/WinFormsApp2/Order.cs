using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    public class Order
    {
        public Client client;
        public int ID;
        public int price;
        public int Id { get { return ID; } set { ID = value; } }
        public int Price { get { return price; } set { price = value; } }
        public string Name { get { return client.name; } }
        private List<OrderDetails> orderdetail;
        public List<OrderDetails> orderdetails
        {
            get
            {
                return orderdetail;
            }
        }
        public Order(Client c, int id)
        {
            client = c;
            price = 0;
            ID = id;
            orderdetail = new List<OrderDetails>();
        }
        public void addDetials(OrderDetails od)
        {
            foreach (OrderDetails ods in orderdetail)
            {
                if (ods.Equals(od))
                { throw new Exception("添加订单明细失败，订单号重复"); }
            }
            orderdetail.Add(od);
            price += od.price;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(client.name);
            sb.Append("已经购买");
            sb.Append(price);
            sb.Append("金额的订单");
            return sb.ToString();
        }
        public List<OrderDetails> findByGname(string name)
        {
            var query = from o in orderdetail
                        where o.goods.name == name
                        orderby o.price
                        select o;
            return query.ToList();
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            Order od = obj as Order;
            return od.ID == ID;
        }
    }
}
