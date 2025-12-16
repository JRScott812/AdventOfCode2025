using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_5
{
	internal class Program
	{
		static void Main(string[] args)
		{
			((long, long)[] ranges, long[] IDs) = LoadFile(args[0]);
			int numberOfFresh = 0;

			foreach (long ID in IDs)
			{
				foreach ((long start, long end) in ranges)
				{
					if (ID >= start && ID <= end)
					{
						numberOfFresh++;
						break;
					}
				}
			}
			Console.WriteLine($"There are {numberOfFresh} fresh ingredients.");
			Console.WriteLine();

			/// Must use sorting instead of previous attempt of brute forcing
			List<(long start, long end)> sortedRanges = [.. ranges.OrderBy(r => r.Item1)];

			/// Merge overlapping or adjacent ranges
			List<(long start, long end)> mergedRanges = [];
			(long currentStart, long currentEnd) = sortedRanges[0];

			for (int i = 1; i < sortedRanges.Count; i++)
			{
				(long start, long end) = sortedRanges[i];

				/// If ranges overlap or are adjacent, merge them
				if (start <= currentEnd + 1)
				{
					currentEnd = Math.Max(currentEnd, end);
				}
				else
				{
					/// No overlap, save current range and start new one
					mergedRanges.Add((currentStart, currentEnd));
					currentStart = start;
					currentEnd = end;
				}
			}
			/// Add the last range
			mergedRanges.Add((currentStart, currentEnd));

			/// Count total IDs across all merged ranges
			long total = 0;
			foreach ((long start, long end) in mergedRanges)
			{
				total += end - start + 1;
			}

			Console.WriteLine($"There are {total} IDs for fresh ingredients.");
		}

		public static ((long, long)[], long[]) LoadFile(string filePath)
		{
			string[] lines = File.ReadAllLines(filePath);

			// find the newline split
			int? splitIndex = null;
			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i] == string.Empty)
				{
					splitIndex = i;
				}
			}
			if (splitIndex is null)
			{
				throw new FileLoadException("Invalid file format!", filePath);
			}

			// Get lines of the values we want
			string[] rangeStrings = lines[..(int)splitIndex];
			string[] IDStrings = lines[((int)splitIndex + 1)..];

			// convert them from strings
			// Ranges
			(long, long)[] ranges = new (long, long)[rangeStrings.Length];
			for (int i = 0; i < rangeStrings.Length; i++)
			{
				string[] values = rangeStrings[i].Split('-');
				long min = long.Parse(values[0]);
				long max = long.Parse(values[1]);

				ranges[i] = (min, max);
			}

			// IDs
			long[] IDs = new long[IDStrings.Length];
			for (int i = 0; i < IDStrings.Length; i++)
			{
				string valueString = IDStrings[i];
				long value = long.Parse(valueString);

				IDs[i] = value;
			}

			return (ranges, IDs);
		}
	}
}
