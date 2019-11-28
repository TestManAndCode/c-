using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.utils;

namespace WindowsFormsApp1.form
{
    public partial class Print : Form
    {
        public Print()
        {
            InitializeComponent();
        }

        private void Print_Shown(object sender, EventArgs e)
        {
            List<string> list= LocalPrinter.GetLocalPrinters();
            foreach (String name in list) {
                listBox1.Items.Add(name);
            }
        }

        private void Print_Load(object sender, EventArgs e)
        {

        }
    }
}
