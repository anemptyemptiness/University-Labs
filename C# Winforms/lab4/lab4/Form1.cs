using System;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;


namespace lab5
{
    public partial class Form1 : Form
    {
        DirectoryInfo currentDir = new DirectoryInfo(@"C:\Users\dneru\Desktop\Учеба\2 курс 2 сем\Васецкий C#\lab5\lab5");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Regex regex = new Regex(@"\w*txt$", RegexOptions.RightToLeft);
            string findingSymbol = textBox2.Text;
            string format = null;

            // Проверка на пустой textBox или на пробелы в нём
            if (string.IsNullOrWhiteSpace(findingSymbol))
                throw new Exception();

            // Ищем все файлы, в которых содержится findingSymbol
            FileInfo[] files = currentDir.GetFiles("*" + findingSymbol + "*.*");

            foreach (var file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file.Name);
                Console.WriteLine("{0}", fileName);

                // Задаем формат
                if (format == null)
                    format = file.Extension;

                // 1-й пункт 30-го варианта
                if (!Directory.Exists(currentDir + "\\" + fileName.Substring(0, 1)))
                {
                    Directory.CreateDirectory(currentDir + "\\" + fileName.Substring(0, 1));
                }
                File.Copy(currentDir + "\\" + fileName + format, currentDir + "\\" + fileName.Substring(0, 1) + "\\" + fileName + format);

                // 3-й пункт 30-го варианта
                if (fileName.Length < 3)
                {
                    string newFolder = "\\TMP";
                    if (!Directory.Exists(currentDir + newFolder))
                    {
                        Directory.CreateDirectory(currentDir + newFolder);
                    }
                    
                    File.Copy(currentDir + "\\" + fileName + format, currentDir + newFolder + "\\" + fileName + format);
                }

                // 2-й пункт 30-го варианта
                if (fileName.Contains(textBox2.Text))
                {
                    if (!Directory.Exists(currentDir + "\\" + textBox2.Text.Substring(0, 3)))
                    {
                        Directory.CreateDirectory(currentDir + "\\" + textBox2.Text.Substring(0, 3));
                    }

                    File.Copy(currentDir + "\\" + fileName + format, currentDir + "\\" + textBox2.Text.Substring(0, 3) + "\\" + fileName + format);
                }
            }

            MessageBox.Show("Поиск завершен");
        }
    }
}
