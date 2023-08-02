class PlugBoard
{
	private readonly string[] ALPHABET = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};

	public int[] BoardTable { get; private set; }

	public PlugBoard()
	{
		BoardTable = PlugBoardConfig();
	}

	public PlugBoard(int[] boardTable)
	{
		BoardTable = boardTable;
	}

	public int[] PlugBoardConfig()
	{
		int[] BoardTable = Enumerable.Range(0, 26).ToArray();
		Console.Clear();
		Console.WriteLine("Do you wish to alter the plugboard on the engima machine? (y/n)");
		bool exit = false;
		string userInput;
		if (Console.ReadKey(true).Key == ConsoleKey.Y)
		{
			do
			{
				Console.WriteLine("\nHow would you wish to alter the plugboard?");
				string firstLetter = Console.ReadLine().ToUpper();
				while (firstLetter == null || firstLetter.Length > 1) 
				{
					Console.WriteLine("Invaild input, try again:");
					firstLetter = Console.ReadLine();
				}
				int letterIndex1 = Array.IndexOf(ALPHABET, firstLetter);

				Console.WriteLine("Which Letter you wish to connect it to?");
				string secondLetter = Console.ReadLine().ToUpper();
				while (secondLetter == null || secondLetter.Length > 1) 
				{
					Console.WriteLine("Invaild input, try again:");
					secondLetter = Console.ReadLine();
				}
				int letterIndex2 = Array.IndexOf(ALPHABET, secondLetter);

				Console.WriteLine($"Switching {firstLetter} with {secondLetter}");

				if (BoardTable[letterIndex1] != letterIndex1 || BoardTable[letterIndex2] != letterIndex2)
				{
					Console.WriteLine("The connection was already made, please choose other letter pair. Or if you wish to exit, type in 'finish'.");
					userInput = Console.ReadLine().ToUpper();
					if (userInput == "FINISH")
					{
						break;
					}
					continue;
				}

				BoardTable[letterIndex1] = letterIndex2;
				BoardTable[letterIndex2] = letterIndex1;

				Console.WriteLine("Current plugboard config.");
				foreach (string letter in ALPHABET)
				{
					Console.Write(letter + " ");
				}
				Console.Write("\n");

				foreach (int i in BoardTable)
				{
					Console.Write(ALPHABET[i] + " ");
				}

				Console.WriteLine("\nTo exit press (Esc).");
			} while (Console.ReadKey(true).Key == ConsoleKey.Escape);
		}
		else
		{
			Console.WriteLine("Plugboard remained in default mode.\n");
		}

		return BoardTable;
	}
}