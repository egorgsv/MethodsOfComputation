using System;
using System.Xml.Linq;

namespace LW3._2
{
    class Program
    {
        static void Main(string[] args)
        {
            int m = 15;

            double a = 0, h = 0.001;
            
            double[] x;
            double[] fx;
            
            double f(double p)
            {
                double res = Math.Exp(1.5*3*p);
                return res;
            }
            
            double df(double p)
            {
                double res = 4.5*Math.Exp(1.5*3*p);
                return res;
            }
            
            double d2f(double p)
            {
                double res = 4.5*4.5*Math.Exp(1.5*3*p);
                return res;
            }

            Console.WriteLine("Нахождение производных таблично-заданной функции по формулам численного дифференцирования");
            Console.WriteLine("Номер варианта: 2\n");
            Console.WriteLine("Значение a = {0}", a);
            Console.WriteLine("Хотите изменить параметр a?");
            string answ = Console.ReadLine();
            if (answ == "y")
            {
                Console.WriteLine("Введите параметр a:");
                a = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Успешно изменён!\n");
            }
            
            Console.WriteLine("Значение h = {0}", h);
            Console.WriteLine("Хотите изменить параметр h?");
            answ = Console.ReadLine();
            if (answ == "y")
            {
                Console.WriteLine("Введите параметр h > 0");
                h = Convert.ToDouble(Console.ReadLine());
                while (h <= 0)
                {
                    Console.WriteLine("Введено недопустимое значение h!");
                    Console.WriteLine("Введите  h > 0");
                    h = Convert.ToDouble(Console.ReadLine());
                }

                Console.WriteLine("Успешно!");
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
                
            void newtable(double a, double h, int m)
            {
                for (int i = 0; i <= m; i++)
                {
                    x[i] = a + i * h;
                    fx[i] = f(x[i]);
                }
                
                Console.WriteLine("Исходная таблица значений функции");
                for (int i = 0; i <= m; i++)
                {
                    Console.WriteLine("{0:D2}    |    {1:F14}    |    {2:F14}", i, x[i], fx[i]);
                }
            }
            
            

            void numDiff(double[] xder, double[] fxder)
            {
                //double[] xn = new double[n+1];
                //сортировка по мере удаления от F
                double[] first_deriv = new double[m+1];
                double[] second_deriv = new double[m+1];
                
                

                first_deriv = Deriv(xder, fxder);
                
                for (int i = 1; i < m; i++)
                {
                    second_deriv[i] = (fxder[i + 1] - 2*fxder[i] + fxder[i - 1]) / Math.Pow(xder[i + 1] - xder[i], 2);
                }
                
                Console.WriteLine("          x                    f(x)                    f'(x)чд              |f'(x) - f'(x)чд|              f''(x)чд          |f''(x) - f''(x)чд|");
                for (int i = 0; i <= m; i++)
                {
                    Console.WriteLine("{0:F14}    |    {1:F14}    |    {2:F14}    |    {3:F14}    |    {4:F14}    |    {5:F14}", xder[i], fxder[i], first_deriv[i], Math.Abs(first_deriv[i] - df(xder[i])), second_deriv[i], Math.Abs(second_deriv[i] - d2f(xder[i])));
                }
            }

            double[] Deriv(double[] xder, double[] fxder)
            {
                double[] deriv = new double[m+1];
                deriv[0] = (-3 * fxder[0] + 4 * fxder[1] - fxder[2]) / (xder[2] - xder[0]);
                for (int i = 1; i < m; i++)
                {
                    deriv[i] = (fxder[i + 1] - fxder[i - 1]) / (xder[i + 1] - xder[i - 1]);
                }
                deriv[m] = (3 * fxder[m] - 4 * fxder[m - 1] + fxder[m - 2]) / (xder[m] - xder[m - 2]);
                return deriv;
            }

            answ = "";
            while (answ != "y")
            {
                newtable(a, h, m);
                numDiff(x, fx);
                
                Console.WriteLine("Хотите создать новую таблицу значений функции?");
                answ = Console.ReadLine();
                if (answ == "y")
                {
                    Console.WriteLine("Хотите изменить параметр a?");
                    answ = Console.ReadLine();
                    if (answ == "y")
                    {
                        Console.WriteLine("Введите параметр a:");
                        a = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Успешно изменён!\n");
                    }
                    
                    Console.WriteLine("Хотите изменить параметр m?");
                    answ = Console.ReadLine();
                    if (answ == "y")
                    {
                        Console.WriteLine("Введите параметр m:");
                        m = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Успешно изменён!\n");
                    }
                    
                    Console.WriteLine("Хотите изменить параметр h?");
                    answ = Console.ReadLine();
                    if (answ == "y")
                    {
                        Console.WriteLine("Введите параметр h > 0");
                        h = Convert.ToDouble(Console.ReadLine());
                        while (h <= 0)
                        {
                            Console.WriteLine("Введено недопустимое значение h!");
                            Console.WriteLine("Введите  h > 0");
                            h = Convert.ToDouble(Console.ReadLine());
                        }

                        Console.WriteLine("Успешно!");
                    }
                }
            
                
                Console.WriteLine("Хотите завершить программу? y/n");
                answ = Console.ReadLine();
                Console.WriteLine("\n");
            }
        }
    }
}