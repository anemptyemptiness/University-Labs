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
    public partial class Form2 : Form
    {
        private string currentFolderPath = "C:\\";
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        // Метод, для очистки содержимого всех элементов управления
        protected void ClearAllFields()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        // Метод, для отображения содержимого папок в ListBox
        protected void DisplayFolderList(string folder)
        {
            DirectoryInfo di = new DirectoryInfo(folder);

            if (!di.Exists)
                throw new DirectoryNotFoundException("Папка не найдена :(");
            ClearAllFields();
            currentFolderPath = di.FullName;

            // Отображение списков всех подпапок и файлов
            foreach (DirectoryInfo d in di.GetDirectories())
                listBox1.Items.Add(d.Name);

            foreach (FileInfo f in di.GetFiles())
                listBox2.Items.Add(f.Name);
        }

        // Перемещение по папкам в листоксе
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItemPath = listBox1.SelectedItem.ToString();
            string fullPathName = Path.Combine(currentFolderPath, selectedItemPath);
            DisplayFolderList(fullPathName);
        }
        // Копирование папки
        void CopyDir(string FromDir, string ToDir)
        {
            Directory.CreateDirectory(ToDir);
            foreach (string s1 in Directory.GetFiles(FromDir))
            {
                string s2 = ToDir + "\\" + Path.GetFileName(s1);
                File.Copy(s1, s2);
            }
            foreach (string s in Directory.GetDirectories(FromDir))
            {
                CopyDir(s, ToDir + "\\" + Path.GetFileName(s));
            }
        }

        // Запуск листбокса
        private void button1_Click(object sender, EventArgs e)
        {
            ClearAllFields();

            DirectoryInfo dr = new DirectoryInfo(currentFolderPath);
            foreach (var d in dr.GetDirectories())
            {
                listBox1.Items.Add(d.Name);
            }
            foreach (var d in dr.GetFiles())
            {
                listBox2.Items.Add(d.Name);
            }
        }

        // Кнопка копирования папок
        string targetPathString;
        string sourcePathString;
        private void button2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Выберите папку, которую хотите скопировать";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    sourcePathString = dlg.SelectedPath;
                }
            }
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

        // Создать новую папку
        private void button3_Click(object sender, EventArgs e)
        {
            string newFolderName = textBox1.Text;

            if (!Directory.Exists(currentFolderPath))
            {
                Directory.CreateDirectory(currentFolderPath);
            }
            string newFolder = currentFolderPath + "\\" + newFolderName;
            Directory.CreateDirectory(newFolder);
            MessageBox.Show($"Каталог успешно создан в {newFolder}");
        }

        // Удаление папки
        private void button4_Click(object sender, EventArgs e)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(currentFolderPath);
            if (dirInfo.Exists)
            {
                dirInfo.Delete(true);
                MessageBox.Show("Каталог удален");
            }
            else
            {
                MessageBox.Show("Каталог не существует");
            }
        }

        // кнопка "Home"
        private void button5_Click(object sender, EventArgs e)
        {
            ClearAllFields();
            currentFolderPath = "C:\\";
            DirectoryInfo dr = new DirectoryInfo(currentFolderPath);
            foreach (var d in dr.GetDirectories())
            {
                listBox1.Items.Add(d.Name);
            }
            foreach (var d in dr.GetFiles())
            {
                listBox2.Items.Add(d.Name);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
