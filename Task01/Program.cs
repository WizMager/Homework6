using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    //1. Изменить программу вывода функции так, чтобы можно было передавать функции типа double (double, double). 
    //Продемонстрировать работу на функции с функцией a * x ^ 2 и функцией a * sin(x).

    public delegate double FunctionOne(double x);
    public delegate double FunctionTwo(double x, double y);
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"Функция TeipleMulti (x * x * x):");
            Table(TripleMulti, 1, 7);
            Console.WriteLine();

            Console.WriteLine(@"Функция MultiDegree (a * x ^ 2):");
            Table(MultiDegree, 1, 5, 10);
            Console.WriteLine();

            Console.WriteLine(@"Функция MultiSin (a * sin(x)):");
            Table(MultiSin, 1, 2, 10);
            Console.ReadKey();
            
        }

        // first - a, second - x, numerator - до которого числа проверять
        public static void Table(FunctionTwo Func, double first, double second, double numerator)
        {
            Console.WriteLine("----- X ----- Y -----");
            while (first <= numerator)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", first, Func(first, second));
                first += 1;
            }
            Console.WriteLine("---------------------");
        }

        public static void Table(FunctionOne Func, double first, double numerator)
        {
            Console.WriteLine("----- X ----- Y -----");
            while (first <= numerator)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", first, Func(first));
                first += 1;
            }
            Console.WriteLine("---------------------");
        }

        public static double TripleMulti(double x)
        {
            return x * x * x;
        }
        public static double MultiDegree(double x, double y)
        {
            return x * (y * y);
        }

        public static double MultiSin(double x, double y)
        {
            return x * Math.Sin(y);
        }

    }
}
