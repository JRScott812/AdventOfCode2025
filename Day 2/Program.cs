using System;
using System.Collections.Generic;
using System.IO;

namespace Day_2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<ulong> invalidIDs = [];

			(ulong, ulong)[] ranges = DetermineRanges(args[0]);

			foreach ((ulong, ulong) range in ranges)
			{
				for (ulong i = range.Item1; i <= range.Item2; i++)
				{
					if (i.ToString()[0] == '0')
					{
						invalidIDs.Add(i);
					}
					else if (DoesRepeat(i.ToString()))
					{
						invalidIDs.Add(i);
					}
				}
			}
			Console.WriteLine($"There are {invalidIDs.Count} invalid IDs.");
			Console.Write($"The invalid IDs total to: ");
			ulong total = 0;
			foreach (ulong ID in invalidIDs)
			{
				total += ID;
			}
			Console.WriteLine(total);
		}

		public static (ulong, ulong)[] DetermineRanges(string filePath)
		{
			string validRangesText = File.ReadAllText(filePath);
			string[] rangesText = validRangesText.Split(',');
			(ulong, ulong)[] ranges = new (ulong, ulong)[rangesText.Length];

			for (ulong i = 0; i < (ulong)rangesText.LongLength; i++)
			{
				string[] parts = rangesText[i].Split('-');
				ulong min = ulong.Parse(parts[0]);
				ulong max = ulong.Parse(parts[1]);

				ranges[i] = (min, max);
			}

			return ranges;
		}

		public static bool IsDoubled(string input)
		{
			if (input.Length % 2 == 0)
			{
				if (input[0..(input.Length / 2)] == input[(input.Length / 2)..])
				{
					return true;
				}
				return false;
			}
			return false;
		}

		// If the string has a repeating pattern
		public static bool DoesRepeat(string input)
		{
			for (int splitSize = 1; splitSize <= input.Length / 2; splitSize++)
			{
				// Only check if the length divides evenly
				if (input.Length % splitSize != 0)
				{
					continue;
				}

				bool isRepeating = true;
				string firstPiece = input[0..splitSize];

				// Compare each subsequent piece to the first
				for (int i = splitSize; i <= input.Length - splitSize; i += splitSize)
				{
					int j = i + splitSize;

					if (input[i..j] != firstPiece)
					{
						isRepeating = false;
						break;
					}
				}

				if (isRepeating)
				{
					return true;
				}
			}

			return false;
		}
	}
}
