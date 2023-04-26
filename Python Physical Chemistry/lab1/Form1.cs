using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace lab1
{
    public partial class Form1 : Form
    {
        // Инициализируем константы
        const int BUFF_SIZE = 7;
        const double R = 8.315;
        double[] HydroKoaf = { 0.23443029E+01, 0.79804248E-02, -0.19477917E-04, 0.20156967E-07, -0.73760289E-11, -0.91792413E+03 };
        double[] CarbonKoaf = { -0.31087207E+00, 0.44035369E-02, 0.19039412E-05, -0.63854697E-08, 0.29896425E-11, -0.10865079E+03 };
        double[] PropaneKoaf = { 4.21093013E+00, 1.70886504E-03, 7.06530164E-05, -9.20060565E-08, 3.64618453E-11, -1.43810883E+04 };

        public Form1()
        {
            InitializeComponent();

            // Задаем имена осям ХY
            Axis ax = new Axis();
            ax.Title = "T(K)";
            chart1.ChartAreas[0].AxisX = ax;
            Axis ay = new Axis();
            ay.Title = "ΔH(kJ/mol)";
            chart1.ChartAreas[0].AxisY = ay;

            chart1.ChartAreas[0].AxisY.Maximum = -110;
        }

        // Формула закона Гесса
        double GetEnthalpy(double EnthalpyHydro_1, double EnthalpyCarbon_1, double EnthalpyPropane_1)
        {
            return EnthalpyPropane_1 - ((4 * EnthalpyHydro_1) + (3 * EnthalpyCarbon_1));
        }

        // Формулa получения энтальпии веществ при разных значениях температуры
        double PolynomialNASA(int Temperature, double[] Koafs)
        {
            return (Koafs[0] + (Koafs[1] / 2) * Temperature + (Koafs[2] / 3) * Math.Pow(Temperature, 2)
                + (Koafs[3] / 4) * Math.Pow(Temperature, 3) + (Koafs[4] / 5) * Math.Pow(Temperature, 4)
                + (Koafs[5] / Temperature)) * (R * Temperature);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Очищаем график, если захотим несколько раз нажать кнопку "Get graph"
            this.chart1.Series[0].Points.Clear();

            // Инициализируем массивы температуры и энтальпий веществ
            int[] Temperature = new int[BUFF_SIZE] { 700, 650, 600, 550, 500, 450, 400 };
            double[] EnthalpyHydro = new double[BUFF_SIZE];
            double[] EnthalpyCarbon = new double[BUFF_SIZE];
            double[] EnthalpyPropane = new double[BUFF_SIZE];

            // Получаем энтальпию веществ при разных значениях температуры
            for (int i = 0; i < BUFF_SIZE; i++)
            {
                EnthalpyHydro[i] = PolynomialNASA(Temperature[i], HydroKoaf);
                EnthalpyCarbon[i] = PolynomialNASA(Temperature[i], CarbonKoaf);
                EnthalpyPropane[i] = PolynomialNASA(Temperature[i], PropaneKoaf);
            }

            // Переводим в Kj/mol
            for (int i = 0; i < BUFF_SIZE; i++)
            {
                EnthalpyHydro[i] /= 1000;
                EnthalpyCarbon[i] /= 1000;
                EnthalpyPropane[i] /= 1000;
            }

            // Строим график
            for (int i = 0; i < BUFF_SIZE; i++)
            {
                this.chart1.Series[0].Points.AddXY(Temperature[i], 
                    GetEnthalpy(EnthalpyHydro[i], EnthalpyCarbon[i], EnthalpyPropane[i]));
            }
        }
    }
}
