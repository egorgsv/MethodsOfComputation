using System;

namespace LW1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int A = -5;
            int B = 10;
            double EPS = 0.000001;
            int  N = 1000;
            
            double f(double x)
            {
                double res = Math.Pow(2, -x) - Math.Sin(x);
                return res;
            }
            
            //производная функции
            double df(double x)
            {
                double res = - Math.Cos(x) - 2*Math.Pow(2, -x) * Math.Log(2);
                return res;
            }
            
            Console.WriteLine("ЧИСЛЕННЫЕ МЕТОДЫ РЕШЕНИЯ НЕЛИНЕЙНЫХ УРАВНЕНИЙ");
            Console.WriteLine("Исходные параметры задачи:");
            Console.WriteLine("A = {0} \nB = {1} \nf(x) = 2^(-x) - sin(x) \nEPS = {2}\nN = {3}\n", A, B, EPS, N);
            Console.WriteLine("Изменить параметры задачи?");
            string answ = Console.ReadLine();
            if (answ == "yes")
            {
                Console.WriteLine("Введите параметр A");
                A = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите параметр B");
                B = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите параметр N");
                N = Convert.ToInt32(Console.ReadLine());
            }
            RootSep(A, B, N);
            
            //Отделение корней
            void RootSep(int a, int b, int n)
            {
                int counter = 0;
                double H = (double)(b - a)/n;
                double x1 = a;
                double x2 = x1 + H;
                double y1 = f(x1);
                while (x2 <= b)
                {
                    double y2 = f(x2);
                    if (y1 * y2 <= 0)
                    {
                        counter++;
                        Console.WriteLine("({0}; {1})", x1, x2);
                        Bisection(x1, x2, EPS);
                        Newton(x1, x2, EPS);
                        ModNewton(x1, x2, EPS);
                        Secant(x1, x2, EPS);
                    }

                    x1 = x2;
                    x2 = x1 + H;
                    y1 = y2;
                }
                Console.WriteLine("Количества отрезков перемены знака функции: {0}", counter);
                
            }

            //Метод бисекции
            void Bisection(double a, double b, double eps)
            {
                double c = 0;
                double X = 0, delta = 0;
                int count = 0;
                if (eps > 0)
                {
                    do
                    {
                        c = (b + a) / 2;
                        if (f(a) * f(c) <= 0)
                        {
                            b = c;
                        }
                        else
                        {
                            a = c;
                        }

                        count++;
                    } while (b - a > 2*eps);

                    X = (a + b) / 2;
                    delta = (b - a) / 2;
                }
                Console.WriteLine("Метод бисекции");
                Console.WriteLine("Начальное приближение: {0}", a);
                Console.WriteLine("x = {0}    delta = {1}    |f(x)| = {2}", X, delta, Math.Abs(f(X)));
                Console.WriteLine("Число шагов: {0}\n", count);
            }

            //Метод Ньютона
            void Newton(double a, double b, double eps)
            {
                double X1 = (a + b)/2, delta = 0;
                int count = 1, p = 1;
                double X2 =  X1 - f(X1) / df(X1);
                
                while (Math.Abs(X1 - X2)> eps)
                {
                    if (df(X1) != 0)
                    {
                        X1 = X2;
                        X2 = X1 - p*f(X1) / df(X1);
                        count++;
                    }
                    else
                    {
                        count = 1;
                        p += 2;
                        X1 = (a + b) / 2;
                        X2 = X1 - p*f(X1) / df(X1);
                    }
                    
                }
                delta = Math.Abs(X1 - X2);
                
                Console.WriteLine("Метод Ньютона");
                Console.WriteLine("Начальное приближение: {0}", a);
                Console.WriteLine("x = {0}    delta = {1}    |f(x)| = {2}", X2, delta, Math.Abs(f(X2)));
                Console.WriteLine("Число шагов: {0}\n", count);
            }
            
            //Модифицированный метод Ньютона
            void ModNewton(double a, double b, double eps)
            {
                double X0 = (a + b)/2, delta = 0;
                double X1 = X0;
                int count = 1;
                double X2 =  X1 - f(X1) / df(X0);
                
                while (Math.Abs(X1 - X2)> eps)
                {
                    X1 = X2;
                    X2 = X1 - f(X1) / df(X0);
                    count++;
                }
                delta = Math.Abs(X1 - X2);
                
                Console.WriteLine("Модифицированный метод Ньютона");
                Console.WriteLine("Начальное приближение: {0}", a);
                Console.WriteLine("x = {0}    delta = {1}    |f(x)| = {2}", X2, delta, Math.Abs(f(X2)));
                Console.WriteLine("Число шагов: {0}\n", count);
            }
            
            //Метод секущих
            void Secant(double a, double b, double eps)
            {
                double X0 = a, delta = 0;
                double X1 = b;
                int count = 2;
                double X2 =  X1 - f(X1) / df(X0);

                while (Math.Abs(X1 - X2)> eps) 
                {
                    X0 = X1;
                    X1 = X2;
                    X2 = X1 - f(X1)*(X1 - X0) / (f(X1) - f(X0));
                    count++;
                }
                delta = Math.Abs(X1 - X2);

                Console.WriteLine("Метод секущих");
                Console.WriteLine("Начальное приближение: {0}", a);
                Console.WriteLine("x = {0}    delta = {1}    |f(x)| = {2}", X2, delta, Math.Abs(f(X2)));
                Console.WriteLine("Число шагов: {0}\n", count);
            }
        }
    }
}