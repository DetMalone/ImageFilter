using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        public static double[] GetWindow(double[,] original, int x, int y)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var window = new List<double>();

            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                    if (x + i > -1 && x + i < width && y + j < height && y + j > -1)
                        window.Add(original[x + i, y + j]);

            return window.ToArray();
        }

        public static double GetWindowMedian(double[,] original, int x, int y)
        {
            var window = GetWindow(original, x, y);
            Array.Sort(window);

            if (window.Length % 2 != 0) return window[window.Length / 2];
            else return (window[window.Length / 2] + window[window.Length / 2 - 1]) / 2;
        }

        public static double[,] MedianFilter(double[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var result = new double[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    result[x, y] = GetWindowMedian(original, x, y);

            return result;
        }
    }
}