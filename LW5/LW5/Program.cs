using System;

namespace LW5
{
    class Program
    {
        static void Main(string[] args)
        {
            //число промежутков деления [A,B]
            int m = 100;
            //начало и конец отрезка
            double a= 0, b = 1;
            //A = -1;

            double h = (b - a) / m;

            int N1 = 2;
            int N2 = 6;
            
            double f(double x)
            {
                //return Math.Sin(x);
                return Math.Cos(x);
            }

            double r(double x)
            {
                return Math.Pow(x, 0.25);
            }

            double F(double x)
            {
                return Math.Sin(x) + Math.Exp(x) * 2.85 + x;
                //return Math.Pow(x, 4) * 0.25;
            }
            
            Console.WriteLine("Приближённое вычисление интегралов при помощи квадратурных формул\n" +
                              "Наивысшей Алгебраической Степени Точности");
            Console.WriteLine("Вариант №2\n");
            Console.WriteLine("[a, b] = [{0}, {1}], m = {2}", a, b, m);
            Console.WriteLine("Хотите изменить параметры задачи?");
            string answ = Console.ReadLine();
            if (answ == "y")
            {
                Console.WriteLine("Введите параметр A:");
                a = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите параметр B:");
                b = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите параметр m:");
                m = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Успешно изменёны!\n");
            }
            
            //Console.WriteLine("f(x) = sin(x)");
            //Console.WriteLine("r(x) = x^(1/4)");

            Console.WriteLine("f(x) = cos(x)");
            Console.WriteLine("r(x) = 1/sqrt(1-x^2)");
            
            //Console.WriteLine("N = {0}", N1);
            Console.WriteLine("N = {0}", N2);

            Console.WriteLine("Квадратурная формула Мелера с {0} узлами: {1}", N2,  Mehler(N2));
            double Mehler(int N)
            {
                double res = 0;
                for (int k = 1; k <= N; k++)
                {
                    res += f(Math.Cos((2 * k - 1) * Math.PI / (2 * N)));
                }

                res *= Math.PI / N;
                return res;
            }
            
        }
    }
}