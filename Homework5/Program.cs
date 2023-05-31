using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// 定义客户类
class Customer
{
    public string Name { get; set; } // 客户姓名
    public string Address { get; set; } // 客户地址

    public Customer(string name, string address)
    {
        Name = name;
        Address = address;
    }

    // 重写ToString方法，显示客户信息
    public override string ToString()
    {
        return $"Customer: {Name}, Address: {Address}";
    }

    // 重写Equals方法，判断两个客户是否相同（根据姓名和地址）
    public override bool Equals(object obj)
    {
        return obj is Customer customer &&
               Name == customer.Name &&
               Address == customer.Address;
    }

    // 重写GetHashCode方法，根据姓名和地址生成哈希码
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Address);
    }
}

// 定义货物类
class Product
{
    public string Name { get; set; } // 货物名称
    public double Price { get; set; } // 货物单价

    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }

    // 重写ToString方法，显示货物信息
    public override string ToString()
    {
        return $"Product: {Name}, Price: {Price}";
    }

    // 重写Equals方法，判断两个货物是否相同（根据名称和单价）
    public override bool Equals(object obj)
    {
        return obj is Product product &&
               Name == product.Name &&
               Price == product.Price;
    }

    // 重写GetHashCode方法，根据名称和单价生成哈希码
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Price);
    }
}

// 定义订单明细类
class OrderDetail
{
    public Product Product { get; set; } // 订单明细中的货物
    public int Quantity { get; set; } // 订单明细中货物的数量

    public OrderDetail(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    // 计算订单明细的金额
    public double Amount
    {
        get => Product.Price * Quantity;
    }

    // 重写ToString方法，显示订单明细信息
    public override string ToString()
    {
        return $"OrderDetail: {Product}, Quantity: {Quantity}, Amount: {Amount}";
    }

    // 重写Equals方法，判断两个订单明细是否相同（根据货物和数量）
    public override bool Equals(object obj)
    {
        return obj is OrderDetail detail &&
               Product.Equals(detail.Product) &&
               Quantity == detail.Quantity;
    }

    // 重写GetHashCode方法，根据货物和数量生成哈希码
    public override int GetHashCode()
    {
        return HashCode.Combine(Product, Quantity);
    }
}

// 定义订单类
class Order : IComparable<Order>
{
    public int Id { get; set; } // 订单号
    public Customer Customer { get; set; } // 订单的客户

    // 使用HashSet存储订单明细，避免重复
    private HashSet<OrderDetail> details = new HashSet<OrderDetail>();

    // 添加订单明细
    public void AddDetail(OrderDetail detail)
    {
        if (details.Contains(detail))
        {
            throw new ApplicationException($"The order detail already exists!");
        }
        details.Add(detail);
    }

    // 删除订单明细
    public void RemoveDetail(OrderDetail detail)
    {
        details.Remove(detail);
    }

    // 计算订单的总金额
    public double TotalAmount
    {
        get => details.Sum(d => d.Amount);
    }

    // 重写ToString方法，显示订单信息
    public override string ToString()
    {
        string result = $"Order: {Id}, Customer: ({Customer}), TotalAmount: {TotalAmount}\n";
        foreach (var detail in details)
        {
            result += $"{detail}\n";
        }
        return result;
    }

    // 重写Equals方法，判断两个订单是否相同（根据订单号）
    public override bool Equals(object obj)
    {
        return obj is Order order &&
               Id == order.Id;
    }

    // 重写GetHashCode方法，根据订单号生成哈希码
    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    // 实现IComparable接口，定义默认的比较方式（根据订单号）
    public int CompareTo(Order other)
    {
        if (other == null) return 1;
        return Id.CompareTo(other.Id);
    }
}

// 定义订单服务类
class OrderService
{
    // 使用List存储订单数据
    private List<Order> orders = new List<Order>();

    // 添加订单
    public void AddOrder(Order order)
    {
        if (orders.Contains(order))
        {
            throw new ApplicationException($"The order already exists!");
        }
        orders.Add(order);
    }

    // 删除订单
    public void DeleteOrder(Order order)
    {
        orders.Remove(order);
    }

    // 修改订单（先删除后添加）
    public void UpdateOrder(Order oldOrder, Order newOrder)
    {
        DeleteOrder(oldOrder);
        AddOrder(newOrder);
    }

    // 查询所有的订单（按照总金额排序）
    public List<Order> QueryAllOrders()
    {
        var query = orders.OrderBy(o => o.TotalAmount);
        return query.ToList();
    }

    // 根据订单号查询订单（返回单个订单或者null）
    public Order QueryById(int id)
    {
        var query = orders.Where(o => o.Id == id);
        return query.FirstOrDefault();
    }

    // 根据商品名称查询订单（返回一个订单列表）
    public List<Order> QueryByProductName(string name)
    {
        var query = orders.Where(o => o.details.Any(d => d.Product.Name == name)).OrderBy(o => o.TotalAmount);
        return query.ToList();
    }

    // 根据客户查询订单（返回一个订单列表）
    public List<Order> QueryByCustomerName(string name)
    {
        var query = orders.Where(o => o.Customer.Name == name).OrderBy(o => o.TotalAmount);
        return query.ToList();
    }

    // 根据订单金额查询订单（返回一个订单列表）
    public List<Order> QueryByAmount(double amount)
    {
        var query = orders.Where(o => o.TotalAmount == amount).OrderBy(o => o.TotalAmount);
        return query.ToList();
    }

    // 使用Lambda表达式进行自定义排序
    public void Sort(Comparison<Order> comparison)
    {
        orders.Sort(comparison);
    }
}

// 测试程序类
class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("Welcome to the order management system!");

        OrderService service = new OrderService();

        Customer customer1 = new Customer("张三", "北京");
        Customer customer2 = new Customer("李四", "上海");
        Customer customer3 = new Customer("王五", "广州");

        Product product1 = new Product("电脑", 5000.0);
        Product product2 = new Product("手机", 3000.0);
        Product product3 = new Product("书籍", 100.0);

        Order order1 = new Order() { Id = 1, Customer = customer1 };
        order1.AddDetail(new OrderDetail(product1, 1));
        order1.AddDetail(new OrderDetail(product3, 10));

        Order order2 = new Order() { Id = 2, Customer = customer2 };
        order2.AddDetail(new OrderDetail(product2, 2));
        order2.AddDetail(new OrderDetail(product3, 5));

        Order order3 = new Order() { Id = 3, Customer = customer3 };
        order3.AddDetail(new OrderDetail(product1, 2));
        order3.AddDetail(new OrderDetail(product2, 1));

        service.AddOrder(order1);
        service.AddOrder(order2);
        service.AddOrder(order3);


        Console.WriteLine("All the orders are:");

        List<Order> orders = service.QueryAllOrders();

        foreach (var order in orders)
        {
            Console.WriteLine(order);

            Console.WriteLine("-----------------");

        }
    }
}