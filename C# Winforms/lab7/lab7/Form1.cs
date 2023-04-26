using System;
using System.Windows.Forms;

namespace lab7
{
    public partial class Form1 : Form
    {
        double X0 = 0;
        double Xk = 0;
        double hx = 0;
        double k = 0;
        public Form1()
        {
            InitializeComponent();

            if (k < 0)
            {
                MessageBox.Show("Константа введена некорректно!");
                return;
            }
            else
                button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            int j = 0;

            try
            {
                X0 = Double.Parse(textBox1.Text);
                Xk = Double.Parse(textBox2.Text);
                hx = Double.Parse(textBox3.Text);
                k = Double.Parse(textBox4.Text);
            }
            catch (Exception ex) { MessageBox.Show("Проверьте корректность ввода данных!"); return; }

            if (X0 > Xk)
            {
                MessageBox.Show("Левая граница Х не может быть больше правой");
                return;
            }

            int size = (int)((Xk - X0) / hx);
            if (size < 0 || size == 0)
            {
                MessageBox.Show("Некорректный ввод X0/Xk/hx");
                return;
            }

            if (k == 0 || k < 0)
            {
                MessageBox.Show("Значение константы k введено некорректно");
                return;
            }

            double[] y = new double[size];

            switch (listBox1.SelectedItem)
            {
                case "Sin":
                    chart1.Series[0].Points.Clear();

                    for (double i = X0; i < Xk; i += hx)
                    {
                        y[j] = Math.Sin(k * i);
                        chart1.Series[0].Points.AddXY(i, y[j]);

                        comboBox1.Items.Add("G1(" + X0.ToString() + ", " + k.ToString() + ") = " + y[j].ToString());

                        j++;
                    }
                    break;
                case "Cos":
                    chart1.Series[0].Points.Clear();

                    for (double i = X0; i < Xk; i += hx)
                    {
                        y[j] = Math.Cos(k * i);
                        chart1.Series[0].Points.AddXY(i, y[j]);

                        comboBox1.Items.Add("G1(" + X0.ToString() + ", " + k.ToString() + ") = " + y[j].ToString());

                        j++;
                    }
                    break;
                case "Tan":
                    chart1.Series[0].Points.Clear();

                    for (double i = X0; i < Xk; i += hx)
                    {
                        y[j] = Math.Tan(k * i);
                        chart1.Series[0].Points.AddXY(i, y[j]);

                        comboBox1.Items.Add("G1(" + X0.ToString() + ", " + k.ToString() + ") = " + y[j].ToString());

                        j++;
                    }
                    break;
                default:
                    MessageBox.Show("Что то пошло не так с первым графиком! Проверьте корректность ввода данных");   
                    break;
            }

            j = 0;
            switch (listBox2.SelectedItem)
            {
                case "Exp":
                    chart2.Series[0].Points.Clear();

                    for (double i = X0; i < Xk; i += hx)
                    {
                        y[j] = Math.Exp(k * i);
                        chart2.Series[0].Points.AddXY(i, y[j]);

                        comboBox1.Items.Add("G2(" + X0.ToString() + ", " + k.ToString() + ") = " + y[j].ToString());
                        
                        j++;
                    }
                    break;
                case "Ln":
                    chart2.Series[0].Points.Clear();

                    for (double i = X0; i < Xk; i += hx)
                    {
                        y[j] = Math.Log(k * i);
                        chart2.Series[0].Points.AddXY(i, y[j]);

                        comboBox1.Items.Add("G2(" + X0.ToString() + ", " + k.ToString() + ") = " + y[j].ToString());

                        j++;
                    }
                    break;
                case "Log":
                    chart2.Series[0].Points.Clear();

                    for (double i = X0; i < Xk; i += hx)
                    {
                        y[j] = Math.Log10(k * i);
                        chart2.Series[0].Points.AddXY(i, y[j]);

                        comboBox1.Items.Add("G2(" + X0.ToString() + ", " + k.ToString() + ") = " + y[j].ToString());

                        j++;
                    }
                    break;
                default:
                    MessageBox.Show("Что то пошло не так со вторым графиком! Проверьте корректность ввода данных");
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            comboBox1.Items.Clear();
        }
    }
}
