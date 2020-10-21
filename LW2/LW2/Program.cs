using System;

namespace LW2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int m = 15; 
            //степень интерполяционного многочлена, который будет построен
            //для того, чтобы найти значение в точке x.
            int n = 7;
            //точка интерполирования, значение в которой хотим найти
            double X = 0.35;
            //начало и конец отрезка
            double a = 0, b = 1;
            
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
            
            Console.WriteLine("ЗАДАЧА АЛГЕБРАИЧЕСКОГО ИНТЕРПОЛИРОВАНИЯ");
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
            Console.WriteLine("Точка интерполирования x: {0}", X);
            Console.WriteLine("Хотите изменить точку интерполирования x?");
            answ = Console.ReadLine();
            if (answ == "y")
            {
                Console.WriteLine("Введите параметр x:");
                X = Convert.ToDouble(Console.ReadLine());
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

            void interpolation()
            {
                double[] xn = new double[n+1];
                //сортировка по мере удаления от x
                for (int i = 0; i <= m; i++)
                {
                    for (int j = i; j <= m; j++)
                    {
                        if (Comparer(x[i], x[j], X))
                        {
                            Swap(ref x[i], ref x[j]);
                        }
                    }
                }

                Console.WriteLine("Ближайшие узлы     Расстояния до x");
                for (int i = 0; i <= n; i++)
                {
                    xn[i] = x[i];
                    Console.WriteLine("{0:F14}   {1:F14}", xn[i], Math.Abs(xn[i] - X));
                }
            
                Console.WriteLine("\nЗначение интерполяционного многочлена Лагранжа в точке x: {0}", Lagrange(xn, X));
                Console.WriteLine("Значение абсолютной фактической погрешности в точке x: {0}\n", Math.Abs(Lagrange(xn, X) - f(X)));
                
                Console.WriteLine("\nЗначение интерполяционного многочлена Ньютона в точке x: {0}", Newton(xn, X));
                Console.WriteLine("Значение абсолютной фактической погрешности в точке x: {0}\n", Math.Abs(Newton(xn, X) - f(X)));
            }

            answ = "";
            while (answ != "y")
            {
                interpolation();
                
                Console.WriteLine("Хотите изменить точку интерполирования x?");
                answ = Console.ReadLine();
                if (answ == "y")
                {
                    Console.WriteLine("Введите параметр x:");
                    X = Convert.ToDouble(Console.ReadLine());
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
                        Console.WriteLine("Введите степень многочлена n n <= {0}", m);
                        n = Convert.ToInt32(Console.ReadLine());
                    }

                    Console.WriteLine("Успешно!");
                }
                
                Console.WriteLine("Хотите завершить программу? y/n");
                answ = Console.ReadLine();
                Console.WriteLine("\n");
            }


            double Lagrange(double[] array, double dot)
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
                            mult1 *= (dot - array[i]);
                            mult2 *= (array[k] - array[i]);
                        }
                    }

                    sum += (f(x[k])*mult1 / mult2);
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

                Xi[0] = 1;
                for (var i = 1; i < n + 1; i++)
                {
                    Xi[i] = Xi[i - 1] * (dot - arrayXn[i-1]);
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
        }
    }
}