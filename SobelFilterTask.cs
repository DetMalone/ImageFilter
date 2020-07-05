using System;
using System.Windows.Forms.VisualStyles;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] ConveyMatrix(double[,] matrix)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            var transporatedMatrix = new double[height, width];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    transporatedMatrix[j, i] = matrix[i, j];

            return transporatedMatrix;
        }

        public static double SobelOperator(double[,] g, double[,] sobelMatrix, int x, int y)
        {
            var width = sobelMatrix.GetLength(0);
            var height = sobelMatrix.GetLength(1);
            double result = 0;

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    result += sobelMatrix[i, j] * g[x - width / 2 + i, y - height / 2 + j];

            return result;
        }

        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var widthG = g.GetLength(0);
            var heightG = g.GetLength(1);
            var widthS = sx.GetLength(0);
            var heightS = sx.GetLength(1);
            var result = new double[widthG, heightG];
            var sy = ConveyMatrix(sx);

            for (int x = widthS / 2; x < widthG - widthS / 2; x++)
                for (int y = heightS / 2; y < heightG - heightS / 2; y++)
                {
                    var gx = SobelOperator(g, sx, x, y);
                    var gy = SobelOperator(g, sy, x, y);
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }

            return result;
        }
    }
}