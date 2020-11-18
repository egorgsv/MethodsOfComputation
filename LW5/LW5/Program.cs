using System;
using System.Configuration;

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
                return Math.Sin(x);
                //return Math.Cos(x);
            }

            double r(double x)
            {
                return Math.Pow(x, 0.25);
            }

            double Rk(double x, int k)
            {
                return Math.Pow(x, k + 1.25) / (k + 1.25);
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
            
            h = (b - a) / m;
            
            Console.WriteLine("\nf(x) = sin(x)");
            Console.WriteLine("r(x) = x^(1/4)");

            //Console.WriteLine("\nf(x) = cos(x)");
            //Console.WriteLine("r(x) = 1/sqrt(1-x^2)");
            
            Console.WriteLine("N = {0}", N1);
            //Console.WriteLine("N = {0}", N2);
            
            Console.WriteLine("\nСоставная КФ Гаусса с {0} узлами: {1}", N1, compoundQFGauss(N1, m, a, b, h));
            
            Console.WriteLine("\nКФ типа Гаусса с {0} узлами: {1}", N1, QFtypeGauss(N1, a, b));
            
            Console.WriteLine("\nКвадратурная формула Мелера с {0} узлами по [a, b] = [-1; 1]: {1}", N2,  Mehler(N2));

            double compoundQFGauss(int N, int M, double A, double B, double H)
            {
                double res = 0;
                double z = A;
                double t1 = 1 / Math.Sqrt(3);
                double t2 = - 1 / Math.Sqrt(3);
                double x1 = 0;
                double x2 = 0;
                for (int i = 0; i < M; i++)
                {
                    x1 = (t1 + 1) * H / 2 + z;
                    x2 = (t2 + 1) * H / 2 + z;
                    res += f(x1) * r(x1) + f(x2) * r(x2);
                    z += H;
                }
                res *= H / 2;
                return res;
            }

            double QFtypeGauss(int N, double A, double B)
            {
                Console.WriteLine("\nКФ типа Гаусса с {0} узлами", N1); 
                
                double [] mu = new double[2*N];

                Console.WriteLine("\nМоменты весовой функции");
                for (int i = 0; i < mu.Length; i++)
                {
                    mu[i] = Rk(B, i) - Rk(A, i);
                    Console.WriteLine("μ{0} = {1}", i, mu[i]);
                }
            
                double a1 = (mu[0] * mu[3] - mu[2]*mu[1])/(mu[1]*mu[1] - mu[2]*mu[0]);
                double a2 = (mu[2] * mu[2] - mu[3]*mu[1])/(mu[1]*mu[1] - mu[2]*mu[0]);
                Console.WriteLine("\nОртогональный многочлен x^2+{0}x+{1}=0\n", a1, a2);
                //корни уравнения
                double x1 = (-a1 + Math.Sqrt(a1 * a1 - 4 * a2)) / 2;
                double x2 = (-a1 - Math.Sqrt(a1 * a1 - 4 * a2)) / 2;
                
                Console.WriteLine("Узлы КФ: x1 = {0}, x2 = {1}", x1, x2);
                
                if (x1 != x2 && x1 > A && x1 < B && x2 > A && x2 < B) 
                {
                    Console.WriteLine("Различны и принадлежат (a, b).");
                }
                else
                {
                    Console.WriteLine("Узлаы не различны и/или не принадлежат (a, b).");
                }

                double A1 = (mu[1] - x2*mu[0])/(x1 - x2);
                double A2 = (mu[1] - x1*mu[0])/(x2 - x1);

                Console.WriteLine("\nКоэффициенты КФ: A1 = {0}, A2 = {1}", A1, A2);
                
                if (A1 + A2 - mu[0] < 1E-10 && A1*A2 > 0) 
                {
                    Console.WriteLine("Коэффициенты одного знака и A1 + A2 = μ0.");
                }

                double J = A1 * f(x1) + A2 * f(x2);

                return J;
            }
            
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