using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace task1
{
    public partial class Form1 : Form
    {
        DirectoryInfo currentDir = new DirectoryInfo(@"C:\Users\dneru\Desktop\Учеба\2 курс 2 сем\Васецкий C#\lab5\task1\task1");
        DirectoryInfo dirForGs = new DirectoryInfo(@"C:\Users\dneru\Desktop\Учеба\2 курс 2 сем\Васецкий C#\lab5\task1\task1\G#.dat");

        private string formatG = ".dat";
        private string formatLog = ".log";
        private string formatErr = ".log";
        private string formatRez = ".rez";

        int N = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void GWithSteps(double x0, double y0, int N, double step)
        {
            double[] x = new double[N];
            double[] y = new double[N];
            double[,] result = new double[N, N];
            string[,] countErrs = new string[N, N];

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

            // ------------------------------------------------------------------------------
            // Запишем всё в основной файл G_main.dat
            File.AppendAllText(dirForGs + "\\G_main" + formatG, "Рассчитываемая функция: y*2^ln(x)" + "\n"
                + "Количество точек х: " + (x.Length * y.Length).ToString() + "\n"
                + "Количество точек y: " + (x.Length * y.Length).ToString() + "\n\n");

            // G_main.dat
            for (int i = 0; i < x.Length + 1; i++)
            {
                for (int j = 0; j < y.Length + 1; j++)
                {
                    if (i == 0 && j == 0)
                        File.AppendAllText(dirForGs + "\\G_main" + formatG, "x\\y" + "\t\t");
                    if (j == 0 && i != 0)
                        File.AppendAllText(dirForGs + "\\G_main" + formatG, "y" + (i - 1).ToString() + "\t\t");
                    if (i == 0 && j != 0)
                        File.AppendAllText(dirForGs + "\\G_main" + formatG, "x" + (j - 1).ToString() + "\t\t");
                    if (j > 0 && i > 0)
                        File.AppendAllText(dirForGs + "\\G_main" + formatG, Math.Round(result[i - 1, j - 1], 3).ToString() + "\t\t");
                }

                File.AppendAllText(dirForGs + "\\G_main" + formatG, "\n");
            }

            // G####.dat
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < y.Length; j++)
                {
                    File.AppendAllText(dirForGs + "\\G" + i.ToString() + j.ToString() + formatG,
                            "Рассчитываемая функция: y*2^ln(x)" + "\n"
                            + "Количество точек х: " + (x.Length * y.Length).ToString() + "\n"
                            + "Количество точек y: " + (x.Length * y.Length).ToString() + "\n\n" + "x\\y" + "\t\t"
                            + "x" + j.ToString() + "\n" + "y" + i.ToString() + "\t\t" + Math.Round(result[i, j], 3).ToString());
                }
            }

            // myProgram.log
            DateTime dateTime = DateTime.Now;
            File.AppendAllText(currentDir + "\\myProgram" + formatLog, "Название программы: task1\n"
                        + "Дата и время начала выполнения расчёта: " + dateTime.ToString() + "\n");

            _x0 = x0;
            _y0 = y0;
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < y.Length; j++)
                {
                    File.AppendAllText(currentDir + "\\myProgram" + formatLog, "Рассчитываемая функция: " + _y0.ToString()
                        + "*2^ln(" + _x0.ToString() + ")" + "\n"
                        + "Файл, содержащий расчёт: G"
                        + i.ToString() + j.ToString() + "\n"
                        + "Результат: " + result[i, j].ToString() + "\n\n\n");

                    _y0 += step;
                }
                _x0 += step;
            }

            // myErrors.log
            _x0 = x0;
            _y0 = y0;
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < y.Length; j++)
                {
                    File.AppendAllText(currentDir + "\\myErrors" + formatErr, "Рассчитываемая функция: " + _y0.ToString()
                        + "*2^ln(" + _x0.ToString() + ")" + "\n"
                        + "Файл, содержащий расчёт: G" + i.ToString() + j.ToString() + "\n"
                        + "Аргумент х: " + _x0.ToString() + "\n"
                        + "Аргумент y: " + _y0.ToString() + "\n"
                        + "Тип ошибки: " + countErrs[i, j] + "\n\n\n");

                    _y0 += step;
                }

                _x0 += step;
            }

            // G#.rez
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < y.Length; j++)
                {
                    File.AppendAllText(dirForGs + "\\G" + formatRez, result[i, j].ToString() + "\t\t");
                }
                File.AppendAllText(dirForGs + "\\G" + formatRez, "\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // a) ФТх
            N = Convert.ToInt32(textBox1.Text);
            double x0 = Convert.ToDouble(textBox3.Text);
            double step = Convert.ToDouble(textBox4.Text);

            //arrayX = x;
            //arrayY = y;

            // б)
            GWithSteps(x0, x0, N, step);

            MessageBox.Show("Работа выполнена");

            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // в)
            List<string> arrayOfArrays = new List<string>();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    arrayOfArrays.Add("Читаем из файла G" + i.ToString() + j.ToString() + ":");
                    arrayOfArrays.Add(File.ReadAllText(dirForGs + "\\G" + i.ToString() + j.ToString() + ".dat"));
                    arrayOfArrays.Add("\n");
                }
            }

            arrayOfArrays.ForEach(Console.WriteLine);

            MessageBox.Show("Данные успешно прочитаны");
        }
    }
}
