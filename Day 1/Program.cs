namespace Day_1
{
	internal class Program
	{
		static void Main()
		{
			int index = 50;
			int timesAtZero = 0;
			int timesPassedZero = 0;

			string[] lines = File.ReadAllLines("..\\..\\..\\input\\input.txt");

			foreach (string line in lines)
			{
				char directionChar = line[0];
				// false is left, true is right
				bool direction = directionChar == 'R';
				int value = int.Parse(line.Substring(1));
				// If direction is left, then the value should be negative
				if (!direction)
				{
					value *= -1;
				}

				int steps = Math.Abs(value);
				int hitsDuring = 0;

				// Count the number of times the dial points at 0 during the rotation.
				// - For right moves: any time we advance past multiples of 100.
				// - For left moves: solve for t in [1..steps] where (index - t) mod 100 == 0.
				if (value > 0) // right
				{
					hitsDuring = (index + steps) / 100;
				}
				else // left
				{
					if (index == 0)
					{
						// starting at 0, hitting 0 during left movement only when a full 100-step occurs
						hitsDuring = steps / 100;
					}
					else if (steps >= index)
					{
						// first hit occurs when we've moved 'index' steps, then every 100 after
						hitsDuring = 1 + (steps - index) / 100;
					}
				}

				timesPassedZero += hitsDuring;

				// Apply movement with wrapping to 0..99
				index = ((index + value) % 100 + 100) % 100;

				if (index == 0)
				{
					timesAtZero++;
				}

				Console.WriteLine($"Moved {directionChar}\t\tby: {steps}\t\tnow at {index} (during: {hitsDuring})");
			}

			Console.WriteLine($"Pointed at 0 {timesAtZero} times.");
			Console.WriteLine($"Passed 0 {timesPassedZero} times.");
		}
	}
}
