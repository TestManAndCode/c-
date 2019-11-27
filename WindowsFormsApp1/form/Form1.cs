using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.service;

namespace WindowsFormsApp1
{
    public partial class form_login : Form
    {
        LoginService loginService = new LoginService();
        public form_login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string loginName = this.loginName.Text;
            string password = this.password.Text;
            String message = loginService.login(loginName, password);
            MessageBox.Show(message);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void form_login_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) {
                notifyIcon1.Visible = true;
                this.Hide();
            }
        }

        private void form_login_Load(object sender, EventArgs e)
        {

        }
    }
}
