using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form2 : Form
    {
        OrderService os;
        List<Client> c;
        List<Goods> g;
        int id;
        bool isClient;
        int findc(string n)
        {
            for(int i = 0; i < c.Count; i++)
            {
                if (c[i].name.Equals(n)) return i;
            }
            return -1;
        }
        int findg(string n)
        {
            for (int i = 0; i < g.Count; i++)
            {
                if (g[i].name.Equals(n)) return i;
            }
            return -1;
        }
        public Form2( OrderService os, List<Client> c)
        {
            this.os = os;
            InitializeComponent();
            this.c = c;
            label1.Text = "client";
            label2.Text = "id";
            for(int i = 0; i < c.Count; i++)
            {
                comboBox1.Items.Add(c[i].name);
            }
            isClient = true;
        }
        public Form2(OrderService os, List<Goods> g,int id)
        {
            this.os = os;
            InitializeComponent();
            this.g = g;
            label1.Text = "goods";
            label2.Text = "num";
            this.id = id;
            for (int i = 0; i < g.Count; i++)
            {
                comboBox1.Items.Add(g[i].name);
            }
            isClient = false;
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isClient)
                try
                {
                    os.add(new Order(c[findc(comboBox1.Text)], int.Parse(textBox2.Text)));
                }
                catch {
                    Form f1 = new Form3("add order wrong");
                    f1.Show();
                }
            else
                try
                {
                    Order otem = os.findByID(id);
                    otem.addDetials(new OrderDetails(g[findg(comboBox1.Text)], int.Parse(textBox2.Text), otem.orderdetails.Count+1));
                    os.revice(id, otem);
                
                }
                catch
                {
                    Form f1 = new Form3("add detail wrong");
                    f1.Show();
                }
            Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
