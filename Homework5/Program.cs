using System;
using System.Collections.Generic;
using System.Linq;

// 订单类
public class Order : IComparable<Order>
{
    // 订单号
    public int Id { get; set; }
    // 客户
    public string Customer { get; set; }
    // 订单明细列表
    public List<OrderDetail> Details { get; set; }

    // 构造函数
    public Order(int id, string customer, List<OrderDetail> details)
    {
        Id = id;
        Customer = customer;
        Details = details;
    }

    // 重写Equals方法，判断两个订单是否相等（根据订单号）
    public override bool Equals(object obj)
    {
        return obj is Order order && Id == order.Id;
    }

    // 重写GetHashCode方法，返回订单号的哈希值
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    // 重写ToString方法，显示订单信息
    public override string ToString()
    {
        return $"Order Id: {Id}, Customer: {Customer}, Total Amount: {GetTotalAmount()}";
    }

    // 实现IComparable接口，按照订单号进行比较
    public int CompareTo(Order other)
    {
        if (other == null) return 1;
        return Id.CompareTo(other.Id);
    }

    // 计算订单总金额
    public double GetTotalAmount()
    {
        return Details.Sum(d => d.GetSubTotal());
    }
}

// 订单明细类
public class OrderDetail
{
    // 商品名称
    public string ProductName { get; set; }
    // 商品单价
    public double UnitPrice { get; set; }
    // 商品数量
    public int Quantity { get; set; }

    // 构造函数
    public OrderDetail(string productName, double unitPrice, int quantity)
    {
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    // 重写Equals方法，判断两个订单明细是否相等（根据商品名称）
    public override bool Equals(object obj)
    {
        return obj is OrderDetail detail && ProductName == detail.ProductName;
    }

    // 重写GetHashCode方法，返回商品名称的哈希值
    public override int GetHashCode()
    {
        return ProductName.GetHashCode();
    }

    // 重写ToString方法，显示订单明细信息
    public override string ToString()
    {
        return $"Product Name: {ProductName}, Unit Price: {UnitPrice}, Quantity: {Quantity}, Subtotal: {GetSubTotal()}";
    }

    // 计算订单明细的小计金额
    public double GetSubTotal()
    {
        return UnitPrice * Quantity;
    }
}

// 订单服务类
// 订单服务类
public class OrderService
{
    // 订单列表
    private List<Order> orders;

    // 构造函数
    public OrderService()
    {
        orders = new List<Order>();
    }

    // 添加订单
    public void AddOrder(Order order)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));
        if (orders.Contains(order)) throw new ApplicationException("The order already exists.");
        orders.Add(order);
    }

    // 删除订单
    public void DeleteOrder(int orderId)
    {
        Order order = orders.Find(o => o.Id == orderId);
        if (order == null) throw new ApplicationException("The order does not exist.");
        orders.Remove(order);
    }

    // 修改订单
    public void UpdateOrder(Order order)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));
        int index = orders.FindIndex(o => o.Id == order.Id);
        if (index < 0) throw new ApplicationException("The order does not exist.");
        orders[index] = order;
    }

    // 查询订单（按照订单号）
    public Order GetOrderById(int orderId)
    {
        return orders.Find(o => o.Id == orderId);
    }

    // 查询订单（按照商品名称）
    public List<Order> GetOrdersByProductName(string productName)
    {
        return orders.Where(o => o.Details.Exists(d => d.ProductName == productName))
                     .OrderBy(o => o.GetTotalAmount())
                     .ToList();
    }

    // 查询订单（按照客户）
    public List<Order> GetOrdersByCustomer(string customer)
    {
        return orders.Where(o => o.Customer == customer)
                     .OrderBy(o => o.GetTotalAmount())
                     .ToList();
    }

    // 查询订单（按照订单金额）
    public List<Order> GetOrdersByAmount(double minAmount, double maxAmount)
    {
        return orders.Where(o => o.GetTotalAmount() >= minAmount && o.GetTotalAmount() <= maxAmount)
                     .OrderBy(o => o.GetTotalAmount())
                     .ToList();
    }

    // 排序订单（默认按照订单号排序）
    public void SortOrders()
    {
        orders.Sort();
    }

    // 排序订单（使用Lambda表达式进行自定义排序）
    public void SortOrders(Comparison<Order> comparison)
    {
        orders.Sort(comparison);
    }

}




class Program
{
    static void Main(string[] args)
    {
        // 创建一个订单服务对象
        OrderService orderService = new OrderService();

        // 创建一些订单明细对象
        OrderDetail detail1 = new OrderDetail("Apple", 5.0, 10);
        OrderDetail detail2 = new OrderDetail("Banana", 3.0, 20);
        OrderDetail detail3 = new OrderDetail("Orange", 4.0, 15);
        OrderDetail detail4 = new OrderDetail("Pear", 6.0, 12);

        // 创建一些订单对象，并添加到订单服务中
        Order order1 = new Order(1, "Alice", new List<OrderDetail>() { detail1, detail2 });
        Order order2 = new Order(2, "Bob", new List<OrderDetail>() { detail3, detail4 });
        Order order3 = new Order(3, "Charlie", new List<OrderDetail>() { detail1, detail4 });
        orderService.AddOrder(order1);
        orderService.AddOrder(order2);
        orderService.AddOrder(order3);

        // 显示所有订单信息
        Console.WriteLine("All orders:");
        foreach (Order order in orderService.GetOrdersByAmount(0, double.MaxValue))
        {
            Console.WriteLine(order);
            foreach (OrderDetail detail in order.Details)
            {
                Console.WriteLine(detail);
            }
            Console.WriteLine();
        }

        // 查询订单（按照订单号）
        Console.WriteLine("Order with id 2:");
        Console.WriteLine(orderService.GetOrderById(2));

        // 查询订单（按照商品名称）
        Console.WriteLine("Orders with product name Apple:");
        foreach (Order order in orderService.GetOrdersByProductName("Apple"))
        {
            Console.WriteLine(order);
        }

        // 查询订单（按照客户）
        Console.WriteLine("Orders with customer Bob:");
        foreach (Order order in orderService.GetOrdersByCustomer("Bob"))
        {
            Console.WriteLine(order);
        }

        // 查询订单（按照订单金额）
        Console.WriteLine("Orders with amount between 50 and 100:");
        foreach (Order order in orderService.GetOrdersByAmount(50, 100))
        {
            Console.WriteLine(order);
        }

        // 删除订单
        Console.WriteLine("Delete order with id 3:");
        orderService.DeleteOrder(3);

        // 修改订单
        Console.WriteLine("Update order with id 2:");
        Order order4 = new Order(2, "David", new List<OrderDetail>() { detail2, detail3 });
        orderService.UpdateOrder(order4);

    }
}
