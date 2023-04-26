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

namespace lab2._1
{
    public partial class Form1 : Form
    {
        string currentFile = "";
        public Form1()
        {
            InitializeComponent();
            textBox1.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;

            button1.Click += button1_Click;
            button2.Click += button2_Click;
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*|Я хочу пиццу(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*|Я хочу пиццу(*.*)|*.*";
        }

        // Открытие файла
        void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                currentFile = openFileDialog1.FileName;
                textBox3.Text = currentFile;
                var fileText = File.ReadAllText(currentFile);
                textBox1.Text = fileText;
                textBox2.Text = fileText;
                MessageBox.Show("Файл открыт");
            }
            else
            {
                MessageBox.Show("Выбираем файл...", "Упс..!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Сохранение файла
        void button2_Click(object sender, EventArgs e)
        {
            /*try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // получаем выбранный файл
                var filename = saveFileDialog1.FileName;
                if (filename == null)
                    MessageBox.Show("Выберите файл");
                // сохраняем текст в файл
                File.WriteAllText(filename, textBox1.Text);
                File.WriteAllText(filename, textBox2.Text);
                MessageBox.Show("Файл сохранен");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }*/
        }

        // Удаление файла
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(currentFile);
                currentFile = "";
                textBox1.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Копирование файла
        private void button4_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Выберите папку";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string newPath = dlg.SelectedPath;
                    string fileName = textBox6.Text;
                    using (FileStream fs = File.Create(Path.Combine(newPath, fileName))) { }

                    File.Copy(currentFile, Path.Combine(newPath, fileName), true);
                }
            }
        }

        // Перемещение
        private void button5_Click(object sender, EventArgs e)
        {
            string MoveTo;
            string Path;

            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Выберите папку";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    MoveTo = dlg.SelectedPath;
                    Path = MoveTo;
                    MoveTo += "\\moved.txt";
                    if (File.Exists(MoveTo))
                    {
                        File.Delete(MoveTo);
                        MoveTo = Path + "\\moved.txt";
                        File.Move(currentFile, MoveTo);
                        textBox4.Text = MoveTo;
                    }
                    else
                    {
                        File.Move(currentFile, MoveTo);
                        textBox4.Text = MoveTo;
                    }
                }
            }
        }

        // Создание
        private void button6_Click(object sender, EventArgs e)
        {
            string newPath;
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Выберите папку";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    newPath = dlg.SelectedPath;
                    string fileName = textBox5.Text;

                    if (fileName == null) 
                    {
                        MessageBox.Show("Вы не ввели имя файла");
                    } else {
                        using (FileStream fs = File.Create(Path.Combine(newPath, fileName))) {}
                    }
                }
  
            }
        }

        // Выход из формы
        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 form2 = new Form2();
            form2.Close();
        }

        // Копирование папок -> выбрать папку, которую хотим скопировать
        string sourcePathString;
        private void button8_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Выберите папку, которую хотите скопировать";
                MessageBox.Show("Выберите папку, которую хотите скопировать");
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    sourcePathString = dlg.SelectedPath;
                    textBox7.Text = sourcePathString;
                }
            }
        }

        // А здесь мы копируем выбранный каталог в другой каталог
        string targetPathString;
        private void button9_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Выберите папку, куда хотите скопировать";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    targetPathString = dlg.SelectedPath;
                    MessageBox.Show("Каталог успешно скопирован");
                }
            }
            CopyFilesRecursively(sourcePathString, targetPathString);
        }

        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }

        // Куда переместился (для button_5)
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // some code
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // some code
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // some code
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // some code
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // some code
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            // some code
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }
    }
}
