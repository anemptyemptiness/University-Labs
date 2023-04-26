using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace lab6
{
    public partial class Form1 : Form
    {
        Process[] processList = Process.GetProcesses();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hWnd, String lpString);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll")]
        private extern static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            button3.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            label1.Enabled = false;
            label2.Enabled = false;
            label5.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process[] processlist = Process.GetProcesses();

            listBox1.Items.Clear();

            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    listBox1.Items.Add(process.MainWindowTitle);
                }
            }

            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            label1.Enabled = true;
            label2.Enabled = true;
            label5.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        protected void ClearAllFields()
        {
            listBox1.Items.Clear();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) == true)
                throw new Exception();

            string name = Convert.ToString(listBox1.Items[listBox1.SelectedIndex]);
            Process[] processlist = Process.GetProcesses();
            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle) && (process.MainWindowTitle == name))
                {
                    SetWindowText(process.MainWindowHandle, textBox1.Text);
                }
            }

            Upload();
        }

        private void Upload()
        {
            ClearAllFields();
            Process[] processlist = Process.GetProcesses();
            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    listBox1.Items.Add(process.MainWindowTitle);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string name = Convert.ToString(listBox1.Items[listBox1.SelectedIndex]);

            Process[] processlist = Process.GetProcesses();
            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle) && (process.MainWindowTitle == name) && process.WaitForInputIdle(15000))
                {
                    SetWindowPos(process.MainWindowHandle, (IntPtr)(-1), 0, 0, Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text), 0x0020);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process[] processlist = Process.GetProcesses();
            foreach (Process process in processlist)
            {
                if (String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    listBox2.Items.Add(process.ProcessName);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
