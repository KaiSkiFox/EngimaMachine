
class EngimaMachine
{
	private int[][] PreRotorTable { get; set;}

	private PlugBoard PlugBoardSet { get; set; }

	private Rotor[] RotorSets { get; set; }

	private Reflector Reflector { get; set; }

	private readonly string[] ALPHABET = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
	private readonly int[] rotorITable = new int[] {4, 10, 12, 5, 11, 6, 3, 16, 21, 25, 13, 19, 14, 22, 24, 7, 23, 20, 18, 15, 0, 8, 1, 17, 2, 9};  //Notch at 24
	private readonly int[] rotorIITable = new int[] {0, 9, 3, 10, 18, 8, 17, 20, 23, 1, 11, 7, 22, 19, 12, 2, 16, 6, 25, 13, 15, 24, 5, 21, 14, 4}; //Notch at 12
	private readonly int[] rotorIIITable = new int[] {1, 3, 5, 7, 9, 11, 2, 15, 17, 19, 23, 21, 25, 13, 24, 4, 8, 22, 6, 0, 10, 12, 20, 18, 16, 14};//Notch at 3
	private readonly int[] rotorIVTable = new int[] {4, 18, 14, 21, 15, 25, 9, 0, 24, 16, 20, 8, 17, 7, 23, 11, 13, 5, 19, 6, 10, 3, 2, 12, 22, 1}; //Notch at 17
	private readonly int[] rotorVTable = new int[] {21, 25, 1, 17, 6, 8, 19, 24, 20, 15, 18, 3, 13, 7, 11, 23, 0, 22, 12, 9, 16, 14, 5, 4, 2, 10}; //Notch at 7
	private readonly int[] reflectorATable = new int[] {4, 9, 12, 25, 0, 11, 24, 23, 21, 1, 22, 5, 2, 17, 16, 20, 14, 13, 19, 18, 15, 8, 10, 7, 6, 3};
	private readonly int[] reflectorBTable = new int[] {24, 17, 20, 7, 16, 18, 11, 3, 15, 23, 13, 6, 14, 10, 12, 8, 4, 1, 5, 25, 2, 22, 21, 9, 0, 19};
	private readonly int[] reflectorCTable = new int[] {5, 21, 15, 9, 8, 0, 14, 24, 4, 3, 17, 25, 23, 22, 6, 2, 19, 10, 20, 16, 18, 1, 13, 12, 7, 11};
	private readonly int[] plugBoardTest = new int[] {18, 1, 24, 13, 15, 20, 22, 21, 19, 9, 14, 25, 16, 3, 10, 4, 12, 17, 0, 8, 5, 7, 6, 23, 2, 11};
	private readonly int[] plugBoardDefault = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25};


	// default engima machine
	public EngimaMachine()
	{
		Rotor rotor1 = new Rotor(0, rotorITable);
		Rotor rotor2 = new Rotor(0, rotorIITable);
		Rotor rotor3 = new Rotor(0, rotorIIITable);
		Reflector = new Reflector(reflectorATable);
		RotorSets = new Rotor[] {rotor1, rotor2, rotor3};
		PlugBoardSet = new PlugBoard(plugBoardTest);
	}

	// customize engima machine
	public EngimaMachine(int rotorSetA, int rotorSetB, int rotorSetC, int reflector, List<int[]> preRotateTable, bool plugBoardSkip = false)
	{
		var rand = new Random();
		int[] randomRotorA = preRotateTable[0];
		int[] randomRotorB = preRotateTable[1];
		int[] randomRotorC = preRotateTable[2];
		int[] randomReflector = preRotateTable[3];
		int[][] rotorSets = new int[][] {rotorITable, rotorIITable, rotorIIITable, rotorIVTable, rotorVTable, randomRotorA, randomRotorB, randomRotorC};
		int[][] reflectorSets = new int[][] {reflectorATable, reflectorBTable, reflectorCTable, randomReflector};
		int[] rotorNotch = new int[] {17, 5, 22, 10, 26, rand.Next(), rand.Next(), rand.Next()};

		Rotor rotor1 = new Rotor(rotorNotch[rotorSetA], rotorSets[rotorSetA]);
		Rotor rotor2 = new Rotor(rotorNotch[rotorSetB], rotorSets[rotorSetB]);
		Rotor rotor3 = new Rotor(rotorNotch[rotorSetC], rotorSets[rotorSetC]);

		Reflector = new Reflector(reflectorSets[reflector]);
		RotorSets = new Rotor[] {rotor1, rotor2, rotor3};
		if (!plugBoardSkip)
		{
			PlugBoardSet = new PlugBoard();
		}
		else
		{
			PlugBoardSet = new PlugBoard(plugBoardDefault);
		}
	}

	public void RotorSetCascade()
	{
		bool rotorCascade1 = RotorSets[2].RotateTable(true);
		bool rotorCascade2 = RotorSets[1].RotateTable(rotorCascade1);
		RotorSets[0].RotateTable(rotorCascade2);
	}

// Rotor Position vs Notch position??
// Rotor notch changes pointing output by length
// Rotor position changes current position and pointing output by lenght (Simulate moving by length)
	

	public void AlterAllRotorAlphabet(int notch1, int notch2, int notch3)
	{
		this.AlterRotorAlphabet(0, notch1);
		this.AlterRotorAlphabet(1, notch2);
		this.AlterRotorAlphabet(2, notch3);
	}

	public void AlterRotorAlphabet(int rotor, int notch)
	{
		if (notch == 0) { return; }
		RotorSets[rotor].AlterAlphabet(notch);
	}

	public void AlterAllRotorPos(int pos1, int pos2, int pos3)
	{
		this.AlterRotorPosition(0, pos1);
		this.AlterRotorPosition(1, pos2);
		this.AlterRotorPosition(2, pos3);
	}

	public void AlterRotorPosition(int rotor, int starting)
	{
		if (starting == 0) { return; }

		RotorSets[rotor].ForwardRotate(starting);
		RotorSets[rotor].AlterAlphabet(starting);
	}

	public int[] InputTranslationToLetter(string input)
	{
		int[] table = new int[input.Length];
		for (int i = 0; i < input.Length; i++)
		{
			// Console.WriteLine(Array.IndexOf(ALPHABET, input[i].ToString()));
			// int letterIndex = Array.IndexOf(ALPHABET, input[i].ToString());
			// table[i] = (PlugBoardSet.BoardTable[letterIndex] != -1) ? PlugBoardSet.BoardTable[letterIndex] : letterIndex;
			table[i] = Array.IndexOf(ALPHABET, input[i].ToString());
		}

		return table;
	}

	public string RotorSetQuery(string input)
	{
		int inputNum = Array.IndexOf(ALPHABET, input.ToUpper());
		if (inputNum != -1 && inputNum < 26)
		{
			return ALPHABET[RotorSetQuery(inputNum)];
		}
		return input;
	}

	public int RotorSetQuery(int input)
	{
		this.RotorSetCascade();
		input = (PlugBoardSet.BoardTable[input] != -1) ? PlugBoardSet.BoardTable[input] : input;

		int output = RotorSets[2].Encode(input);
		output = RotorSets[1].Encode(output);
		output = RotorSets[0].Encode(output);
		output = Reflector.Query(output);
		output = RotorSets[0].Decode(output);
		output = RotorSets[1].Decode(output);
		output = RotorSets[2].Decode(output);

		return (PlugBoardSet.BoardTable[output] != -1) ? PlugBoardSet.BoardTable[output] : output;
		// TEST BLOCK
		// int mediate;
		// int output = RotorSets[2].Encode(input);
		// Console.WriteLine($"Input {ALPHABET[input]} Transit to {ALPHABET[output]}");
		// mediate = RotorSets[1].Encode(output);
		// Console.WriteLine($"Input {ALPHABET[output]} Transit to {ALPHABET[mediate]}");
		// output = RotorSets[0].Encode(mediate);
		// Console.WriteLine($"Input {ALPHABET[mediate]} Transit to {ALPHABET[output]}");
		// mediate = Reflector.Query(output);
		// Console.WriteLine($"Input {ALPHABET[output]} Transit to {ALPHABET[mediate]}");
		// output = RotorSets[0].Decode(mediate);
		// Console.WriteLine($"Input {ALPHABET[mediate]} Transit to {ALPHABET[output]}");
		// mediate = RotorSets[1].Decode(output);
		// Console.WriteLine($"Input {ALPHABET[output]} Transit to {ALPHABET[mediate]}");
		// output = RotorSets[2].Decode(mediate);
		// Console.WriteLine($"Input {ALPHABET[mediate]} Transit to {ALPHABET[output]}");
		// int plugreturn = (PlugBoardSet.BoardTable[output] != -1) ? PlugBoardSet.BoardTable[output] : output;
		// Console.WriteLine("");
		// return plugreturn;
	}

	public void RotorSettingExport(string rotorLabel1, string rotorLabel2, string rotorLabel3, string reflectorLabel)
	{
		Console.WriteLine("Bitte seien Sie wachsam, wenn Sie die Steckbrettkonfiguration exportieren.".ToUpper());
		Console.WriteLine($"[{rotorLabel1} | {rotorLabel2} | {rotorLabel3} | {reflectorLabel}]");
		Console.WriteLine("  Rotor1 |  Rotor2  |   Rotor3  |  Reflec");
		
		for (int i = 0; i < 26; i++)
		{
			string letterSets = " " + ALPHABET[i];
			letterSets += (i == RotorSets[2].RotateSequence - 1) ? " >=< " : "  -  ";
			letterSets += ALPHABET[RotorSets[2].ExchangeTable[i]] + " | ";
			letterSets += ALPHABET[i];
			letterSets += (i == RotorSets[1].RotateSequence - 1) ? " >=< " : "  -  ";
			letterSets += ALPHABET[RotorSets[1].ExchangeTable[i]] + "  |  ";
			letterSets += ALPHABET[i];
			letterSets += (i == RotorSets[0].RotateSequence - 1) ? " >=< " : "  -  ";
			letterSets += ALPHABET[RotorSets[0].ExchangeTable[i]] + "  | ";
			letterSets += ALPHABET[i] + "  -  " + ALPHABET[Reflector.ExchangeTable[i]];
			Console.WriteLine(letterSets);
		}
	}

}