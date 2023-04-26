using System;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.Media;

namespace lab4
{
    public partial class Form1 : Form
    {
        SoundPlayer player = new SoundPlayer(@"C:\Users\dneru\Desktop\Учеба\2 курс 2 сем\Красильников C#\lab4\lab4\1.wav");

        System.Timers.Timer timer1 = null;
        int ms, s, m, h;
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form_Shortcut);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        // Шифруем/Дешифруем текст из TextBox 
        private void Form_Shortcut(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.E) // Ctrl+E - зашифровать
            {
                string textFromList = textBox1.Text;

                if (textFromList.Length != 0)
                {
                    var cipher = new CesarCipher();
                    var shift = 2;
                    var newTextForList = "";

                    newTextForList = cipher.Encrypt(textFromList, shift);
                    textBox1.Text = newTextForList;
                }

                e.SuppressKeyPress = true;
            }

            if (e.Control && e.KeyCode == Keys.D) // Ctrl+D - дешифровать
            {
                string textFromList = textBox1.Text;

                var decipher = new CesarCipher();
                var shift = 2;
                var newTextForList = "";

                newTextForList = decipher.Decrypt(textFromList, shift);
                textBox1.Text = newTextForList;

                e.SuppressKeyPress = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1 = new System.Timers.Timer();
            timer1.Interval = 1;
            timer1.Elapsed += MyTimer;
        }

        private void labelCounter_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartProcessTimer();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            h = 0;
            m = 0;
            s = 0;
            ms = 0;
            labelCounter.Text = "00:00:00:00";
            timer1.Stop();
        }

        private void MyTimer(object sender, ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                ms += 1;
                if (ms == 100)
                {
                    ms = 0;
                    s += 1;
                }
                if (s == 60)
                {
                    s = 0;
                    m += 1;
                }
                if (m == 60)
                {
                    m = 0;
                    h += 1;
                }
                labelCounter.Text = String.Format("{0}:{1}:{2}:{3}", h.ToString().ToString().PadLeft(2, '0'), m.ToString().ToString().PadLeft(2, '0'),
                    s.ToString().ToString().PadLeft(2, '0'), ms.ToString().ToString().PadLeft(2, '0'));
            }));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StartProcessMusic();
        }

        private void StartProcessMusic()
        {
            Thread music = new Thread(() =>
            {
                player.Play();
            })
            {
                IsBackground = false,
            };
            music.Start();
        }

        private void StartProcessTimer()
        {
            Thread timer = new Thread(() =>
            {
                timer1.Start();
            })
            {
                IsBackground = false,
            };
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    timer.Priority = ThreadPriority.Highest;
                    break;
                case 1:
                    timer.Priority = ThreadPriority.AboveNormal;
                    break;
                case 2:
                    timer.Priority = ThreadPriority.Normal;
                    break;
                case 3:
                    timer.Priority = ThreadPriority.BelowNormal;
                    break;
                case 4:
                    timer.Priority = ThreadPriority.Lowest;
                    break;
            }
            timer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class CesarCipher
    {
        const string alfabet_eng = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        
        private string CodeEncode(string text, int shift)
        {
            var fullAlfaber_eng = alfabet_eng + alfabet_eng.ToLower();
            var fullLength_eng = fullAlfaber_eng.Length;
            var newText = "";

            for (int i = 0; i < text.Length; i++)
            {
                var ch = text[i];
                var index_eng = fullAlfaber_eng.IndexOf(ch);
                if (index_eng < 0)
                {
                    newText += ch.ToString();
                }
                else
                {
                    var codeIndex_eng = (fullLength_eng + index_eng + shift) % fullLength_eng;
                    newText += fullAlfaber_eng[codeIndex_eng];
                }
            }

            return newText;
        }

        public string Encrypt(string mainText, int shift) => CodeEncode(mainText, shift);
        public string Decrypt(string decodeText, int shift) => CodeEncode(decodeText, -shift);
    }
}
