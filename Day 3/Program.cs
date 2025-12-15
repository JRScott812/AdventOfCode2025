using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_3
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int[][] voltages = GetVoltages(args[0]);
			long[] jolts = GetJolts(voltages);

			Console.WriteLine($"The total combined joltage is: {jolts.Sum()}");
		}

		static int[][] GetVoltages(string filePath)
		{
			string[] lines = File.ReadAllLines(filePath);

			// Populate the array
			int linesCount = lines.Length;
			int itemsPerLine = lines[0].Length;
			int[][] voltages = new int[linesCount][];
			for (int i = 0; i < linesCount; i++)
			{
				voltages[i] = new int[itemsPerLine];
			}

			for (int i = 0; i < lines.Length; i++)
			{
				for (int j = 0; j < lines[i].Length; j++)
				{
					voltages[i][j] = int.Parse(lines[i][j].ToString());
				}
			}

			return voltages;
		}

		public static long[] GetJolts(int[][] voltages)
		{
			long[] jolts = new long[voltages.Length];
			for (int i = 0; i < voltages.Length; i++)
			{
				jolts[i] = CombineJoltValues(DetermineHighestJolt(voltages[i], 12));
			}
			return jolts;
		}

		public static int[] DetermineHighestJolt(int[] array, int joltCounts)
		{
			int digitsToRemove = array.Length - joltCounts;

			/// Digit's index and value
			List<int> digits = new(joltCounts);

			for (int i = 0; i < array.Length; i++)
			{
				int value = array[i];

				// Remove smaller digits from the end if current digit is larger
				while (digits.Count > 0 && digitsToRemove > 0 && digits[^1] < value)
				{
					digits.RemoveAt(digits.Count - 1);
					digitsToRemove--;
				}

				digits.Add(value);
			}

			// Return only the first joltCounts digits
			return [.. digits.Take(joltCounts)];
		}

		public static long CombineJoltValues(int[] jolts) => long.Parse(string.Join(string.Empty, jolts));
	}
}
