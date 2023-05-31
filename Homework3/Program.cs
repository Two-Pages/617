using System;
using System.Collections.Generic;

// 定义一个抽象类Shape，表示形状
public abstract class Shape
{
    // 定义一个抽象属性Area，表示面积
    public abstract double Area { get; }

    // 定义一个抽象方法IsValid，判断形状是否合法
    public abstract bool IsValid();
}

// 定义一个类Rectangle，表示长方形，继承自Shape
public class Rectangle : Shape
{
    // 定义两个私有字段，表示长和宽
    private double length;
    private double width;

    // 定义一个构造函数，接受长和宽作为参数，并赋值给字段
    public Rectangle(double length, double width)
    {
        this.length = length;
        this.width = width;
    }

    // 重写Area属性，返回长乘以宽
    public override double Area
    {
        get { return length * width; }
    }

    // 重写IsValid方法，判断长和宽是否都大于0
    public override bool IsValid()
    {
        return length > 0 && width > 0;
    }
}

// 定义一个类Square，表示正方形，继承自Rectangle
public class Square : Rectangle
{
    // 定义一个构造函数，接受边长作为参数，并传递给基类的构造函数
    public Square(double side) : base(side, side)
    {

    }
}

// 定义一个类Triangle，表示三角形，继承自Shape
public class Triangle : Shape
{
    // 定义三个私有字段，表示三条边的长度
    private double a;
    private double b;
    private double c;

    // 定义一个构造函数，接受三条边的长度作为参数，并赋值给字段
    public Triangle(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    // 重写Area属性，使用海伦公式计算面积
    public override double Area
    {
        get
        {
            double p = (a + b + c) / 2; // 计算半周长
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c)); // 计算面积并返回
        }
    }

    // 重写IsValid方法，判断三条边是否满足三角形的条件
    public override bool IsValid()
    {
        return a > 0 && b > 0 && c > 0 && a + b > c && a + c > b && b + c > a;
    }
}

// 定义一个静态类ShapeFactory，表示形状工厂，用于创建形状对象
public static class ShapeFactory
{
    // 定义一个静态方法CreateShape，接受一个字符串作为参数，表示形状的类型，并返回一个Shape对象
    public static Shape CreateShape(string type)
    {
        // 使用随机数生成器创建一个Random对象
        Random random = new Random();

        // 根据类型的不同，创建不同的形状对象，并返回
        switch (type)
        {
            case "Rectangle": // 如果类型是长方形，则创建一个Rectangle对象，并随机生成长和宽（1到10之间的整数）
                return new Rectangle(random.Next(1, 11), random.Next(1, 11));
            case "Square": // 如果类型是正方形，则创建一个Square对象，并随机生成边长（1到10之间的整数）
                return new Square(random.Next(1, 11));
            case "Triangle": // 如果类型是三角形，则创建一个Triangle对象，并随机生成三条边的长度（1到10之间的整数）
                return new Triangle(random.Next(1, 11), random.Next(1, 11), random.Next(1, 11));
            default: // 如果类型是其他，则返回null
                return null;
        }

    }
}

// 定义一个类Program，表示程序入口类
class Program
{
    static void Main(string[] args)
    {
        // 创建一个List对象，用于存储形状对象
        List<Shape> shapes = new List<Shape>();

        // 创建一个字符串数组，用于存储形状的类型
        string[] types = { "Rectangle", "Square", "Triangle" };

        // 循环10次，每次随机选择一个类型，并调用ShapeFactory的CreateShape方法创建一个形状对象，并添加到List中
        for (int i = 0; i < 10; i++)
        {
            int index = new Random().Next(0, 3); // 随机生成一个索引（0到2之间的整数）
            string type = types[index]; // 根据索引获取对应的类型
            Shape shape = ShapeFactory.CreateShape(type); // 调用CreateShape方法创建形状对象

            shapes.Add(shape); // 将形状对象添加到List中

            Console.WriteLine("Created a {0} with area {1}", type, shape.Area); // 输出创建的形状的类型和面积

        }

        // 创建一个变量sum，用于存储所有形状的面积之和，并初始化为0
        double sum = 0;

        // 遍历List中的每个形状对象，并将其面积累加到sum中
        foreach (Shape shape in shapes)
        {
            sum += shape.Area;
        }

        Console.WriteLine("The total area of all shapes is {0}", sum); // 输出所有形状的面积之和

    }
}

