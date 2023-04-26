using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace coursach2
{
    public partial class Form1 : Form
    {
        private int loses = 0;
        private int wins = 0;
        Random random = new Random();

        List<string> iconsEasy = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        Label firstClicked, secondClicked;
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
            KeyPreview = true;
            KeyDown += new KeyEventHandler(Form1_Shortcut);

            MessageBox.Show("Чтобы ознакомиться с инструкцией, нажмите Ctrl+I\n" +
            "Чтобы перейти ко второй форме, нажмите Ctrl+N\n" +
            "Чтобы выйти из игры, нажмите Ctrl+L\n" +
            "Приятной игры!");

            ShowAndHideAll();
        }

        private void Form1_Shortcut(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                Form2 form2 = new Form2();
                this.Hide();
                form2.Show();

            }

            if (e.Control && e.KeyCode == Keys.I)
            {
                MessageBox.Show("Чтобы ознакомиться с инструкцией, нажмите Ctrl+I\n" +
                "Чтобы перейти ко второй форме, нажмите Ctrl+N\n" +
                "Чтобы выйти из игры, нажмите Ctrl+L\n" +
                "Приятной игры!");

            }

            if (e.Control && e.KeyCode == Keys.L)
            {
                this.Close();
            }
        }

        private void CheckForWinner()
        {
            Label label;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = tableLayoutPanel1.Controls[i] as Label;

                if (label != null && label.ForeColor == label.BackColor)
                    return;
            }

            loses -= 8;

            MessageBox.Show($"Вы промазали {loses} раз! Пройти игру можно за {wins - loses} ходов");

            GetStats();
            //Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label_Click1(object sender, EventArgs e)
        {
            if (firstClicked != null && secondClicked != null)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel == null)
                return;

            if (clickedLabel.ForeColor == Color.Black)
                return;

            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                loses++;
                return;
            }

            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Black;
            wins++;

            CheckForWinner();

            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
            }
            else
                timer1.Start();
        }

        private void AssignIconsToSquares()
        {
            Label label;
            int randomNumber; // случайный индекс

            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] is Label)
                {
                    label = (Label)tableLayoutPanel1.Controls[i];
                }
                else
                {
                    continue;
                }

                randomNumber = random.Next(0, iconsEasy.Count);
                label.Text = iconsEasy[randomNumber];

                iconsEasy.RemoveAt(randomNumber);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void GetStats()
        {
            // Helper.ClearFormControls(this);

            // TabControl using
            // строка состояние -> закинуть результаты loses и wins (StatusBar)
        }

        private void ShowAndHideAll()
        {
            Label label;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] is Label)
                {
                    label = (Label)tableLayoutPanel1.Controls[i];
                    label.ForeColor = Color.Black;
                }
            }

            Timer timer = new Timer();
            timer.Interval = 6000; // 6 секунд
            timer.Tick += (sender, args) =>
            {
                for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
                {
                    if (tableLayoutPanel1.Controls[i] is Label)
                    {
                        label = (Label)tableLayoutPanel1.Controls[i];
                        label.ForeColor = Color.CornflowerBlue;
                    }
                }
                timer.Stop();
            };
            timer.Start();
        }
    }

    class Helper
    {
        public static void ClearFormControls(Form form)
        {
            foreach (Control control in form.Controls)
            {
                if (control is Label)
                {
                    Label label = (Label)control;
                    label.Visible = false;
                }
                else if (control is TableLayoutPanel)
                {
                    TableLayoutPanel tableLayout = (TableLayoutPanel)control;
                    tableLayout.Visible = false;
                }
            }
        }
    }
}
