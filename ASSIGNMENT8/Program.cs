using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OrderManagement
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=orderdb;user=root;password=yourpassword");
        }
    }

    public class Order
    {
        [Key]
        public int OrderNumber { get; set; }
        public string Customer { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }

        public Order(int orderNumber, string customer)
        {
            OrderNumber = orderNumber;
            Customer = customer;
            OrderDetails = new List<OrderDetails>();
        }

        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   OrderNumber == order.OrderNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderNumber);
        }

        public override string ToString()
        {
            return $"Order Number: {OrderNumber}, Customer: {Customer}";
        }
    }

    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }

        public double TotalAmount => Quantity * UnitPrice;

        public OrderDetails()
        {

        }

        public OrderDetails(string productName, int quantity, double unitPrice)
        {
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public override bool Equals(object obj)
        {
            return obj is OrderDetails details &&
                   ProductName == details.ProductName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductName);
        }

        public override string ToString()
        {
            return $"Product Name: {ProductName}, Quantity: {Quantity}, Unit Price: {UnitPrice}, Total Amount: {TotalAmount}";
        }
    }

    public class OrderService
    {
        private readonly OrderContext context;

        public OrderService(OrderContext context)
        {
            this.context = context;
        }

        public void AddOrder(Order order)
        {
            if (context.Orders.Any(o => o.OrderNumber == order.OrderNumber))
                throw new Exception("Order already exists!");
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void DeleteOrder(int orderNumber)
        {
            var order = context.Orders.Include(o => o.OrderDetails).FirstOrDefault(o => o.OrderNumber == orderNumber);
            if (order == null)
                throw new Exception("Order not found!");
            context.Orders.Remove(order);
            context.SaveChanges();
        }

        public IEnumerable<Order> QueryOrdersByOrderNumber(int orderNumber)
        {
            var query = from o in context.Orders.Include(o => o.OrderDetails)
                        where o.OrderNumber == orderNumber
                        orderby o.OrderDetails.Sum(d => d.TotalAmount) descending
                        select o;
            return query.ToList();
        }

        public IEnumerable<Order> QueryOrdersByProductName(string productName)
        {
            var query = from o in context.Orders.Include(o => o.OrderDetails)
                        where o.OrderDetails.Any(d => d.ProductName == productName)
                        orderby o.OrderDetails.Sum(d => d.TotalAmount) descending
                        select o;
            return query.ToList();
        }

        public IEnumerable<Order> QueryOrdersByCustomer(string customer)
        {
            var query = from o in context.Orders.Include(o => o.OrderDetails)
                        where o.Customer == customer
                        orderby o.OrderDetails.Sum(d => d.TotalAmount) descending
                        select o;
            return query.ToList();
        }

        public IEnumerable<Order> QueryOrdersByTotalAmount(double totalAmount)
        {
            var query = from o in context.Orders.Include(o => o.OrderDetails)
                        where o.OrderDetails.Sum(d => d.TotalAmount) == totalAmount
                        orderby o.OrderDetails.Sum(d => d.TotalAmount) descending
                        select o;
            return query.ToList();
        }
    }
}


