using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.service;
using WindowsFormsApp1.utils;

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
            Boolean hasLogin=loginService.login(loginName, password);
            MessageBox.Show(hasLogin ? "登陆成功" : "登陆失败");
            if (hasLogin)
            {
                WindowState = FormWindowState.Minimized;
                PrintUtil.Myprinter(comboBox1.SelectedItem.ToString());

            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void form_login_Resize(object sender, EventArgs e)
        {
           
        }

        private void form_login_Load(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示    
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点
                this.Activate();
                //任务栏区显示图标
                this.ShowInTaskbar = true;
                //托盘区图标隐藏
                notifyIcon1.Visible = false;
            }
        }

        private void form_login_SizeChanged(object sender, EventArgs e)
        {
            //判断是否选择的是最小化按钮
            if (WindowState == FormWindowState.Minimized)
            {
                //隐藏任务栏区图标
                this.ShowInTaskbar = false;
                //图标显示在托盘区
                notifyIcon1.Visible = true;
            }
        }

        private void form_login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
        }

        private void 显示打印机器设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void form_login_Shown(object sender, EventArgs e)
        {
            List<string> list = LocalPrinter.GetLocalPrinters();
            foreach (String name in list)
            {
                comboBox1.Items.Add(name);
            }
            comboBox1.SelectedIndex = 0;
            Thread thread2 = new Thread(start);
            thread2.IsBackground = true;
            thread2.Start();
            Thread thread3 = new Thread(link);
            thread3.IsBackground = true;
            thread3.Start();
            
           

        }
        public static void link() {
            SuperSocketClient.link();
        }
        public static void start()
        {
            SuperSocketServer.Main(new string[] { });
        }
    }
}
