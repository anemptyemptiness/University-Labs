using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using ZedGraph;
using System.Windows.Forms.DataVisualization.Charting;

namespace lab9
{
    public partial class Form1 : Form
    {
        private int arraySize = 10;
        private double k = 0.5;
        private int koef = 10;

        double[] xChart = new double[10];
        double[] yChart = new double[10];

        LineItem lineItem1;
        LineItem lineItem2;
        public Form1()
        {
            InitializeComponent();
            SetProperties();

            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;

            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;

            button2.Enabled = false;
            
            comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            comboBox4.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
        }

        private double Sin(double x)
        {
            return Math.Sin(x);
        }
        private void ScatterPlot(GraphPane pane)
        {
            PointPairList list = new PointPairList();
            double[] x = new double[arraySize];
            double[] ySin = new double[arraySize];

            for (int i = 0; i < x.Length; i++)
            {
                x[i] = i;
                ySin[i] = Sin(x[i]);
                list.Add(x[i], ySin[i]);
            }

            pane.CurveList.Clear();

            pane.Title.Text = "Вариант 21";
            pane.XAxis.Title.Text = "Ось X";
            pane.YAxis.Title.Text = "Ось Y";
            pane.XAxis.MajorGrid.IsVisible = true;
            pane.YAxis.MajorGrid.IsVisible = true;

            LineItem lineItem = pane.AddCurve("y = sin(x)", list, Color.Red, SymbolType.Diamond);
            lineItem.Line.IsVisible = false;
            lineItem.Symbol.Size = 10;
            lineItem1 = lineItem;

            zedGraphControl.AxisChange(); // обновляем данные об осях
            zedGraphControl.Invalidate(); // обновляем график
        }
        private void LinePlot(GraphPane pane)
        {
            PointPairList list = new PointPairList();
            double[] x = new double[arraySize];
            double[] ySin = new double[arraySize];

            for (int i = 0; i < x.Length; i++)
            {
                x[i] = i;
                ySin[i] = Sin(x[i]);
                list.Add(x[i], ySin[i]);
            }

            pane.CurveList.Clear();

            pane.Title.Text = "Вариант 21";
            pane.XAxis.Title.Text = "Ось X";
            pane.YAxis.Title.Text = "Ось Y";
            pane.XAxis.MajorGrid.IsVisible = true;
            pane.YAxis.MajorGrid.IsVisible = true;

            LineItem lineItem = pane.AddCurve("y = sin(x)", list, Color.Blue, SymbolType.None);
            lineItem2 = lineItem;
            

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void SurfacePlot()
        {
            chart1.Series[0].Points.Clear();

            for (int i = 0; i < arraySize; i++)
            {
                xChart[i] = koef;
                yChart[i] = k * xChart[i];
                chart1.Series[0].Points.AddXY(xChart[i], yChart[i]);

                koef += 10;
            }            

            ChartArea3DStyle chartArea3DStyle = new ChartArea3DStyle()
            {
                Enable3D = true
            };

            chart1.ChartAreas[0].Area3DStyle = chartArea3DStyle;

            chart1.Series[0].LegendText = "y = kx";
            chart1.Titles.Add("Вариант 21");
        }

        private void VisibleComboBox(ComboBox comboBox1, ComboBox comboBox2, ComboBox comboBox3, ComboBox comboBox4)
        {
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            comboBox3.Visible = true;
            comboBox4.Visible = true;
        }

        private void VisibleLabel(System.Windows.Forms.Label label1, System.Windows.Forms.Label label2,
            System.Windows.Forms.Label label3, System.Windows.Forms.Label label4)
        {
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ScatterPlot(zedGraphControl.GraphPane);
            LinePlot(zedGraphControl1.GraphPane);
            SurfacePlot();

            VisibleComboBox(comboBox1, comboBox2, comboBox3, comboBox4);
            VisibleLabel(label1, label2, label3, label4);

            button2.Enabled = true;
            button1.Enabled = false;
        }

        private void SetProperties()
        {
            comboBox1.Items.Add("Логарифмическая");
            comboBox1.Items.Add("Линейная");
            comboBox1.Items.Add("Текстовая");
            comboBox1.Items.Add("Экспонентная");

            comboBox2.Items.Add("Сплошная");
            comboBox2.Items.Add("Точечная");
            comboBox2.Items.Add("Штриховая");
            comboBox2.Items.Add("Штрихпунктирная");

            comboBox3.Items.Add("Красный");
            comboBox3.Items.Add("Зеленый");
            comboBox3.Items.Add("Синий");
            comboBox3.Items.Add("Черный");

            comboBox4.Items.Add("Красный");
            comboBox4.Items.Add("Зеленый");
            comboBox4.Items.Add("Синий");
            comboBox4.Items.Add("Черный");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                ChangeAxisType(zedGraphControl.GraphPane, zedGraphControl, ZedGraph.AxisType.Log);
                ChangeAxisType(zedGraphControl1.GraphPane, zedGraphControl1, ZedGraph.AxisType.Log);

                chart1.Series[0].Points.Clear();
                chart1.ChartAreas[0].AxisX.IsLogarithmic = true;
                chart1.ChartAreas[0].AxisY.IsLogarithmic = true;

                for (int i = 0; i < arraySize; i++)
                    chart1.Series[0].Points.AddXY(xChart[i], yChart[i]);
            }

            if (comboBox1.SelectedIndex == 1)
            {
                ChangeAxisType(zedGraphControl.GraphPane, zedGraphControl, ZedGraph.AxisType.Linear);
                ChangeAxisType(zedGraphControl1.GraphPane, zedGraphControl1, ZedGraph.AxisType.Linear);

                chart1.ChartAreas[0].AxisX.IsLogarithmic = false;
                chart1.ChartAreas[0].AxisY.IsLogarithmic = false;
            }

            if (comboBox1.SelectedIndex == 2)
            {
                ChangeAxisType(zedGraphControl.GraphPane, zedGraphControl, ZedGraph.AxisType.Text);
                ChangeAxisType(zedGraphControl1.GraphPane, zedGraphControl1, ZedGraph.AxisType.Text);

                if (tabControl1.SelectedIndex == 2)
                {
                    chart1.Series[0].Points.Clear();
                    for (int i = 0; i < arraySize; i++)
                        chart1.Series[0].Points.AddXY(xChart[i], yChart[i]);
                }
            }

            if (comboBox1.SelectedIndex == 3)
            {
                ChangeAxisType(zedGraphControl.GraphPane, zedGraphControl, ZedGraph.AxisType.Exponent);
                ChangeAxisType(zedGraphControl1.GraphPane, zedGraphControl1, ZedGraph.AxisType.Exponent);

                if (tabControl1.SelectedIndex == 2)
                {
                    chart1.Series[0].Points.Clear();
                    for (int i = 0; i < arraySize; i++)
                        chart1.Series[0].Points.AddXY(xChart[i], yChart[i]);
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeGraphLineType(zedGraphControl, zedGraphControl1, chart1);
        }

        public void ChangeGraphLineType(ZedGraphControl zedGraphControll, ZedGraphControl zedGraphControlll,
            System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                chart.Series[0].Points.Clear();

                lineItem1.Line.Style = DashStyle.Solid;
                lineItem2.Line.Style = DashStyle.Solid;
                chart.Series[0].ChartType = (SeriesChartType)ChartDashStyle.Solid;

                zedGraphControll.AxisChange();
                zedGraphControll.Invalidate();

                zedGraphControlll.AxisChange();
                zedGraphControlll.Invalidate();

                for (int i = 0; i < arraySize; i++)
                    chart.Series[0].Points.AddXY(xChart[i], yChart[i]);
            }

            else if (comboBox2.SelectedIndex == 1)
            {
                chart.Series[0].Points.Clear();

                lineItem1.Line.Style = DashStyle.Dot;
                lineItem2.Line.Style = DashStyle.Dot;
                chart.Series[0].ChartType = (SeriesChartType)ChartDashStyle.Dot;

                zedGraphControll.AxisChange();
                zedGraphControll.Invalidate();

                zedGraphControlll.AxisChange();
                zedGraphControlll.Invalidate();

                for (int i = 0; i < arraySize; i++)
                    chart.Series[0].Points.AddXY(xChart[i], yChart[i]);
            }

            else if (comboBox2.SelectedIndex == 2)
            {
                chart.Series[0].Points.Clear();

                lineItem1.Line.Style = DashStyle.Dash;
                lineItem2.Line.Style = DashStyle.Dash;
                chart.Series[0].ChartType = (SeriesChartType)ChartDashStyle.Dash;

                zedGraphControll.AxisChange();
                zedGraphControll.Invalidate();

                zedGraphControlll.AxisChange();
                zedGraphControlll.Invalidate();

                for (int i = 0; i < arraySize; i++)
                    chart.Series[0].Points.AddXY(xChart[i], yChart[i]);
            }

            else if (comboBox2.SelectedIndex == 3)
            {
                chart.Series[0].Points.Clear();

                lineItem1.Line.Style = DashStyle.DashDot;
                lineItem2.Line.Style = DashStyle.DashDot;
                chart.Series[0].ChartType = (SeriesChartType)ChartDashStyle.DashDot;

                zedGraphControll.AxisChange();
                zedGraphControll.Invalidate();

                zedGraphControlll.AxisChange();
                zedGraphControlll.Invalidate();

                for (int i = 0; i < arraySize; i++)
                    chart.Series[0].Points.AddXY(xChart[i], yChart[i]);
            }
        }

        private void ChangeAxisType(GraphPane pane, ZedGraphControl zedGraphControll, ZedGraph.AxisType axisType)
        {
            pane.XAxis.Type = axisType;
            pane.YAxis.Type = axisType; 

            zedGraphControll.AxisChange();
            zedGraphControll.Invalidate();
        }

        private void ChangeColorAxis(GraphPane pane, ZedGraphControl zedGraphControll, Color color,
            System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            pane.XAxis.Color = color;
            pane.YAxis.Color = color;

            chart.ChartAreas[0].AxisX.LineColor = color;
            chart.ChartAreas[0].AxisY.LineColor = color;

            zedGraphControll.AxisChange();
            zedGraphControll.Invalidate();
        }

        private void ChangeColorGrid(GraphPane pane, ZedGraphControl zedGraphControll, Color color,
            System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            pane.XAxis.MajorGrid.Color = color;
            pane.YAxis.MajorGrid.Color = color;

            chart.ChartAreas[0].AxisX.MajorGrid.LineColor = color;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = color;

            zedGraphControll.AxisChange();
            zedGraphControll.Invalidate();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                ChangeColorAxis(zedGraphControl.GraphPane, zedGraphControl, Color.Red, chart1);
                ChangeColorAxis(zedGraphControl1.GraphPane, zedGraphControl1, Color.Red, chart1);
            }

            if (comboBox3.SelectedIndex == 1)
            {
                ChangeColorAxis(zedGraphControl.GraphPane, zedGraphControl, Color.Green, chart1);
                ChangeColorAxis(zedGraphControl1.GraphPane, zedGraphControl1, Color.Green, chart1);
            }

            if (comboBox3.SelectedIndex == 2)
            {
                ChangeColorAxis(zedGraphControl.GraphPane, zedGraphControl, Color.Blue, chart1);
                ChangeColorAxis(zedGraphControl1.GraphPane, zedGraphControl1, Color.Blue, chart1);
            }

            if (comboBox3.SelectedIndex == 3)
            {
                ChangeColorAxis(zedGraphControl.GraphPane, zedGraphControl, Color.Black, chart1);
                ChangeColorAxis(zedGraphControl1.GraphPane, zedGraphControl1, Color.Black, chart1);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex == 0)
            {
                ChangeColorGrid(zedGraphControl.GraphPane, zedGraphControl, Color.Red, chart1);
                ChangeColorGrid(zedGraphControl1.GraphPane, zedGraphControl1, Color.Red, chart1);
            }

            if (comboBox4.SelectedIndex == 1)
            {
                ChangeColorGrid(zedGraphControl.GraphPane, zedGraphControl, Color.Green, chart1);
                ChangeColorGrid(zedGraphControl1.GraphPane, zedGraphControl1, Color.Green, chart1);
            }

            if (comboBox4.SelectedIndex == 2)
            {
                ChangeColorGrid(zedGraphControl.GraphPane, zedGraphControl, Color.Blue, chart1);
                ChangeColorGrid(zedGraphControl1.GraphPane, zedGraphControl1, Color.Blue, chart1);
            }

            if (comboBox4.SelectedIndex == 3)
            {
                ChangeColorGrid(zedGraphControl.GraphPane, zedGraphControl, Color.Black, chart1);
                ChangeColorGrid(zedGraphControl1.GraphPane, zedGraphControl1, Color.Black, chart1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            zedGraphControl.GraphPane.CurveList.Clear();
            zedGraphControl1.GraphPane.CurveList.Clear();
            chart1.Series[0].Points.Clear();
            chart1.Titles.Clear();
            chart1.Series[0].LegendText = "";

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;

            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();

            button2.Enabled = false;
            button1.Enabled = true;
        }
    }
}
