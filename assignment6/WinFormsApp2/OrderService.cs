using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    public class OrderService
    {
        private List<Order> orders;
        public OrderService()
        {
            orders = new List<Order>();
        }
        public int getNum()
        {
            return orders.Count;
        }
        public List<Order> QueryAll()
        {
            return orders;
        }
        public void add(Order o)
        {
            int i;
            for (i = 0; i < orders.Count; i++)
            {
                if (orders[i].Equals(o))
                { throw new Exception("添加订单失败，订单号重复"); }
                if (orders[i].ID > o.ID) break;
            }
            orders.Insert(i, o);
        }
        public void dele(int id)
        {
            int idx = orders.FindIndex(o => o.ID == id);
            if (idx >= 0) orders.RemoveAt(idx);
        }
        public void revice(int id, Order otem)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].ID == id)
                {
                    orders[i] = otem;
                    orders[i].ID = id;
                    return;
                }
                //     if (orders[i].ID > id) break;
            }
            throw new Exception("修改失败，订单号不存在");
        }
        public Order findByID(int id)
        {
            return orders.FirstOrDefault(o => o.ID == id);
        }
        public List<Order> findByGname(string name)
        {
            var query = orders.Where(o => o.orderdetails.Any(d => d.goods.name == name))
                .OrderBy(o => o.price);
            return query.ToList();
        }
        public List<Order> findByCname(string name)
        {
            var query = orders.Where(o => o.client.name == name)
                .OrderBy(o => o.price);
            return query.ToList();
        }
        public List<Order> findByPrice(int price)
        {
            return (orders.Where(o => o.price >= price)
                .OrderBy(o => o.price)).ToList();
        }
    };
}
