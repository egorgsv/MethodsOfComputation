using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace LW4
{
    class Program
    {
        static void Main(string[] args)
        {
            //число промежутков деления [A,B]
            int m = 15;
            //точка интерполирования, значение в которой хотим найти
            double X = 0.35;
            //начало и конец отрезка
            double A = 0, B = 1;

            double h = (B - A) / m;
            
            //вес
            int w = 1;
            // double w(double x)
            // {
            //     return 1;
            // }

            //многочлен нулевой степени
            double f0(double x)
            {
                return 3.5;
            }
            
            //многочлен первой степени
            double f1(double x)
            {
                return x * 6 + 3.5;
            }
            
            //многочлен третьей степени
            double f3(double x)
            {
                return x * x * x - x * x + 1;
            }

            double f(double x)
            {
                return Math.Sin(2 * x);
            }

            //integral sin(2x) from 0 to 1
            double J = 0.7080734182735711934;

            
            Console.WriteLine("Приближённое вычисление интеграла по составным квадратурным формулам");
            Console.WriteLine("A = {0}, B = {1}, m = {2}, h = {3}", A, B, m, h);
            Console.WriteLine("Хотите изменить параметры задачи?");
            string answ = Console.ReadLine();
            if (answ == "y")
            {
                Console.WriteLine("Введите параметр A:");
                A = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите параметр B:");
                B = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите параметр m:");
                m = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Успешно изменёны!\n");
            }
            
            
            Console.WriteLine("f(x) = sin(2x)");
            Console.WriteLine("J = {0}", J);
            
            double [] x = new double[m+1];
            double [] fx = new double[m+1];
                
            
            for (int i = 0; i <= m; i++)
            {
                x[i] = A + i * Math.Abs(B - A) / m;
                fx[i] = f(x[i]);
            }

            h = (B - A) / m;
            
            double y = 0;
            double p = f((x[0] + x[1]) / 2);
            for (int i = 1; i < m; i++)
            {
                y += fx[i];
                p += f((x[i] + x[i + 1]) / 2);
            }

            double leftQuadrature(double [] fx, int m, double h)
            {
                double sum = 0;
                for (int i = 0; i < m; i++)
                {
                    sum += fx[i] * h;
                }
                return sum;
            }
            
            double rightQuadrature(double [] fx, int m, double h)
            {
                double sum = 0;
                for (int i = 1; i <= m; i++)
                {
                    sum += fx[i] * h;
                }
                return sum;
            }
            
            double midQuadrature(double p, double h)
            {
                double sum = p * h;
                return sum;
            }

            double trapezoidQuadrature(double [] fx, int m, double y)
            {
                double sum = h * ((fx[0] + fx[m]) / 2 + y);
                return sum;
            }
            
            //D'OH
            double SimpsonQuadrature(double [] fx, double y, double h, int m, double p)
            {
                double sum = h / 6 * (fx[0] + fx[m] + 2 * y + 4 * p);
                return sum;
            }
        }
    }
}