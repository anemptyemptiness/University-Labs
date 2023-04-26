using System;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Timers;
using System.Threading;

namespace lab2
{
    public partial class Form1 : Form
    {
        private static System.Timers.Timer aTimer;
        public Form1()
        {
            InitializeComponent();
        }

        private static void SetTimer()
        {
            aTimer = new System.Timers.Timer(2000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            while (i < 40)
            {
                SetTimer();
                textBox1.Text += "The application started at" + DateTime.Now.ToString();

                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo d in allDrives)
                {
                    textBox1.Text += "\r\n";
                    textBox1.Text += "Disk names: " + d.Name.ToString();
                    textBox1.Text += "\r\n";
                    textBox1.Text += "Disk type: " + d.DriveType.ToString();
                    textBox1.Text += "\r\n";
                    textBox1.Text += "The amount of free disk space: " + d.TotalFreeSpace.ToString() + " bytes";
                }

                ManagementObjectSearcher ramMonitor =
                    new ManagementObjectSearcher("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem");

                textBox1.Text += "\r\n";

                foreach (ManagementObject objram in ramMonitor.Get())
                {
                    ulong totalRam = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
                    ulong busyRam = totalRam - Convert.ToUInt64(objram["TotalVisibleMemorySize"]);
                    textBox1.Text += "\r\n";
                    textBox1.Text += "Total-RAM is about " + totalRam.ToString() + " bytes";
                    textBox1.Text += "\r\n";
                    textBox1.Text += "Busy-RAM is about " + busyRam.ToString() + " bytes";
                    ulong result = busyRam * 100 / totalRam;
                    textBox1.Text += "\r\n";
                    textBox1.Text += "Occupied memory as a percentage: " + result.ToString() + "%";
                }

                aTimer.Stop();
                aTimer.Dispose();
                textBox1.Text += "\r\n";
                textBox1.Text += "Terminating the application...";
                textBox1.Text += "\r\n";
                textBox1.Clear();
                Thread.Sleep(500);
                i++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("C:\\Users\\dneru\\Desktop\\Учеба\\2 курс 2 сем\\Красильников C#\\lab2\\lab2\\Result.txt");
            SetTimer();
            sw.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                sw.WriteLine($"Disk names: {d.Name}");
                sw.WriteLine($"Disk type: {d.DriveType}");
                sw.WriteLine($"The amount of free disk space: {d.TotalFreeSpace / Math.Pow(2, 30)} Gbytes");
            }

            ManagementObjectSearcher ramMonitor =
                new ManagementObjectSearcher("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem");

            sw.WriteLine();


            foreach (ManagementObject objram in ramMonitor.Get())
            {
                ulong totalRam = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
                ulong busyRam = totalRam - Convert.ToUInt64(objram["TotalVisibleMemorySize"]);
                sw.WriteLine("Total-RAM is about {0} bytes", totalRam); // / Math.Pow(2, 30)
                sw.WriteLine("Busy-RAM is about {0} bytes", busyRam); // / Math.Pow(2, 30)
                sw.WriteLine("Occupied memory as a percentage: {0}%", (busyRam * 100) / totalRam);
            }

            sw.WriteLine("Terminating the application...");
            aTimer.Stop();
            aTimer.Dispose();
            sw.Close();
        }
    }
}
