using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab8
{
    public partial class Form1 : Form
    {
        private string path = @"C:\Users\dneru\Desktop\Учеба\2 курс 2 сем\Васецкий C#\lab8\lab8\matrix.txt";
        string[] matrix = new string[4];
        char[,] charArray = new char[4, 6];
        public Form1()
        {   
            InitializeComponent();

            int flag = 0;
            if (flag == 0)
            {
                LoadMatrix();
                flag++;
            }

            tabControl1.TabPages[0].Text = "Вкладка АДЫН";
            tabControl1.TabPages[1].Text = "Вкладка ДЫВА";

            comboBox1.Items.Add("Игнорировать числа");
            comboBox1.Items.Add("Перемещать циклически");

            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
        }

        private void LoadMatrix()
        {
            int j = 0;
            using (StreamReader reader = new StreamReader(path))
            {
                for (int i = 0; i < 4; i++)
                {
                    matrix[i] = reader.ReadLine();

                    foreach (var c in matrix[i])
                    {
                        if (c == ' ')
                            continue;
                        else
                            charArray[i, j] = c;
                        j++;
                    }
                    j = 0;
                }
            }
        }
        private void ShowMatrixAll()
        {
            string temp = "";

            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 6; k++)
                {
                    temp += charArray[i, k] + " ";
                }
                listBox1.Items.Add(temp);
                temp = "";
            }
        }

        private void PrintMatrix(string[] matrix)
        {
            listBox1.Items.Clear();

            string temp = "";

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    temp += charArray[i, j] + " ";
                }
                listBox1.Items.Add(temp);
                temp = "";
            }
        }

        private void ToLeftIgnore(char[,] charArray)
        {
            char[] temp = new char[6];

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 6; j++)
                    if (!isNumeric(charArray[i, j]))
                        temp[j] = charArray[i, j];

            // Сдвиг влево
            char tempp = temp[0];
            for (int i = 1; i < temp.Length; i++)
                temp[i - 1] = temp[i];

            temp[temp.Length - 1] = tempp;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 6; j++)
                    if (!isNumeric(charArray[i, j]))
                        charArray[i, j] = temp[j];
        }

        private void ToRightIgnore(char[,] charArray)
        {
            char[] temp = new char[6];

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 6; j++)
                    if (!isNumeric(charArray[i, j]))
                        temp[j] = charArray[i, j];

            // Сдвиг вправо
            char lastElement = temp[temp.Length - 1];
            Array.Copy(temp, 0, temp, 1, temp.Length - 1);
            temp[0] = lastElement;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 6; j++)
                    if (!isNumeric(charArray[i, j]))
                        charArray[i, j] = temp[j];
        }

        public static bool isNumeric(char c)
        {
            return Char.IsNumber(c);
        }

        private void ToRightCycling(char[,] charArray)
        {
            char lastCharRow0 = charArray[0, charArray.GetLength(1) - 1];
            char lastCharRow1 = charArray[1, charArray.GetLength(1) - 1];
            char lastCharRow2 = charArray[2, charArray.GetLength(1) - 1];
            char lastCharRow3 = charArray[3, charArray.GetLength(1) - 1];

            for (int row = 0; row < charArray.GetLength(0); row++)
            {
                for (int col = charArray.GetLength(1) - 1; col > 0; col--)
                {
                    charArray[row, col] = charArray[row, col - 1];
                }
            }

            charArray[0, 0] = lastCharRow0;
            charArray[1, 0] = lastCharRow1;
            charArray[2, 0] = lastCharRow2;
            charArray[3, 0] = lastCharRow3;
        }

        private void ToLeftCycling(char[,] charArray)
        {
            char firstCharRow0 = charArray[0, 0];
            char firstCharRow1 = charArray[1, 0];
            char firstCharRow2 = charArray[2, 0];
            char firstCharRow3 = charArray[3, 0];

            for (int row = 0; row < charArray.GetLength(0); row++)
            {
                for (int col = 1; col < charArray.GetLength(1); col++)
                {
                    charArray[row, col - 1] = charArray[row, col];
                }
            }

            charArray[0, charArray.GetLength(1) - 1] = firstCharRow0;
            charArray[1, charArray.GetLength(1) - 1] = firstCharRow1;
            charArray[2, charArray.GetLength(1) - 1] = firstCharRow2;
            charArray[3, charArray.GetLength(1) - 1] = firstCharRow3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                listBox1.Items.Clear();

                ToLeftIgnore(charArray);
                PrintMatrix(matrix);
            }

            if (comboBox1.SelectedIndex == 1)
            {
                listBox1.Items.Clear();

                ToLeftCycling(charArray);
                PrintMatrix(matrix);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                listBox1.Items.Clear();

                ToRightIgnore(charArray);
                PrintMatrix(matrix);
            }

            if (comboBox1.SelectedIndex == 1)
            {
                listBox1.Items.Clear();

                ToRightCycling(charArray);
                PrintMatrix(matrix);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }

            if (comboBox1.SelectedIndex == 1)
            {
                listBox1.Items.Clear();
                ShowMatrixAll();
            }

            if (comboBox1.SelectedIndex == 0)
            {
                listBox1.Items.Clear();
                ShowMatrixAll();
            }
        }
    }
}
