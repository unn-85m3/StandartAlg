using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace standart_alg
{



    class standart_alg
    {
        private double h; //шаг

        public double Func(double a, double b)
        {
            return (1/((a+b)*a*a));
        }

        public double H{
            get {return h;}
            set { h = value;}
        }


        public double Algorithm(double pin_min, double pin_max, double pout_min, double pout_max, double compr_min, double compr_max)
        {
            double k, cost;

            cost = Func(pin_min, pout_min);

            for (double i = pin_min; i <= pin_max; i = i + h)
                for (double j = pout_min; j <= pout_max; j = j + h)
                    if (((i / j) <= compr_max) && ((i / j) >= compr_min))
                    {
                        /*try
                        {
                            k = Func(i, j);
                        }
                        catch
                        {  //я по-прежнему не знаю, что делать с эксепшеном
                        }*/
                        k = Func(i, j);
                        Console.WriteLine("k = {0}, i = {1}, j = {2},", k, i, j);
                        if (k < cost)
                            cost = k;
                    }
            return cost;
        }




        static void Main(string[] args)
        {
            standart_alg A = new standart_alg();
            A.H = 1;
            double pin_min = 1;
            double pin_max = 4;
            double pout_min = 1;
            double pout_max = 4;
            double compr_min = 0.5;
            double compr_max = 1;

            double res = A.Algorithm(pin_min, pin_max, pout_min, pout_max, compr_min, compr_max);
            Console.WriteLine(res);
            Console.Write(A.H);
            Console.ReadKey();

        }

    }
}