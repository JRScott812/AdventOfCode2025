using System;
using System.IO;

namespace Day_4
{
	internal class Program
	{
		static void Main(string[] args)
		{
			char[,] map = LoadMap(args[0]);
			bool[,] selected = new bool[map.GetLength(1), map.GetLength(1)];

			int grandTotal = 0;
			int previousGrandTotal = grandTotal - 1;

			int rounds = 1;
			for (; previousGrandTotal != grandTotal; rounds++)
			{
				int currentTotal = 0;
				for (int i = 0; i < map.GetLength(0); i++)
				{
					for (int j = 0; j < map.GetLength(1); j++)
					{
						switch (map[i, j])
						{
							case '@':
								int adjacent = CountAdjacent(i, j, map);
								if (adjacent < 4)
								{
									currentTotal++;
									selected[i, j] = true;
								}
								break;
							default:
								break;
						}
					}
				}

				previousGrandTotal = grandTotal;
				grandTotal += currentTotal;

				// remove the selected rolls
				for (int i = 0; i < map.GetLength(0); i++)
				{
					for (int j = 0; j < map.GetLength(1); j++)
					{
						if (selected[i, j])
						{
							map[i, j] = '.';
						}
					}
				}

				if (currentTotal == 0)
				{
					break;
				}
			}
			// Display the picked rolls
			for (int i = 0; i < map.GetLength(0); i++)
			{
				for (int j = 0; j < map.GetLength(1); j++)
				{
					if (selected[i, j])
					{
						Console.Write('x');
					}
					else
					{
						Console.Write(map[i, j]);
					}
				}
				Console.WriteLine();
			}
			Console.WriteLine();
			Console.WriteLine($"Forklifts can pick up {grandTotal} rolls of paper after {rounds} rounds.");
		}

		public static char[,] LoadMap(string filePath)
		{
			// true = forklift, false = paper roll, null = empty

			string[] lines = File.ReadAllLines(filePath);

			char[,] map = new char[lines.Length, lines[0].Length];
			for (int i = 0; i < lines.Length; i++)
			{
				for (int j = 0; j < lines.Length; j++)
				{
					map[i, j] = lines[i][j];
				}
			}

			return map;
		}

		public static int CountAdjacent(int row, int col, char[,] map)
		{
			int count = 0;
			for (int i = -1; i <= 1; i++)
			{
				int currentRow = row + i;
				if (currentRow >= 0 && currentRow < map.GetLength(0))
				{
					for (int j = -1; j <= 1; j++)
					{
						int currentCol = col + j;
						if (currentCol >= 0 && currentCol < map.GetLength(1))
						{
							// no need to check the tile that the forklift itself is on
							if (!(i == 0 && j == 0))
							{
								if (map[currentRow, currentCol] == '@')
								{
									count++;
								}
							}
						}
					}
				}
			}
			return count;
		}
	}
}
