using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1_project
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/
            Class1 class1 = new Class1();
            uint[] Values = new uint[3]; 

            const float CFloat = Class1.floatValue;
            class1.longValue = 2;
            class1.ShortArray[0] = 2;
            uint Value1 = (uint)TypesOfNumbers.uintValue1;
            

            Console.WriteLine($"class1.longValue: {class1.longValue}");
            Console.WriteLine($"class1.ShortArray[0]: {class1.ShortArray[0]}");
            Console.WriteLine($"Class1.floatValue(constant): {CFloat}");
            Console.WriteLine($"uintValue1 from enum: {Value1}");
        }
    }
}
