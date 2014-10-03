using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSModels;

namespace standart_alg
{



    class standart_alg
    {
        private double h; //шаг

        public double H
        {
            get { return h; }
            set { h = value; }
        }

        /// <summary>
        /// Standart Algorithm
        /// </summary>
        /// <param name="pin_min">input pressure minimum value</param>
        /// <param name="pin_max">input pressure maximum value</param>
        /// <param name="pout_min">output pressure minimum value</param>
        /// <param name="pout_max">output pressure maximum value</param>
        /// <param name="compr_min">input/output pressure minimum value</param>
        /// <param name="compr_max">input/output pressure maximum value</param>
        /// <returns>cost of upgrading and maintenance</returns>
        public double Algorithm(double pin_min, double pin_max, double pout_min, double pout_max, double compr_min, double compr_max)
        {
            double k = -1, cost = -1;
            double a, b, c, d, e, maintaince, upgrade;
            int n = 0; //количество успешных вычислений функции

            //путь к библиотеке нужно каждому поправить на свой, я так понимаю, чтоб это работало
            DllBlackBoxCalculator calc = new DllBlackBoxCalculator(@"C:\Users\1\Desktop\GitHub\StandartAlg\standart_alg\bbs\Models\1.1.КС.r1", null);
            for (double i = pin_min; i <= pin_max; i = i + h)
                for (double j = pout_min; j <= pout_max; j = j + h)
                    if (((i / j) <= compr_max) && ((i / j) >= compr_min))
                    {
                        try
                        {
                            //calc.Calculate(i, j, Qout, Tin, Cin, Din, out qin1, out tout1, out EZ, out KZ, out Expand, out cout1, out dout1, getInfo = false);
                            calc.Calculate(i, j, 15, 5, 7, 3, out a, out b, out maintaince, out upgrade, out c, out d, out e);
                            Console.WriteLine("a = {0}, b = {1}, c = {2}, d = {3}, e = {4}, maintaince = {5}, upgrade = {6}", a, b, c, d, e, maintaince, upgrade);
                            k = maintaince + upgrade;
                            n++; 
                        }
                        catch
                        {
                        }
                        if (n == 1)
                            cost = k;
                        else if (k < cost)
                            cost = k;
                        Console.WriteLine("k = {0}, i = {1}, j = {2}", k, i, j);
                    }
            Console.WriteLine("n = {0}", n);
            return cost;
        }



        /// <summary>
        /// Just something to test my algorithm
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            standart_alg A = new standart_alg();
            A.H = 0.5;
            ///в библиотеке определены константы 
            ///PInMax = 80.0
            ///PInMin = 45.0
            ///POutMax = 85.2
            ///POutMin = 50.0
            ///KKomprMax = 2.0
            ///KKomprMin = 1.1
            ///QOutMax = 440
            ///QOutMin = 10
            ///параметры ниже подбирались исходя из этого
            ///там ещё есть KZMax = 0, м.б. поэтому upgrade всегда равен нулю
            double pin_min = 50;
            double pin_max = 60;
            double pout_min = 60;
            double pout_max = 70;
            double compr_min = 0.8;
            double compr_max = 0.9;

            double res = A.Algorithm(pin_min, pin_max, pout_min, pout_max, compr_min, compr_max);
            Console.WriteLine("cost = {0}", res);
            Console.WriteLine("step = {0}", A.H);
            Console.ReadKey();

        }

    }
}
