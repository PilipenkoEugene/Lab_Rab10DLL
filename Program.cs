using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10Dll
{
    public class Matrix
    {
        private float[,] matrix;
        private int m, n;

        public Matrix() { }

        public Matrix(int m, int n)
        {
            matrix = new float[m, n];
        }

        public void GenerateMatrix(int M, int N)
        {
            m = M;
            n = N;

            var r = new Random();

            matrix = new float[M, N];

            for (var i = 0; i < M; i++)
            {
                for (var j = 0; j < N; j++)
                    matrix[i, j] = (float)r.NextDouble() * 1.0277F;
            }
        }

        public void SaveMatrix(string pFileName)
        {
            if (File.Exists(pFileName))
                File.Delete(pFileName);

            var f = new FileInfo(pFileName);
            var tw = f.CreateText();

            tw.WriteLine(m);
            tw.WriteLine(n);

            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n; j++)
                    tw.WriteLine($"{i} {j} {matrix[i, j]:E10}");
            }

            tw.Close();
        }

        public bool LoadMatrix(string pFileName)
        {
            if (!File.Exists(pFileName))
                return false;

            try
            {
                var tr = File.OpenText(pFileName);
                m = int.Parse(tr.ReadLine());
                n = int.Parse(tr.ReadLine());

                matrix = new float[m, n];

                for (var i = 0; i < m; i++)
                {
                    for (var j = 0; j < n; j++)
                    {
                        var line = tr.ReadLine();
                        var substring = line.Split(' ');
                        matrix[i, j] = float.Parse(substring[2]);
                    }
                }

                tr.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void PrintMatrix()
        {
            if (matrix.Length <= 0)
                return;

            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n; j++)
                    Console.Write($"{matrix[i, j]:E3} ");

                Console.WriteLine();
            }
        }

        public float SumOfAllElements()
        {
            float sum = 0;

            for(var i = 0; i < m; i++)
            {
                for(var j = 0; j < n; j++)
                {
                    sum += matrix[i, j];
                }
            }

            return sum;
        }

        public float DiagonalProduct()
        {
            float product = 1;
            int dim = Math.Min(m, n);

            for(var i = 0; i < dim; i++)
            {
                product *= matrix[i,i];
            }

            return product;
        }
    }
}
