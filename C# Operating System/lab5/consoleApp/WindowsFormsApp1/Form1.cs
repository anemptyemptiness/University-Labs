using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using System.Reflection.Emit;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Add()
        {
            Assembly a = Assembly.Load("ClassLibrary1");
            Object o = a.CreateInstance("ClassLibrary1.Class1");
            Type t = a.GetType("ClassLibrary1.Class1");
            Object[] num = new Object[2];
            num[0] = Int32.Parse(textBox1.Text);
            num[1] = Int32.Parse(textBox2.Text);
            MethodInfo mi = t.GetMethod("add");
            object numNew = mi.Invoke(o, num);
            SetTextDelegate del = SetText;
            textBox1.BeginInvoke(del, numNew.ToString());
            
        }

        private delegate void SetTextDelegate(string msg);
        private void SetText(string msg)
        {
            MessageBox.Show(msg);
        }

        private void Sub()
        {
            Assembly a = Assembly.Load("ClassLibrary1");
            Object o = a.CreateInstance("ClassLibrary1.Class1");
            Type t = a.GetType("ClassLibrary1.Class1");
            Object[] num = new Object[2];
            num[0] = Int32.Parse(textBox1.Text);
            num[1] = Int32.Parse(textBox2.Text);
            MethodInfo mi = t.GetMethod("sub");
            object numNew = mi.Invoke(o, num);
            SetTextDelegate del = SetText;
            textBox1.BeginInvoke(del, numNew.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(Add);
            thread1.Start();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(Sub);
            thread1.Start();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}
