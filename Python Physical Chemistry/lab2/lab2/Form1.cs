using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form1 : Form
    {
        const double R = 8.315; // Газовая постоянная
        const double T = 250; // Температура (K)
        // Низкотемпературные коэффициенты
        double[] Koafs = { 2.06484531E+00, 2.06827764E-02, 5.54675716E-05, -9.75079697E-08, 4.31809897E-11, 1.78174435E+01 };
        public Form1()
        {
            InitializeComponent();
        }
        
        // Функция для расчета энтропии вещества при 250К
        double PolynomialNASA(double Temperature, double[] Koafs)
        {
            return (Koafs[0] * Math.Log(Temperature) + Koafs[1] * Temperature + (Koafs[2] / 2) * Math.Pow(Temperature, 2) +
                (Koafs[3] / 3) * Math.Pow(Temperature, 3) + (Koafs[4] / 4) * Math.Pow(Temperature, 4) + Koafs[5]) * R;
        }

        // Задача 1
        // Вывод результата расчета на экран
        // Вариант 31
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Entropy result is: {Math.Round(PolynomialNASA(T, Koafs), 5)} Дж/К"); // Вывод результата на экран
        }

        // Задача 2
        // Расчет энтропии и вывод результата на экран
        // Вариант 6
        private void button2_Click(object sender, EventArgs e)
        {
            double Entropy; // Инициализация переменной, куда запишется результат расчета по формуле из лекции
            double Va = 2E-04; // Объем газа А
            double Vb = 3E-04; // Объем газа В
            double P = 1.01E+05;
            double T = 298;

            double mol_a = P * Va / R * T; // Расчет количества моль для газа А
            double mol_b = P * Vb / R * T; // Расчет количества моль для газа В
            Entropy = mol_a * R * Math.Log((Va + Vb) / Va) + mol_b * R * Math.Log((Va + Vb) / Vb); // Расчет энтропии
            MessageBox.Show($"Answer of this exercise is: {Math.Round(Entropy, 3)} Дж/К"); // Вывод результата на экран
        }
    }
}
