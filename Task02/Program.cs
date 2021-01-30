using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02
{
    //    2. Модифицировать программу нахождения минимума функции так, чтобы можно было передавать функцию в виде делегата.
    //       а) Сделайте меню с различными функциями и предоставьте пользователю выбор, для какой функции и на каком отрезке находить минимум.
    //       б) Используйте массив(или список) делегатов, в котором хранятся различные функции.
    //       в) * Переделайте функцию Load, чтобы она возвращала массив считанных значений.Пусть она возвращает минимум через параметр.
    class Program
    {
        public delegate double Function(double x);

        static void Main(string[] args)
        {
            #region Task 2.2
            List<Function> functions = new List<Function>() { DefaultFunc,Func1, Func2, Func3};
            #endregion

            string path = AppDomain.CurrentDomain.BaseDirectory + "SveFile.txt";
            UserInput(path, functions);
            //SaveFunc(path, Func1, -100, 100, 1);
            List<double> listOfValues = new List<double>(Load(path, out double minValue));
            PrintList(listOfValues);
            Console.WriteLine();
            Console.WriteLine($"Минимальное значение: {minValue}.");
            Console.ReadKey();
        }

        #region
        static void UserInput(string path, List<Function> functions)
        {
            int funcNumb;
            double start;
            double end;
            double step;
            Console.WriteLine("Введите номер функции, которую вы хотите использовать.");
            Console.WriteLine(@"1: x^2 - 50x + 10");
            Console.WriteLine(@"2: x^2 - 10x + 7");
            Console.WriteLine(@"3: 3(x^2) - 11x + 3");
            funcNumb = int.Parse(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Введите промежуток, на котором вы хотите вычислять и с каким шагом.");
            Console.Write("Начало: ");
            start = double.Parse(Console.ReadLine());
            Console.Write("Конец: ");
            end = double.Parse(Console.ReadLine());
            Console.Write("Шаг: ");
            step = double.Parse(Console.ReadLine());
            Console.Clear();

            SaveFunc(path, NameFunc(funcNumb, functions), start, end, step);
        }

        static Function NameFunc(int num, List<Function> functions)
        {
            switch (num)
            {
                case 1: return functions[1];
                case 2: return functions[2];
                case 3: return functions[3];
                default: return functions[0];
            }
        }
        #endregion

        static double Func1(double x)
        {
            return x * x - 50 * x + 10;
        }

        static double Func2(double x)
        {
            return x * x - 10 * x + 7;
        }

        static double Func3(double x)
        {
            return 3 * x * x - 11 * x + 3;
        }

        static double DefaultFunc(double x)
        {
            return x * x - x;
        }

        public static void SaveFunc(string fileName, Function Func, double a, double b, double h)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    double x = a;
                    while (x <= b)
                    {
                        bw.Write(Func(x));
                        x += h;
                    }
                }
            }
        }

        #region Task 2.3
        public static List<double> Load(string fileName, out double minVal)
        {
            double min = double.MaxValue;
            minVal = double.MaxValue;
            double d;

            List<double> result = new List<double>();
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader bw = new BinaryReader(fs))
                {
                    for (int i = 0; i < fs.Length / sizeof(double); i++)
                    {
                        d = bw.ReadDouble();
                        if (d < min)
                        {
                            result.Add(d);
                            min = d;
                            minVal = d;
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        static void PrintList(List<double> list)
        {
            foreach(double mem in list)
            {
                Console.WriteLine(mem);
            }
        }
    }
}
