using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Registry_lol
{
    public partial class Form1 : Form
    {
        RegistryKey user = Registry.CurrentUser;
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 2000;
            timer1.Start(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistryKey myKey = user.CreateSubKey("LOL");
            myKey.SetValue("Somebody", "once told me");
            myKey.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            user.DeleteSubKey("LOL");
        }

        private int Monitoring()
        {
            object hkey = typeof(RegistryKey).InvokeMember(
                   "hkey",
                   BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic,
                   null,
                   this.user,
                   null
                   );
            IntPtr ptr = (IntPtr)typeof(SafeHandle).InvokeMember(
                   "handle",
                   BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic,
                   null,
                   hkey,
                   null);
            return RegNotifyChangeKeyValue(ptr, true, REG_NOTIFY_CHANGE.LAST_SET, IntPtr.Zero, false);
        }



        [DllImport("advapi32.dll")]
        private static extern int RegNotifyChangeKeyValue(
   IntPtr hKey,
   bool watchSubtree,
   REG_NOTIFY_CHANGE notifyFilter,
   IntPtr hEvent,
   bool asynchronous
   );
        [Flags]
        public enum REG_NOTIFY_CHANGE : uint
        {
            NAME = 0x1,
            ATTRIBUTES = 0x2,
            LAST_SET = 0x4,
            SECURITY = 0x8
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int change = Monitoring();
            if (change == 0)
            {
                textBox1.Text += "Произошло изменение" + "\r\n";
            }
        }
    }
}
