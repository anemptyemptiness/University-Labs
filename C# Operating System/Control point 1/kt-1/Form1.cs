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
using Faker;

namespace kt_1
{
    public partial class Form1 : Form
    {
        private string currentFolderPath = @"C:\Users\dneru\Desktop\Учеба\2 курс 2 сем\Красильников C#\КР-1\kt-1";

        public Form1()
        {
            InitializeComponent();
        }
        // Метод, для отображения содержимого папок в листбокс
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

        // Метод, для очистки содержимого всех элементов управления
        protected void ClearAllFields()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }
        // Перемещение по папкам в листбоксе
        string fullPathName;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItemPath = listBox1.SelectedItem.ToString();
            fullPathName = Path.Combine(currentFolderPath, selectedItemPath);
            DisplayFolderList(fullPathName);
        }

        // Кнопка "Запуск"
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

        // Функция для изменения имени файла с помощью Faker, откуда я возьму генерацию случайного имени
        private void RenameTXT()
        {
            string[] names = new string[100];
            string format = ".txt";
            int index = 0;

            foreach (string item in listBox2.Items)
            {
                if (item.Contains(format))
                {
                    string newName = Faker.Name.First();
                    names[index] = item;
                    File.Move(currentFolderPath + "\\" + names[index], currentFolderPath + "\\" + newName + format);

                    index++;
                }
            }

            // Динамическое отображение изменения имени всех txt-файлов
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

        // Кнопка "Переименовать"
        private void button2_Click(object sender, EventArgs e)
        {
            RenameTXT();
        }
    }
}
