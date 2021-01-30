using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task03
{
    //    3. Переделать программу «Пример использования коллекций» для решения следующих задач:
    //    а) Подсчитать количество студентов учащихся на 5 и 6 курсах;
    //    б) подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся(частотный массив);
    //    в) отсортировать список по возрасту студента;
    //    г) * отсортировать список по курсу и возрасту студента;
    //    д) разработать единый метод подсчета количества студентов по различным параметрам выбора с помощью делегата и методов предикатов.
    class Program
    {
        public delegate int FilterTypeInt(List<int> values, int value);
        public delegate int FilterTypeString(List<string> values, string value);

        static void Main(string[] args)
        {
            int course_5 = 0;
            int course_6 = 0;
            List<Student> list = new List<Student>();
            Dictionary<int, int> stud = new Dictionary<int, int>(CreateStudentCourseAge());

            using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Students.scv"))
            {
                while (!sr.EndOfStream)
                {
                    string[] s = sr.ReadLine().Split(',');
                    list.Add(new Student(s[0], s[1], s[2], s[3], s[4], int.Parse(s[5]), int.Parse(s[6]), int.Parse(s[7]), s[8]));
                    #region Task 3.1(а)
                    if (int.Parse(s[6]) == 5) course_5++;
                    if (int.Parse(s[6]) == 6) course_6++;
                    #endregion

                    StudentAgeCourse(s, stud);
                }
            }
            Console.WriteLine("Всего студентов:" + list.Count);
            Console.WriteLine($"Студентов пятого курса:{course_5}");
            Console.WriteLine($"Студентов шестого курса:{course_6}");
            Console.WriteLine();

            PrintStudentCourseAge(stud);
            Console.WriteLine();
            list.Sort(ComparisonAge);
            foreach (var item in list)
            {
                Console.WriteLine($"{item.firstName} | {item.lastName} | {item.university} | {item.faculty} | {item.department} | {item.age} | {item.course} | {item.group} | {item.city}");
            }
            Console.WriteLine();
            list.Sort(ComparisonCourseAge);
            foreach (var item in list)
            {
                Console.WriteLine($"{item.firstName} | {item.lastName} | {item.university} | {item.faculty} | {item.department} | {item.age} | {item.course} | {item.group} | {item.city}");
            }
            Console.WriteLine();

            List<FilterTypeInt> filterTypeInts = new List<FilterTypeInt> { FilterAge, FilterCourse, FilterGroup };
            List<FilterTypeString> filterTypeStrings = new List<FilterTypeString> { FilterFirstName, FilterSecondName, FilterUniversity, FilterFacult, FilterCaf, FilterCity };
            Filters(list, filterTypeStrings, filterTypeInts);

            Console.ReadKey();

        }

        #region Task 3.2(б)
        static Dictionary<int, int> StudentAgeCourse(string[] s, Dictionary<int, int> studAgeCourse)
        {
            if (int.Parse(s[5]) > 17 && int.Parse(s[5]) < 21)
            {
                if (int.Parse(s[5]) == 18)
                {
                    studAgeCourse[18] += 1;
                    if (s[6] == "1") studAgeCourse[118]++;
                    if (s[6] == "2") studAgeCourse[218]++;
                    if (s[6] == "3") studAgeCourse[318]++;
                    if (s[6] == "4") studAgeCourse[418]++;
                    if (s[6] == "5") studAgeCourse[518]++;
                    if (s[6] == "6") studAgeCourse[618]++;
                }
                if (int.Parse(s[5]) == 19)
                {
                    studAgeCourse[19] += 1;
                    if (s[6] == "1") studAgeCourse[119]++;
                    if (s[6] == "2") studAgeCourse[219]++;
                    if (s[6] == "3") studAgeCourse[319]++;
                    if (s[6] == "4") studAgeCourse[419]++;
                    if (s[6] == "5") studAgeCourse[519]++;
                    if (s[6] == "6") studAgeCourse[619]++;
                }
                if (int.Parse(s[5]) == 20)
                {
                    studAgeCourse[20] += 1;
                    if (s[6] == "1") studAgeCourse[120]++;
                    if (s[6] == "2") studAgeCourse[220]++;
                    if (s[6] == "3") studAgeCourse[320]++;
                    if (s[6] == "4") studAgeCourse[420]++;
                    if (s[6] == "5") studAgeCourse[520]++;
                    if (s[6] == "6") studAgeCourse[620]++;
                }
            }
            return studAgeCourse;
        }

        static Dictionary<int, int> CreateStudentCourseAge()
        {
            Dictionary<int, int> studAgeCourse = new Dictionary<int, int>();
            studAgeCourse.Add(18, 0);
            studAgeCourse.Add(19, 0);
            studAgeCourse.Add(20, 0);
            studAgeCourse.Add(118, 0);
            studAgeCourse.Add(119, 0);
            studAgeCourse.Add(120, 0);
            studAgeCourse.Add(218, 0);
            studAgeCourse.Add(219, 0);
            studAgeCourse.Add(220, 0);
            studAgeCourse.Add(318, 0);
            studAgeCourse.Add(319, 0);
            studAgeCourse.Add(320, 0);
            studAgeCourse.Add(418, 0);
            studAgeCourse.Add(419, 0);
            studAgeCourse.Add(420, 0);
            studAgeCourse.Add(518, 0);
            studAgeCourse.Add(519, 0);
            studAgeCourse.Add(520, 0);
            studAgeCourse.Add(618, 0);
            studAgeCourse.Add(619, 0);
            studAgeCourse.Add(620, 0);

            return studAgeCourse;
        }

        static void PrintStudentCourseAge(Dictionary<int, int> studAgeCourse)
        {
            Console.WriteLine($"Количество студентов 18ти лет: {studAgeCourse[18]}.");
            Console.WriteLine("На каких курсах/курсе студенты 18 лет.");
            Console.Write($"Первый курс: {studAgeCourse[118]}. ");
            Console.Write($"Второй курс: {studAgeCourse[218]}. ");
            Console.Write($"Третий курс: {studAgeCourse[318]}. ");
            Console.Write($"Четвёртый курс: {studAgeCourse[418]}. ");
            Console.Write($"Пятый курс: {studAgeCourse[518]}. ");
            Console.WriteLine($"Шестой курс: {studAgeCourse[618]}.");
            Console.WriteLine($"Количество студентов 19ти лет: {studAgeCourse[19]}.");
            Console.WriteLine("На каких курсах/курсе студенты 19 лет.");
            Console.Write($"Первый курс: {studAgeCourse[119]}. ");
            Console.Write($"Второй курс: {studAgeCourse[219]}. ");
            Console.Write($"Третий курс: {studAgeCourse[319]}. ");
            Console.Write($"Четвёртый курс: {studAgeCourse[419]}. ");
            Console.Write($"Пятый курс: {studAgeCourse[519]}. ");
            Console.WriteLine($"Шестой курс: {studAgeCourse[619]}.");
            Console.WriteLine($"Количество студентов 20ти лет: {studAgeCourse[20]}.");
            Console.WriteLine("На каких курсах/курсе студенты 20 лет.");
            Console.Write($"Первый курс: {studAgeCourse[120]}. ");
            Console.Write($"Второй курс: {studAgeCourse[220]}. ");
            Console.Write($"Третий курс: {studAgeCourse[320]}. ");
            Console.Write($"Четвёртый курс: {studAgeCourse[420]}. ");
            Console.Write($"Пятый курс: {studAgeCourse[520]}. ");
            Console.WriteLine($"Шестой курс: {studAgeCourse[620]}.");
        }
        #endregion

        #region Task 3.3(в)
        static int ComparisonAge(Student st1, Student st2)
        {
            if (st1.age == 0)
            {
                if (st2.age == 0)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (st2.age == 0)
                {
                    return 1;
                }
                else
                {
                    return st1.age.CompareTo(st2.age);
                }
            }
        }
        #endregion

        #region Task 3.4(г)
        static int ComparisonCourseAge(Student st1, Student st2)
        {
            if (st1.course == 0)
            {
                if (st2.course == 0)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (st2.course == 0)
                {
                    return 1;
                }
                else
                {

                    if (st1.age == 0)
                    {
                        if (st2.age == 0)
                        {
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        if (st2.age == 0)
                        {
                            return 1;
                        }
                        else
                        {
                            return st1.course.CompareTo(st2.course);
                        }
                    }
                }
            }
        }
        #endregion

        #region Task 3.5(д)
        static void Filters(List<Student> students, List<FilterTypeString> filterTypeStrings, List<FilterTypeInt> filterTypeInts)
        {
            int indexMethod;
            Console.WriteLine("Выберите ячейку для которой будет применятся фильтр.");
            Console.WriteLine("1: по фамилиям.");
            Console.WriteLine("2: по именам.");
            Console.WriteLine("3: по университету.");
            Console.WriteLine("4: по факультету.");
            Console.WriteLine("5: по кафедре.");
            Console.WriteLine("6: по возрасту.");
            Console.WriteLine("7: по курсу.");
            Console.WriteLine("8: по группе.");
            Console.WriteLine("9: по городу");
            indexMethod = int.Parse(Console.ReadLine());

            Console.Clear();

            switch (indexMethod)
            {
                case 1:
                    {
                        string value;
                        List<string> values = new List<string>();
                        foreach (var student in students)
                        {
                            values.Add(student.firstName);
                        }
                        Console.Write("Введите фамилию, которую хотите посчитать: ");
                        value = Console.ReadLine();
                        AllFunctionsString(1, values, value, filterTypeStrings);
                        break;
                    }
                case 2:
                    {
                        string value;
                        List<string> values = new List<string>();
                        foreach (var student in students)
                        {
                            values.Add(student.lastName);
                        }
                        Console.Write("Введите имя, которое хотите посчитать: ");
                        value = Console.ReadLine();
                        AllFunctionsString(2, values, value, filterTypeStrings);
                        break;
                    }
                case 3:
                    {
                        string value;
                        List<string> values = new List<string>();
                        foreach (var student in students)
                        {
                            values.Add(student.university);
                        }
                        Console.Write("Введите название университета, которое хотите посчитать: ");
                        value = Console.ReadLine();
                        AllFunctionsString(3, values, value, filterTypeStrings);
                        break;
                    }
                case 4:
                    {
                        string value;
                        List<string> values = new List<string>();
                        foreach (var student in students)
                        {
                            values.Add(student.faculty);
                        }
                        Console.Write("Введите название факультета, которое хотите посчитать: ");
                        value = Console.ReadLine();
                        AllFunctionsString(4, values, value, filterTypeStrings);
                        break;
                    }
                case 5:
                    {
                        string value;
                        List<string> values = new List<string>();
                        foreach (var student in students)
                        {
                            values.Add(student.department);
                        }
                        Console.Write("Введите название кафедры, которую хотите посчитать: ");
                        value = Console.ReadLine();
                        AllFunctionsString(5, values, value, filterTypeStrings);
                        break;
                    }

                case 6:
                    {
                        int value;
                        List<int> values = new List<int>();
                        foreach (var student in students)
                        {
                            values.Add(student.age);
                        }
                        Console.Write("Введите возраст, который хотите посчитать: ");
                        value = int.Parse(Console.ReadLine());
                        AllFunctionsInt(6, values, value, filterTypeInts);
                        break;
                    }

                case 7:
                    {
                        int value;
                        List<int> values = new List<int>();
                        foreach (var student in students)
                        {
                            values.Add(student.course);
                        }
                        Console.Write("Введите курс, который хотите посчитать: ");
                        value = int.Parse(Console.ReadLine());
                        AllFunctionsInt(7, values, value, filterTypeInts);
                        break;
                    }

                case 8:
                    {
                        int value;
                        List<int> values = new List<int>();
                        foreach (var student in students)
                        {
                            values.Add(student.group);
                        }
                        Console.Write("Введите номер группы, которую хотите посчитать: ");
                        value = int.Parse(Console.ReadLine());
                        AllFunctionsInt(8, values, value, filterTypeInts);
                        break;
                    }


                case 9:
                    {
                        string value;
                        List<string> values = new List<string>();
                        foreach (var student in students)
                        {
                            values.Add(student.city);
                        }
                        Console.Write("Введите название города, который хотите посчитать: ");
                        value = Console.ReadLine();
                        AllFunctionsString(9, values, value, filterTypeStrings);
                        break;
                    }

                default:
                    Console.WriteLine("Выбранной команды не существует.");
                    break;
            }
        }

        static void AllFunctionsInt(int index, List<int> values, int value, List<FilterTypeInt> filterTypes)
        {
            switch (index)
            {
                case 6:
                    Console.WriteLine($"Совпадений: {filterTypes[0](values, value)}");
                    break;
                case 7:
                    Console.WriteLine($"Совпадений: {filterTypes[1](values, value)}");
                    break;
                case 8:
                    Console.WriteLine($"Совпадений: {filterTypes[2](values, value)}");
                    break;
                default:
                    break;
            }
        }

        static void AllFunctionsString(int index, List<string> values, string value, List<FilterTypeString> filterTypes)
        {
            switch (index)
            {
                case 1:
                    Console.WriteLine($"Совпадений: {filterTypes[0](values, value)}");
                    break;
                case 2:
                    Console.WriteLine($"Совпадений: {filterTypes[1](values, value)}");
                    break;
                case 3:
                    Console.WriteLine($"Совпадений: {filterTypes[2](values, value)}");
                    break;
                case 4:
                    Console.WriteLine($"Совпадений: {filterTypes[3](values, value)}");
                    break;
                case 5:
                    Console.WriteLine($"Совпадений: {filterTypes[4](values, value)}");
                    break;
                case 9:
                    Console.WriteLine($"Совпадений: {filterTypes[5](values, value)}");
                    break;
                default:
                    break;
            }
        }

        static int FilterAge(List<int> ages, int age)
        {
            int result = 0;
            foreach (var item in ages)
            {
                if (item == age) result++;
            }
            return result;
        }

        static int FilterCourse(List<int> courses, int course)
        {
            int result = 0;
            foreach (var item in courses)
            {
                if (item == course) result++;
            }
            return result;
        }

        static int FilterGroup(List<int> groups, int group)
        {
            int result = 0;
            foreach (var item in groups)
            {
                if (item == group) result++;
            }
            return result;
        }

        static int FilterUniversity(List<string> universitys, string university)
        {
            int result = 0;
            foreach (var item in universitys)
            {
                if (item == university) result++;
            }
            return result;
        }

        static int FilterFacult(List<string> facults, string facult)
        {
            int result = 0;
            foreach (var item in facults)
            {
                if (item == facult) result++;
            }
            return result;
        }
        static int FilterCaf(List<string> cafs, string caf)
        {
            int result = 0;
            foreach (var item in cafs)
            {
                if (item == caf) result++;
            }
            return result;
        }

        static int FilterFirstName(List<string> firstNames, string firstName)
        {
            int result = 0;
            foreach (var item in firstNames)
            {
                if (item == firstName) result++;
            }
            return result;
        }

        static int FilterSecondName(List<string> secondNames, string secondName)
        {
            int result = 0;
            foreach (var item in secondNames)
            {
                if (item == secondName) result++;
            }
            return result;
        }

        static int FilterCity(List<string> cities, string city)
        {
            int result = 0;
            foreach (var item in cities)
            {
                if (item == city) result++;
            }
            return result;
        }
        #endregion

        class Student
        {
            public string lastName;
            public string firstName;
            public string university;
            public string faculty;
            public int course;
            public string department;
            public int group;
            public string city;
            public int age;

            public Student(string firstName, string lastName, string university, string faculty, string department, int age, int course, int group, string city)
            {
                this.lastName = lastName;
                this.firstName = firstName;
                this.university = university;
                this.faculty = faculty;
                this.department = department;
                this.course = course;
                this.age = age;
                this.group = group;
                this.city = city;
            }

        }
    }
}
