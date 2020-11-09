using System;

namespace LW4
{
    class Program
    {
        static void Main(string[] args)
        {
            //число промежутков деления [A,B]
            int m = 1000;
            //точка интерполирования, значение в которой хотим найти
            double X = 0.35;
            //начало и конец отрезка
            double A = 0, B = 5;

            double h = (B - A) / m;
            
            //вес
            int w = 1;
            // double w(double x)
            // {
            //     return 1;
            // }

            double f(double x)
            {
                //многочлен нулевой степени
                //return 1;
                //многочлен первой степени
                //return x;
                //многочлен третьей степени
                //return x * x * x;
                return Math.Sin(2 * x);
            }

            //integral sin(2x) from 0 to 5
            double J = 0.919535764538226226129431973912;
            //J = 5;
            //J = 12.5;
            //J = 156.25;
            
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
            
            Console.WriteLine("\nCоставная формула левых прямоугольников");
            double left = leftQuadrature(fx, m, h);
            Console.WriteLine("J(h) = {0}", left);
            Console.WriteLine("│J − J(h)│ = {0}", Math.Abs(left - J));
            double leftError = TheoreticalError(0.5, 2, B, A, h, 0);
            Console.WriteLine("Теоретическая погрешность: {0}", leftError);  
            Console.WriteLine("Разность погрешностей: {0}", leftError - Math.Abs(left - J));

            Console.WriteLine("\nCоставная формула правых прямоугольников");
            double right = rightQuadrature(fx, m, h);
            Console.WriteLine("J(h) = {0}", right);
            Console.WriteLine("│J − J(h)│ = {0}", Math.Abs(right - J));
            double rightError = TheoreticalError(0.5, 2, B, A, h, 0);
            Console.WriteLine("Теоретическая погрешность: {0}", rightError);  
            Console.WriteLine("Разность погрешностей: {0}", rightError - Math.Abs(right - J));

            Console.WriteLine("\nCоставная формула средних прямоугольников");
            double mid = midQuadrature(p, h);
            Console.WriteLine("J(h) = {0}", mid);
            Console.WriteLine("│J − J(h)│ = {0}", Math.Abs(mid - J));
            double midError = TheoreticalError((double) 1/24, 4, B, A, h, 1);
            Console.WriteLine("Теоретическая погрешность: {0}", midError);  
            Console.WriteLine("Разность погрешностей: {0}", midError - Math.Abs(mid - J));
            
            Console.WriteLine("\nCоставная формула трапеции");
            double trapezoid = trapezoidQuadrature(fx, m, y);
            Console.WriteLine("J(h) = {0}", trapezoid);
            Console.WriteLine("│J − J(h)│ = {0}", Math.Abs(trapezoid - J));
            double trapError = TheoreticalError((double) 1/12, 4, B, A, h, 1);
            Console.WriteLine("Теоретическая погрешность: {0}", trapError);  
            Console.WriteLine("Разность погрешностей: {0}", trapError - Math.Abs(trapezoid - J));
            
            Console.WriteLine("\nСимпсон");
            double Simpson = SimpsonQuadrature(fx, y, h, m, p);
            Console.WriteLine("J(h) = {0}", Simpson);
            Console.WriteLine("│J − J(h)│ = {0}", Math.Abs(Simpson - J));
            double SimpsonError = TheoreticalError((double) 1/2880, 16, B, A, h, 3);
            Console.WriteLine("Теоретическая погрешность: {0}", SimpsonError);  
            Console.WriteLine("Разность погрешностей: {0}", SimpsonError - Math.Abs(Simpson - J));

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

            double TheoreticalError(double Const, double M, double B, double A, double h, int d)
            {
                double error = Const * M * (B - A) * Math.Pow(h, d + 1);
                return error;
            }
        }
    }
}