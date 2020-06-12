using System;
using System.Collections.Generic;
using System.Linq;

namespace ANNExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            var weights1 = new double[,] { { .4, .7, .2 }, { .8, .3, .5 } };
            var weights2 = new double[,] { { .3, .7, .1 } };
            for (int i = 0; i < 100000; i++)
            {
                var val1 = rnd.Next(0, 2);
                var val2 = rnd.Next(0, 2);
                var input = new double[,] { { val1 }, { val2 }, { 1 } };
                var output1 = multiplyMatrix(weights1, input);
                var outputsig1 = Sigmoid(output1);
                var augoutput1 = new double[,] { { outputsig1[0, 0] },{ outputsig1[1, 0] }, { 1 } };
                var output2 = multiplyMatrix(weights2, augoutput1);
                var outputsig2 = Sigmoid(output2);
                var d3 = CalculateLastErrorGradient(new double[,] { { (val1 == 1 ^ val2 == 1) ? 1.0 : 0.0 } }, outputsig2);
                var d2 = CalculateFirstErrorGradient(d3, new double[,] { { weights2[0, 0], weights2[0, 1] } }, outputsig1);
                var grad3 = multiplyMatrix(d3, Transpose(augoutput1));
                var grad2 = multiplyMatrix(d2, Transpose(input));
                var ratedGrad2 = multiplyMatrix(.3, grad2);
                var ratedGrad3 = multiplyMatrix(.3, grad3);
                weights2 = subtractMatrix(weights2, ratedGrad3);
                weights1 = subtractMatrix(weights1, ratedGrad2);

            }

            var va = 1;
            var val = 1;
            var inpu = new double[,] { { va }, { val }, { 1 } };
            var output = multiplyMatrix(weights1, inpu);
            var outputsig = Sigmoid(output);
            var augoutput = new double[,] { { outputsig[0, 0] }, { outputsig[1, 0] }, { 1 } };
            var outpu = multiplyMatrix(weights2, augoutput);
            var outputsi = Sigmoid(outpu);
            Console.WriteLine(outputsi[0,0]);

             va = 0;
             val = 1;
             inpu = new double[,] { { va }, { val }, { 1 } };
             output = multiplyMatrix(weights1, inpu);
             outputsig = Sigmoid(output);
             augoutput = new double[,] { { outputsig[0, 0] }, { outputsig[1, 0] }, { 1 } };
            outpu = multiplyMatrix(weights2, augoutput);
             outputsi = Sigmoid(outpu);
            Console.WriteLine(outputsi[0, 0]);

             va = 1;
             val = 0;
             inpu = new double[,] { { va }, { val }, { 1 } };
             output = multiplyMatrix(weights1, inpu);
             outputsig = Sigmoid(output);
             augoutput = new double[,] { { outputsig[0, 0] }, { outputsig[1, 0] }, { 1 } };
            outpu = multiplyMatrix(weights2, augoutput);
             outputsi = Sigmoid(outpu);
            Console.WriteLine(outputsi[0, 0]);

             va = 0;
             val = 0;
             inpu = new double[,] { { va }, { val }, { 1 } };
             output = multiplyMatrix(weights1, inpu);
             outputsig = Sigmoid(output);
             augoutput = new double[,] { { outputsig[0, 0] }, { outputsig[1, 0] }, { 1 } };
            outpu = multiplyMatrix(weights2, augoutput);
             outputsi = Sigmoid(outpu);
            Console.WriteLine(outputsi[0, 0]);
        }


        public static void printMatrix(double[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    Console.Write(a[i,j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static double[,] multiplyMatrix(double[,] a, double[,] b)
        {
            double[,] c = new double[a.GetLength(0), b.GetLength(1)];

            
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    for (int j = 0; j < b.GetLength(1); j++)
                    {
                        c[i, j] = 0;
                        for (int k = 0; k < a.GetLength(1); k++)
                        {
                            c[i, j] += a[i, k] * b[k, j];
                        }
                    }
                }

            return c;
        }

        public static double[,] elementwiseMultiplyMatrix(double[,] a, double[,] b)
        {
            double[,] c = new double[a.GetLength(0), b.GetLength(1)];


            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    c[i, j] = a[i, j] * b[i, j];
                }
            }

            return c;
        }

        public static double[,] multiplyMatrix(double a, double[,] b)
        {
            double[,] c = new double[b.GetLength(0), b.GetLength(1)];


            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    c[i, j] = a * b[i, j];
                }
            }

            return c;
        }

        public static double[,] subtractMatrix(double[,] a, double[,] b)
        {
            double[,] c = new double[a.GetLength(0), a.GetLength(1)];


            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    c[i, j] = a[i, j] - b[i, j];
                }
            }

            return c;
        }

        public static double[,] subtractMatrix(double a, double[,] b)
        {
            double[,] c = new double[b.GetLength(0), b.GetLength(1)];


            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    c[i, j] = a - b[i, j];
                }
            }

            return c;
        }

        public static double[,] Transpose(double[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            double[,] result = new double[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return result;
        }

        public static double[,] Sigmoid(double[,] a)
        {
            double[,] c = new double[a.GetLength(0), a.GetLength(1)];

            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    c[i, j] = 1.0 / (1.0 + Math.Exp(- a[i, j]));
                }
            }
            return c;
        }

        public static double[,] CalculateFirstErrorGradient(double[,] d, double[,] w, double[,] a2)
        {
            var multiplied = elementwiseMultiplyMatrix(multiplyMatrix(Transpose(w),d), elementwiseMultiplyMatrix(a2, subtractMatrix(1, a2)));
            return multiplied;
        }

        public static double[,] CalculateLastErrorGradient(double[,] target, double[,] output)
        {
            var TMinusO = subtractMatrix(target, output);
            var oneMinus0 = subtractMatrix(1.0, output);
            var multiplied = elementwiseMultiplyMatrix(TMinusO, output);
            multiplied = elementwiseMultiplyMatrix(multiplied, oneMinus0);
            multiplied = multiplyMatrix(-1, multiplied);
            return multiplied;
        }
    }
}
