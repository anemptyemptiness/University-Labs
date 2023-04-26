using System;
using System.Windows.Forms;
using System.IO;

namespace lab2
{
    public partial class Form1 : Form
    {
        string path = "C:\\Users\\dneru\\Desktop\\Учеба\\2 курс 2 сем\\Васецкий C#\\lab2\\lab2\\Result.txt";

        decimal w = (decimal)0.351;
        double M = 100.205;
        double Tc = 540.2;
        double Pc = 27.0;
        decimal Experiment = (decimal)0.085;
        public Form1()
        {
            InitializeComponent();
        }
        double Tr(double Temperature)
        {
            return Temperature / Tc;
        }

        decimal ViscosityCritical()
        {
            return (decimal)Math.Pow(Tc, 0.166666) / (decimal)(Math.Pow(M, 0.5) * Math.Pow(Pc, 0.666666));
        }

        decimal Func_null(double Temperature)
        {
            return (decimal)(0.015174 - 0.02135 * Tr(Temperature)) + (decimal)(0.0075 * Math.Pow(Tr(Temperature), 2));
        }

        decimal Func_one(double Temperature)
        {
            return (decimal)(0.042552 - 0.07674 * Tr(Temperature)) + (decimal)(0.0340 * Math.Pow(Tr(Temperature), 2));
        }

        decimal LeruStill(double Temperature)
        {
            return (Func_null(Temperature) + w * Func_one(Temperature)) / ViscosityCritical();
        }

        // Write to Window
        private void button1_Click(object sender, EventArgs e)
        {
/*            decimal[] Result = new decimal[(250 - 200) / 2.5];
*/          double Temperature = Convert.ToDouble(textBox1.Text);
            decimal Error = (Math.Round(LeruStill(Temperature + 273.2), 3) - Experiment) / Experiment;

            DateTime dateNow = DateTime.Now;
            OperatingSystem os = Environment.OSVersion;
            Version ver = os.Version;

            string date = dateNow.ToString();
            string operatingSystem = os.ToString();
            string version = ver.ToString();

            MessageBox.Show($"Вязкость при {Temperature}°C равна {Math.Round(LeruStill(Temperature + 273.2), 3)} сП\n" +
                $"Ошибка при сравнении с экспериментальным значением составила {Math.Round(Error * 100, 1)}%\n" +
                $"Время последнего запуска программы: {date}\n" +
                $"Операционная система: {operatingSystem} ({version})");
        }

        // Write to File
        private void button2_Click(object sender, EventArgs e)
        {
            double Temperature = Convert.ToDouble(textBox1.Text); // Temperature in C to K
            decimal Error = (Math.Round(LeruStill(Temperature + 273.2), 3) - Experiment) / Experiment;

            DateTime dateNow = DateTime.Now;
            OperatingSystem os = Environment.OSVersion;
            Version ver = os.Version;

            string date = dateNow.ToString();
            string operatingSystem = os.ToString();
            string version = ver.ToString();

            File.WriteAllText(path, $"Вязкость при {Temperature}°C равна {Math.Round(LeruStill(Temperature + 273.2), 3)} сП\n" +
                $"Ошибка при сравнении с экспериментальным значением 0.085 сП составила {Math.Round(Error * 100, 1)}%\n" +
                $"Время последнего запуска программы: {date}\n" +
                $"Операционная система: {operatingSystem} ({version})");

            MessageBox.Show("Файл успешно сохранен");
        }
    }
}
