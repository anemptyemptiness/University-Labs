using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace lab6
{
    public partial class Form1 : Form
    {
        DirectoryInfo dirForGs = new DirectoryInfo(@"C:\Users\dneru\Desktop\Учеба\2 курс 2 сем\Васецкий C#\lab6\lab6\");

        private string formatRez = ".rez";
        //double[,] array = new double[,] { };
        int size;
        public Form1()
        {
            InitializeComponent();
            //button2.Enabled = false;
        }


        private void GWithSteps(double x0, double y0, int N, double step)
        {
            double[] x = new double[N];
            double[] y = new double[N];
            double[,] result = new double[N, N];
            string[,] countErrs = new string[N, N];

            size = N;
            y0 = x0;

            double _y0 = y0;
            double _x0 = x0;

            if (N < 4 || step < 1 || step > N)
                throw new Exception();
            else
            {
                try
                {
                    for (int i = 0; i < x.Length; i++)
                    {
                        for (int j = 0; j < y.Length; j++)
                        {
                            result[i, j] = Math.Round(_y0 * Math.Pow(2, Math.Log(_x0 - 1)), 2);

                            // Запись ошибки в массив ошибок countErrs
                            if (x0 - 1 < 0 || x0 - 1 == 0)
                            {
                                countErrs[i, j] = "ошибка отрицательного значения в логарифме";
                                result[i, j] = double.NaN;
                            }
                            else
                                countErrs[i, j] = "ошибок не обнаружено";

                            _y0 += step;
                        }

                        _x0 += step;
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }

            //array = result;

            // G.rez
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < y.Length; j++)
                {
                    File.AppendAllText(dirForGs + "G" + formatRez, result[i, j] + "\t");
                }
                File.AppendAllText(dirForGs + "G" + formatRez, "\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int N = Convert.ToInt32(textBox1.Text);
            double x0 = Convert.ToDouble(textBox3.Text);
            double step = Convert.ToDouble(textBox4.Text);

            GWithSteps(x0, x0, N, step);

            MessageBox.Show("Работа выполнена");

            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            if (size == 0 || size < 0)
                throw new Exception();

            string path = dirForGs + "G" + ".rez";

            foreach (var val in File.ReadAllLines(path))
            {
                listBox1.Items.Add(val);
            }

            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            double[,] array = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                string[] values = lines[i].Split('\t');
                for (int j = 0; j < size; j++)
                {
                    array[i, j] = Double.Parse(values[j]);
                }
            }

            int topLeftRow = Int32.Parse(textBox5.Text);
            int topLeftColumn = Int32.Parse(textBox6.Text);
            int bottomRightRow = Int32.Parse(textBox2.Text);
            int bottomRightColumn = Int32.Parse(textBox7.Text);

            if (topLeftRow > size || topLeftColumn > size || bottomRightColumn > size || bottomRightRow > size)
                throw new Exception();

            if (topLeftRow < 0 || topLeftColumn < 0 || bottomRightColumn < 0 || bottomRightRow < 0)
                throw new Exception();

            double[,] subMatrix = new double[bottomRightRow - topLeftRow, bottomRightColumn - topLeftColumn];
            int k = 0;
            int m = 0;

            for (int i = topLeftRow; i < bottomRightRow; i++)
            {
                for (int j = topLeftColumn; j < bottomRightColumn; j++)
                {
                    subMatrix[k, m] = array[i, j];
                    m++;
                }
                m = 0;
                k++;
            }

            ArrayToMessageBox(subMatrix);
        }

        private void ArrayToMessageBox(double[,] array)
        {
            string message = "";
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    message += array[i, j] + " ";
                }
                message += "\n";
            }
            MessageBox.Show(message);
        }
    }
}
