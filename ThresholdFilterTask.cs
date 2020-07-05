using System;
using System.Security.Cryptography.X509Certificates;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double GetThreshold(double[,] original, double whitePixelsFraction)
		{
			var width = original.GetLength(0);
			var height = original.GetLength(1);
			var pixels = new double[width * height];

			for (int x = 0; x < width; x++)
				for (int y = 0; y < height; y++)
					pixels[y + x * height] = original[x, y];
			Array.Sort(pixels);

			var whitePixelCount = (int)(whitePixelsFraction * original.Length);

			if (whitePixelCount != 0) return pixels[pixels.Length - whitePixelCount];
			else return Double.MaxValue;
		}

		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
			var width = original.GetLength(0);
			var height = original.GetLength(1);
			var result = new double[width, height];

			for (int x = 0; x < width; x++)
				for (int y = 0; y < height; y++)
				{
					if (original[x, y] >= GetThreshold(original, whitePixelsFraction))
						result[x, y] = 1.0;
					else result[x, y] = 0.0;
				}

			return result;
		}
	}
}