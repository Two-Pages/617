using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    public class Client
    {
        public string name { get; set; }
        public string telenum { get; set; }
        public Client(string n, string tn)
        {
            name = n;
            telenum = tn;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("name:：");
            sb.Append(name);
            sb.Append("/ttel：");
            sb.Append(telenum);
            return sb.ToString();
        }
    }
}
