namespace Recognizer
{
	public static class GrayscaleTask
	{
		public static double[,] ToGrayscale(Pixel[,] original)
		{
			var result = new double[original.GetLength(0), original.GetLength(1)];
			var width = original.GetLength(0);
			var height = original.GetLength(1);

			for (int x = 0; x < width; x++)
				for (int y = 0; y < height; y++)
					result[x, y] = (0.299 * original[x, y].R + 0.587 * original[x, y].G + 0.114 * original[x, y].B) / 255;

			return result;
		}
	}
}