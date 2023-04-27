using System.Collections;
using System.Text;

namespace WinFormsCrawler
{
    public partial class Form1 : Form
    {
        List<string> rightList;
        List<string> wrongList;
        private string startUrl;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void inputURL(object sender, EventArgs e)
        {
            
        }

        private void start(object sender, EventArgs e)
        {
            try
            {
                startUrl = textBox1.Text;
                SimpleCrawler s = new SimpleCrawler(startUrl);
                while (s.noFinish) ;
                StringBuilder sbtem = new StringBuilder();
                sbtem.Append(s.right);
                sbtem.Append(s.wrong);
                textBox2.Text = sbtem.ToString();
            }
            catch(Exception ex)
            {
                textBox2.Text = "inputWrong";
            }
        }
    }
}