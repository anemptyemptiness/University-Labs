using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace coursach2
{
    public partial class Form2 : Form
    {
        private int loses = 0;
        private int wins = 0;
        Random random = new Random();

        List<string> iconsMedium = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z",
            "a", "a", "d", "d", "[", "[", "t", "t",
            "q", "q", "M", "M", ":", ":", "v", "v",
            "s", "s", "G", "G"
        };

        Label firstClicked, secondClicked;
        public Form2()
        {
            InitializeComponent();
            AssignIconsToSquares();
            KeyPreview = true;
            KeyDown += new KeyEventHandler(Form2_Shortcut);

            ShowAndHideAll();
        }

        private void Form2_Shortcut(object sender, KeyEventArgs e)
        {

            if (e.Control && e.KeyCode == Keys.N)
            {
                Form1 form1 = new Form1();
                this.Hide();
                form1.Show();

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
                Form1 form1 = new Form1();
                this.Close();
                form1.Show();
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

            loses -= 18;

            MessageBox.Show($"Вы промазали {loses} раз! Пройти игру можно за {wins - loses} ходов");

            Close();
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

                randomNumber = random.Next(0, iconsMedium.Count);
                label.Text = iconsMedium[randomNumber];

                iconsMedium.RemoveAt(randomNumber);
            }
        }

        private void label_Click(object sender, EventArgs e)
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

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
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
            timer.Interval = 10000; // 10 секунд
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
}
