namespace Day_1
{
	internal class Program
	{
		static void Main()
		{
			int index = 50;
			int timesAtZero = 0;
			int timesPassedZero = 0;

			string[] lines = File.ReadAllLines("..\\..\\..\\input\\test input.txt");

			foreach (string line in lines)
			{
				char directionChar = line[0];
				// false is left, true is right
				bool direction = directionChar == 'R';
				int value = int.Parse(line.Substring(1, line.Length - 1));
				// If direction is left, then the value should be negative
				if (!direction)
				{
					value *= -1;
				}

				index += value;
				if (index > 99)
				{
					int factorOffset = (index / 100) + 1;
					index -= 100 * factorOffset;
					timesPassedZero += factorOffset;
				}
				if (index < 0)
				{
					int factorOffset = (index / -100) + 1;
					index += 100 * factorOffset;
					timesPassedZero += factorOffset;
				}

				if (index == 0)
				{
					timesAtZero++; 
				}

				Console.WriteLine($"Moved {directionChar}\t\tby: {Math.Abs(value)}\t\tnow at {index}");
			}

			Console.WriteLine($"Pointed at 0 {timesAtZero} times.");
			Console.WriteLine($"Passed 0 {timesPassedZero} times.");
			Console.WriteLine($"Total {timesAtZero} + {timesPassedZero} = {timesAtZero + timesPassedZero}");
		}
	}
}
