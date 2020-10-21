using System;

namespace LW3
{
    class Program
    {
        public static void Main(string[] args)
        {
            int m = 10; 
            //степень интерполяционного многочлена, который будет построен
            //для того, чтобы найти значение в точке x.
            int n = 7;
            //значение F
            double F = 0.35;
            //начало и конец отрезка
            double a = 0, b = 1;
            
            double EPS = 0.00000001;
            
            double[] x;
            double[] fx;
            
            double f(double p)
            {
                double res = Math.Log(1+p);
                return res;
            }
            
            void Swap<T>(ref T a1, ref T b1)
            {
                T c = a1;
                a1 = b1;
                b1 = c;
            }
            
            bool Comparer(double x1, double x2, double interpol)
            {
                if (Math.Abs(x1 - interpol) > Math.Abs(x2 - interpol))
                {
                    return true;
                }

                if (Math.Abs(x1 - interpol) < Math.Abs(x2 - interpol))
                {
                    return false;
                }
                
                return false;
            }
            
            Console.WriteLine("ЗАДАЧА ОБРАТНОГО ИНТЕРПОЛИРОВАНИЯ");
            Console.WriteLine("Номер варианта: 2\n");
            Console.WriteLine("Отрезок [a, b] = [{0}, {1}]", a, b);
            Console.WriteLine("Хотите изменить параметры a и b?");
            string answ = Console.ReadLine();
            if (answ == "y")
            {
                Console.WriteLine("Введите параметр a:");
                a = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите параметр b:");
                b = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Успешно изменёны!\n");
            }
            Console.WriteLine("Число значений в таблице: {0}", m + 1);
            Console.WriteLine("Хотите изменить параметр m?");
            answ = Console.ReadLine();
            if (answ == "y")
            {
                Console.WriteLine("Введите параметр m:");
                m = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Успешно изменён!");
            }
            Console.WriteLine("eps = {0}", EPS);
            Console.WriteLine("Хотите изменить параметр eps?");
            answ = Console.ReadLine();
            if (answ == "y")
            {
                Console.WriteLine("Введите параметр eps:");
                EPS = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Успешно изменён!");
            }
            
            x = new double[m+1];
            fx = new double[m+1];
            
            for (int i = 0; i <= m; i++)
            {
                x[i] = a + i * Math.Abs(b - a) / m;
                fx[i] = f(x[i]);
            }
            Console.WriteLine("Исходная таблица значений функции");
            for (int i = 0; i <= m; i++)
            {
                Console.WriteLine("{0:D2}    |    {1:F14}    |    {2:F14}", i, x[i], fx[i]);
            }
            Console.WriteLine("Значение F: {0}", F);
            Console.WriteLine("Хотите изменить значение F?");
            answ = Console.ReadLine();
            if (answ == "y")
            {
                Console.WriteLine("Введите параметр F:");
                F = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Успешно изменён!");
            }
            Console.WriteLine("Введите степень многочлена n <= {0}", m);
            n = Convert.ToInt32(Console.ReadLine());
            while (n > m)
            {
                Console.WriteLine("Введено недопустимое значение n!");
                Console.WriteLine("Введите степень многочлена n <= {0}", m);
                n = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("Успешно!");

            void interpolation1(double[] x, double[] fx)
            {
                double[] fxn = new double[n+1];
                double[] xn = new double[n+1];
                //сортировка по мере удаления от F
                for (int i = 0; i <= m; i++)
                {
                    for (int j = i; j <= m; j++)
                    {
                        if (Comparer(fx[i], fx[j], F))
                        {
                            Swap(ref fx[i], ref fx[j]);
                            Swap(ref x[i], ref x[j]);
                        }
                    }
                }

                Console.WriteLine("Ближайшие узлы     Расстояния до x");
                for (int i = 0; i <= n; i++)
                {
                    fxn[i] = fx[i];
                    xn[i] = x[i];
                    Console.WriteLine("{0:F14}   {1:F14}", fxn[i], Math.Abs(fxn[i] - F));
                }

                double res = Lagrange(fxn, xn, F);
                Console.WriteLine("\nX = {0}", res);
                Console.WriteLine("|f(X) - F| = {0}\n", Math.Abs(f(res) - F));
            }

            void interpolation2(double[] x, double[] fx)
            {
                int num = RootSep(x);
                double x1 = x[num];
                double x2 = x[num + 1];
                double c = (x2 + x1) / 2;
                double[] xn = new double[n+1];
                //сортировка по мере удаления от c
                for (int i = 0; i <= m; i++)
                {
                    for (int j = i; j <= m; j++)
                    {
                        if (Comparer(x[i], x[j], c))
                        {
                            Swap(ref x[i], ref x[j]);
                        }
                    }
                }

                Console.WriteLine("Ближайшие узлы     Расстояния до c");
                for (int i = 0; i <= n; i++)
                {
                    xn[i] = x[i];
                    Console.WriteLine("{0:F14}   {1:F14}", xn[i], Math.Abs(xn[i] - c));
                }
            
                
                Bisection(x1, x2, EPS, xn);
                //RootSep(a, b, m, x);
            }

            answ = "";
            while (answ != "y")
            {
                double[] interpol1_x = new double[m + 1];
                double[] interpol1_fx = new double[m + 1];
                double[] interpol2_x = new double[m + 1];
                double[] interpol2_fx = new double[m + 1];
                for (int i = 0; i <= m; i++)
                {
                    interpol1_x[i] = x[i];
                    interpol2_x[i] = x[i];
                    interpol1_fx[i] = fx[i];
                    interpol2_fx[i] = fx[i];
                }
                interpolation1(interpol1_x, interpol1_fx);
                interpolation2(interpol2_x, interpol2_fx);
                
                Console.WriteLine("Хотите изменить параметр F?");
                answ = Console.ReadLine();
                if (answ == "y")
                {
                    Console.WriteLine("Введите параметр F:");
                    F = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Успешно изменён!");
                }
            
                Console.WriteLine("Хотите изменить степень многочлена n?");
                answ = Console.ReadLine();
                if (answ == "y")
                {
                    Console.WriteLine("Введите степень многочлена n <= {0}", m);
                    n = Convert.ToInt32(Console.ReadLine());
                    while (n > m)
                    {
                        Console.WriteLine("Введено недопустимое значение n!");
                        Console.WriteLine("Введите степень многочлена n <= {0}", m);
                        n = Convert.ToInt32(Console.ReadLine());
                    }

                    Console.WriteLine("Успешно!");
                }
                
                Console.WriteLine("Хотите изменить eps?");
                answ = Console.ReadLine();
                if (answ == "y")
                {
                    Console.WriteLine("Введите eps > 0");
                    EPS = Convert.ToDouble(Console.ReadLine());
                    while (EPS <= 0)
                    {
                        Console.WriteLine("Введено недопустимое значение eps!");
                        Console.WriteLine("Введите eps > 0");
                        EPS = Convert.ToInt32(Console.ReadLine());
                    }

                    Console.WriteLine("Успешно!");
                }
                
                Console.WriteLine("Хотите завершить программу? y/n");
                answ = Console.ReadLine();
                Console.WriteLine("\n");
            }


            double Lagrange(double[] arrayfxn, double[] arrayxn, double dot)
            {
                double sum = 0;
                for (int k = 0; k <= n; k++)
                {
                    double mult1 = 1;
                    double mult2 = 1;
                    for (int i = 0; i <= n; i++)
                    {
                        if (k != i)
                        {
                            mult1 *= (dot - arrayfxn[i]);
                            mult2 *= (arrayfxn[k] - arrayfxn[i]);
                        }
                    }

                    sum += (arrayxn[k]*mult1 / mult2);
                }

                return sum;
            }
            
            double Newton(double[] arrayXn, double dot)
            {
                double[,] array = new double[n+2,n+1];

                for (int j = 0; j < n + 1; j++)
                {
                    array[0, j] = arrayXn[j];
                    array[1, j] = f(arrayXn[j]);
                }
                

                int  k = n;
                for (var i = 2; i < n + 2; i++)
                {
                    for (var j = 0; j < k; j++)
                    {
                        array[i, j] = (array[i - 1, j + 1] - array[i - 1, j]) / (array[0, j+i-1] - array[0, j]);
                    }
                    k--;
                }

                double [] Ai = new double[n + 1];
                double [] Xi = new double[n + 1];
                
                for (int i = 0; i < n + 1; i++)
                {
                    Ai[i] = array[i + 1, 0];
                }

                double res = 0;
                for (int i = n; i > 0; i--)
                {
                    res += Ai[i];
                    res *= (dot - arrayXn[i-1]);
                }

                res += Ai[0];
                return res;
            }
            
            int RootSep(double[] x)
            {
                int counter = 0;
                int c = 0;
                for (int i = 0; i < m; i++)
                {
                    var x1 = x[i];
                    var x2 = x[i + 1];
                    double y1 = f(x1) - F;
                    double y2 = f(x2) - F;
                    if (y1 * y2 <= 0)
                    {
                        counter++;
                        Console.WriteLine("({0}; {1})", x1, x2);
                        c = i;
                    }
                }
                Console.WriteLine("Количества отрезков перемены знака функции: {0}", counter);
                return c;
            }
            
            void Bisection(double a, double b, double eps, double[] x)
            {
                double c = 0;
                double X = 0, delta = 0;
                int count = 0;
                if (eps > 0)
                {
                    do
                    {
                        c = (b + a) / 2;
                        if ((Newton(x, a) - F) * (Newton(x, c) - F) <= 0)
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
                Console.WriteLine("X = {0}    delta = {1}    |f(X) - F| = {2}", X, delta, Math.Abs(f(X) - F));
                Console.WriteLine("Число шагов: {0}\n", count);
            }
        }
    }
}