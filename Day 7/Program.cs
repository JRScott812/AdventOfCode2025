using System;
using System.Collections.Generic;
using System.IO;

namespace Day_7
{
	internal class Program
	{
		static void Main(string[] args)
		{
			char[,] map = LoadFile(args[0]);

			int splits = CountSplits(map);

			Console.WriteLine($"The beam splits {splits} times.");
		}

		public static char[,] LoadFile(string filePath)
		{
			string[] lines = File.ReadAllLines(filePath);

			char[,] map = new char[lines.Length, lines[0].Length];

			for (int row = 0; row < lines.Length; row++)
			{
				for (int col = 0; col < lines[row].Length; col++)
				{
					map[row, col] = lines[row][col];
				}
			}

			return map;
		}

		public static int CountSplits(char[,] map)
		{
			List<(int, int)> countedSplitters = [];

			/// find the beam beginning
			(int, int) beamStartIndex = (0, -1);
			for (int col = 0; col < map.GetLength(1); col++)
			{
				if (map[0, col] == 'S')
				{
					beamStartIndex.Item2 = col;
					break;
				}
			}

			TrackBeam(beamStartIndex.Item1, beamStartIndex.Item2, map, countedSplitters);

			return countedSplitters.Count;
		}


		/// <summary>Make offshoots of the beam on the left and right, and add the split position to the list.</summary>
		public static ((int, int), (int, int)) Split(int row, int col, List<(int, int)> countedSplitters)
		{
			countedSplitters.Add((row, col));

			return ((row, col - 1), (row, col + 1));
		}

		public static void TrackBeam(int beamStartRow, int beamStartCol, char[,] map, List<(int, int)> countedSplitters)
		{
			/// check that the beam hasn't gone out of bounds
			if (beamStartCol < 0 || beamStartCol >= map.GetLength(1))
			{
				return;
			}

			for (int row = beamStartRow + 1; row < map.GetLength(0); row++)
			{
				if (map[row, beamStartCol] == '^')
				{
					/// only process this splitter if we haven't counted it yet
					if (!countedSplitters.Contains((row, beamStartCol)))
					{
						((int, int), (int, int)) newBeams = Split(row, beamStartCol, countedSplitters);

						/// track the new beams
						TrackBeam(newBeams.Item1.Item1, newBeams.Item1.Item2, map, countedSplitters);
						TrackBeam(newBeams.Item2.Item1, newBeams.Item2.Item2, map, countedSplitters);

						/// Stop this current beam from being tracked once it splits
						return;
					}
					else
					{
						/// Stop tracking, since this path has already been counted
						return;
					}
				}
			}
		}
	}
}
