using System;

namespace LW6
{
    class Program
    {
        static void Main(string[] args)
        {
            double h = 0.1;
            
            int N = 10;

            double x0 = 0;
            double y0 = 1;

            double f(double x, double y_tepm)
            {
                return -y_tepm + x;
            }
            
            double y(double x)
            {
                return 2*Math.Exp(-x) + x - 1;
            }
            
            double Taylor(double x)
            {
                return 1 - x + Math.Pow(x, 2) - Math.Pow(x, 3)/3 + Math.Pow(x, 4)/12 - Math.Pow(x, 5)/60;
            }
            
            Console.WriteLine("Решение Задачи Коши для обыкновенного дифференциального уравнения первого порядка");
            Console.WriteLine("Вариант №2\n");
            Console.WriteLine("y ́(x)= - y(x)+ x, y(0)=1");
            Console.WriteLine("N = {0}, h = {1}", N, h);
            Console.WriteLine("Хотите изменить параметры задачи?");
            string answ = Console.ReadLine();
            if (answ == "y")
            {
                Console.WriteLine("Введите параметр N:");
                N = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите параметр h:");
                h = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Успешно изменёны!\n");
            }
            
            Console.WriteLine("\nТочное решение задачи Коши: y = 2*e^(-x) + x - 1\n");
            
            double [] xk = new double[N + 3];
            
            Console.WriteLine("    xk            y(xk)");
            for (int k = -2; k <= N; k++)
            {
                xk[k + 2] = x0 + k * h;
                Console.WriteLine("{0:F10}  |  {1:F14}", xk[k + 2], y(xk[k + 2]));
            }
            
            Console.WriteLine("\nМетод разложения в ряд Тейлора\n");
            
            Console.WriteLine("xk    значение приближённого решения    абсолютная погрешность");
            for (int k = -2; k <= N; k++)
            {
                xk[k + 2] = x0 + k * h;
                Console.WriteLine("{0:F10}  |  {1:F14}  |  {2:F14}", xk[k + 2], Taylor(xk[k + 2]), Math.Abs(Taylor(xk[k + 2]) - y(xk[k + 2])));
            }
            
            //методом Эйлера, методом Эйлера I и методом Эйлера II в точках xk = x0+k∙h, где k=1,2,...,N
            Console.WriteLine("\nMетод Эйлера\n");
            double yk = y0 + h * f(x0, y0);
            double ykI = y0 + h * f(x0 + h / 2, y0 + h / 2 * f(x0, y0));
            double ykII = y0 + h / 2 * (f(x0, y0) + f(x0 + h, y0 + h * f(x0, y0)));
            double yk1 = 0, yk12 = 0, yk1I = 0, Yk1 = 0, yk1II = 0;
            for (int k = 1; k <= N; k++)
            {
                
                //Mетод Эйлера
                yk1 = yk + h*f(xk[k + 2], yk);
                //Mетод Эйлера I
                yk12 = ykI + h / 2 * f(xk[k + 2], ykI);
                yk1I = ykI + h * f(xk[k + 2] + h / 2, yk12);
                //Mетод Эйлера II
                double fkII = f(xk[k + 2], ykII);
                Yk1 = ykII +h*fkII;
                if (k != N)
                {
                    yk1II = ykII + h / 2 * (fkII + f(xk[k + 3], Yk1));
                }
                Console.WriteLine("{0:F10}  |  {1:F14}  |  {2:F14}  |  {3:F14}", xk[k + 2], yk, ykI, ykII);
                if (k == N)
                {
                    abserror(yk);
                    abserror(ykI);
                    abserror(ykII);
                }
                yk = yk1;
                ykI = yk1I;
                ykII = yk1II;
            }

            Console.WriteLine("\nМетод Рунге-Кутта 4-го порядка\n");
            double k1, k2, k3, k4;
            double yn = y0;
            double yn1 = 0;
            for (int k = 1; k <= N; k++)
            {
                k1 = h*f(xk[k + 2],yn);
                k2 = h*f(xk[k + 2] +h/2,yn +k1/2);
                k3 = h*f(xk[k + 2] +h/2,yn +k2/2);
                k4 = h*f(xk[k + 2] + h, yn + k3);
                yn1 = yn + (k1 + 2*k2 + 2*k3 + k4)/6;
                Console.WriteLine("{0:F10}  |  {1:F14}", xk[k + 2], yn);
                if (k == N)
                {
                    abserror(yn);
                }
                yn = yn1;
            } 
            
            Console.WriteLine("\nМетодом Адамса 4-го порядка\n");
            double [,] diff = new double[N + 3, 6];
            double yn1a;
            for (int i = 0; i < 5; i++)
            {
                diff[i, 0] = Taylor(xk[i]);
                diff[i, 1] = h * f(xk[i], diff[i, 0]);
            }
            
            for (int i = 2; i < 6; i++)
            {
                for (int j = 0; j < 6 - i; j++)
                {
                    diff[j, i] = diff[j + 1, i - 1] - diff[j, i - 1];
                }
            }

            double getdelyn(int n)
            {
                int j = n + 4;
                diff[j, 1] = h * f(xk[j], diff[j, 0]);
                for (int i = 2; i < 6; i++)
                {
                    diff[j - i + 1, i] = diff[j - i + 2, i - 1] - diff[j - i + 1, i - 1];
                    
                }
                return diff[n + 4, 1] + diff[n + 3, 2]/2 + diff[n + 2, 3]*5/12 + diff[n + 1, 4]*3/8 + diff[n, 5]*251/720;
            }
            
            for (int k = 3; k <= N; k++)
            {
                
                yn1a = diff[k + 1, 0] + getdelyn(k - 3);
                diff[k + 2, 0] = yn1a;
                Console.WriteLine("{0:F10}  |  {1:F14}", xk[k + 2], diff[k + 2, 0]);
            }
            abserror(diff[N+2, 0]);

            void abserror(double num)
            {
                Console.WriteLine("Aбсолютнуя погрешность: {0}", Math.Abs(num - y(xk[N + 2])));
            }
        }
    }
}