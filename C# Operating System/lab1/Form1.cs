using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form

    {
        static double Value;
        public Form1()
        {
            InitializeComponent();
        }

        /*private void label1_Click(object sender, EventArgs e)
        {

        }*/

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Value = Convert.ToDouble(textBox1.Text);
                textBox1.Text = Value.ToString();
            }
            catch
            {
                MessageBox.Show("Введи число или цифру, а не символ, идиот!");
            }
        }

        private void button2_Click(object sender, EventArgs e) // multiplication
        {
            ToolTip t = new ToolTip();

            Value *= 2;
            textBox1.Text = Value.ToString();
            if (t.Active)
            {
                t.SetToolTip(button2, "Пришел, пупсик :)");
                t.SetToolTip(button1, "Ушел от меня к другому, да? :(");
            }
            else
            {
                t.SetToolTip(button2, "Ушел от меня к другому, да? :(");
                t.SetToolTip(button1, "Пришел, пупсик :)");
            }
        }
        private void button1_Click(object sender, EventArgs e) // division
        {
            ToolTip t = new ToolTip();

            Value /= 2;
            if (Value == 0)
            {
                MessageBox.Show("Как ты собираешься делить на 0, идиот?");
            }

            textBox1.Text = Value.ToString();

            if (t.Active)
            {
                t.SetToolTip(button1, "Пришел, пупсик :)");
                t.SetToolTip(button2, "Ушел от меня к другому, да? :(");
            }
            else
            {
                t.SetToolTip(button1, "Ушел от меня к другому, да? :(");
                t.SetToolTip(button2, "Пришел, пупсик :)");
            }
        }

        private void Form1_MouseLeave_1(object sender, EventArgs e)
        {
            //MessageBox.Show("КУДА?!");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44)
            {
                e.Handled = true;
            }
        }
    }
}
