using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    //1. Изменить программу вывода функции так, чтобы можно было передавать функции типа double (double, double). 
    //Продемонстрировать работу на функции с функцией a * x ^ 2 и функцией a * sin(x).

    public delegate double Function(double x);
    class Program
    {
        static void Main(string[] args)
        {
            Table(Multi, 1, 10);
            Console.ReadKey();
            
        }
        public static void Table(Function Func, double first, double second)
        {
            Console.WriteLine("----- X ----- Y -----");
            while (first <= second)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", first, Func(first));
                first += 1;
            }
            Console.WriteLine("---------------------");
        }

        public static double Multi(double x)
        {
            return x * x * x;
        }
    }
}
