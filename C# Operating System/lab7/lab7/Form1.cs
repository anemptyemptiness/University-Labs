using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

namespace lab7
{
    public partial class Form1 : Form
    {
        private string path = @"C:\Users\dneru\Desktop\Учеба\2 курс 2 сем\Красильников C#\lab7\lab7\";
        private string format = ".txt";
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        [DllImport("user32.dll")]
        public static extern UInt32 GetWindowThreadProcessId(IntPtr hwnd, ref Int32 pid);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        public Form1()
        {
            IntPtr h = GetForegroundWindow();
            int processID = 0;
            GetWindowThreadProcessId(h, ref processID);
            Process process = Process.GetProcessById(processID);

            if (Thread.CurrentThread.Name == null)
                Thread.CurrentThread.Name = "MainThread";

            InitializeComponent();
            _hookID = SetHook(_proc);
            Log(DateTime.Now.ToString(), _proc.ToString(), process.MainWindowTitle, Thread.CurrentThread.Name);
            Application.Run();
            UnhookWindowsHookEx(_hookID); // выключение хука для избежания утечек память, связанных с удержанием ресурса, после завершения работы программы
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback( // метод для регистрации глобальных хуков в ОС Windows через WinAPI и user32.dll
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam); // перевод нажатой клавиши в int
                Console.WriteLine((Keys)vkCode); // вывод имени нажатой клавиши в консоль
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void Log(string date, string currrentEvent, string currentWindow, string currentThread)
        {
            File.WriteAllText(path + "log" + format, "Date: " + date + "\n"
                        + "Event: " + currrentEvent + "\n"
                        + "Active window: " + currentWindow + "\n"
                        + "Thread: " + currentThread);
        }
    }
}
