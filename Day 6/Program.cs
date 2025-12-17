using System;
using System.IO;
using System.Linq;

namespace Day_6
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[][] problems = LoadFile(args[0]);
			long[] values = GetValues(problems);

			Console.WriteLine($"The total result is {values.Sum()}");
		}

		public static string[][] LoadFile(string filePath)
		{
			string[] lines = File.ReadAllLines(filePath);

			int size = lines[0].Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries).Length;

			string[][] problems = new string[size][];
			// Allocate inner arrays
			for (int col = 0; col < problems.Length; col++)
			{
				problems[col] = new string[lines.Length];
			}

			for (int row = 0; row < lines.Length; row++)
			{
				for (int col = 0; col < size; col++)
				{
					string valueText = lines[row].Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries)[col];
					problems[col][row] = valueText;
				}
			}

			return problems;
		}

		public static long[] GetValues(string[][] values)
		{
			long[] results = new long[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				long total = 0;
				int[] parsedValues = new int[values[i].Length - 1];
				for (int j = 0; j < values[i].Length - 1; j++)
				{
					parsedValues[j] = int.Parse(values[i][j]);
				}

				// Then get the operator
				string op = values[i][^1];

				// Perform the operation
				if (op == "+")
				{
					total = parsedValues.Sum();
				}
				else if (op == "*")
				{
					total = 1;
					foreach (int val in parsedValues)
					{
						total *= val;
					}
				}
				results[i] = total;
			}
			return results;
		}
	}
}
