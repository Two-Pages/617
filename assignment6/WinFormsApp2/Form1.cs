using System.ComponentModel;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        int row=-1;
        BindingList<Order> o ;
        BindingList<OrderDetails> od;
        OrderService orderService = new OrderService();
        List<Client> c= new List<Client>();
        List<Goods> g = new List<Goods>();
        string fid = "find by id";
        string fgn = "find by gname";
        string fcn = "find by cname";
        string fpc = "find by price";
        public Form1()
        {
            InitializeComponent();

            Goods g1 = new Goods("g1", 5);
            Goods g2 = new Goods("g2", 3);
            Goods g3 = new Goods("g3", 10);
            Client c1 = new Client("c1", "234567");
            Client c2 = new Client("c2", "134567");

            c.Add(c1);
            c.Add(c2);
            g.Add(g1);
            g.Add(g2);
            g.Add(g3);

            Order o1 = new Order(c1, 1);
            o1.addDetials(new OrderDetails(g1, 2, 1));
            o1.addDetials(new OrderDetails(g2, 1, 2));

            Order o2 = new Order(c1, 2);
            o2.addDetials(new OrderDetails(g1, 1, 1));
            o2.addDetials(new OrderDetails(g2, 2, 2));

            Order o3 = new Order(c2, 3);
            o3.addDetials(new OrderDetails(g1, 4, 1));
            o3.addDetials(new OrderDetails(g3, 2, 2));

            Order o4 = new Order(c2, 4);
            o4.addDetials(new OrderDetails(g2, 2, 1));

            orderService.add(o1);
            orderService.add(o2);
            orderService.add(o3);
            orderService.add(o4);


            CName.DataPropertyName = "Name";
            ID.DataPropertyName = "Id";
            Price.DataPropertyName = "Price";
            ID_d.DataPropertyName = "Id";
            Gname_d.DataPropertyName = "Name";
            Num_d.DataPropertyName = "Num";
            Price_d.DataPropertyName = "Price";
            TolPrice_d.DataPropertyName = "tolPrice";
            o = new BindingList<Order>(orderService.QueryAll());
            
            comboBox1.Items.Add(fid);
            comboBox1.Items.Add(fgn);
            comboBox1.Items.Add(fcn);
            comboBox1.Items.Add(fpc);
            dataGridView1.DataSource = o;
            row = -1;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
              row= dataGridView1.CurrentRow.Index;
              od = new BindingList<OrderDetails>(o[row].orderdetails);
              dataGridView2.DataSource = od;

        }

        private void label1_Click(object sender, EventArgs e)
        {
         
        }

        private void buttonFind(object sender, EventArgs e)
        {
          
            try
            {
                if (comboBox1.Text.Equals(fid))
                {
                    o = new BindingList<Order> { orderService.findByID(int.Parse(textBox1.Text)) };
                }
                else if (comboBox1.Text.Equals(fgn))
                {
                    o = new BindingList<Order>(orderService.findByGname(textBox1.Text));
                }
                else if (comboBox1.Text.Equals(fpc))
                {
                    o = new BindingList<Order>(orderService.findByPrice(int.Parse(textBox1.Text)));
                }
                else if (comboBox1.Text.Equals(fcn)){
                    o = new BindingList<Order>(orderService.findByCname(textBox1.Text));
                }
            }
            catch 
            {
                o = new BindingList<Order>();
            }
            dataGridView1.DataSource = o;
            row = -1;
            dataGridView2.DataSource = new BindingList<OrderDetails>();
        }

        private void buttonAdd(object sender, EventArgs e)
        {
            Form form = new Form2(orderService,c);
            form.ShowDialog();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonTotalOrder(object sender, EventArgs e)
        {
            o = new BindingList<Order>(orderService.QueryAll());
            dataGridView1.DataSource = o;
            row = -1;
            dataGridView2.DataSource = new BindingList<OrderDetails>();
        }

        private void buttonDelete(object sender, EventArgs e)
        {
            Form form;
            if (row != -1)
            {
                string ss = dataGridView1.Rows[row].Cells[0].Value.ToString();
                int id = int.Parse(ss);
                orderService.dele(id);
                form = new Form3("delete success!");
            }
            else { form = new Form3("delete wrong!"); }
            form.ShowDialog();
        }

        private void buttonAddDetails(object sender, EventArgs e)
        {
            Form form;
            if (row != -1)
            {
                string ss = dataGridView1.Rows[row].Cells[0].Value.ToString();
                int id = int.Parse(ss);
                form = new Form2(orderService, g,id);
            }else
            {
                form = new Form3("no order selected");
            }
            form.ShowDialog();
        }
    }
}