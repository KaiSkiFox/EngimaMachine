class Rotor
{
	// look up table for the random interger, [0] -> 4 etc. An array of 26 interger and non-repeating random interger contained within.
	public int[] ExchangeTable { get; set; }
	
	// the cascade number which allows the next rotor to rotate.
	private int rotateSequence;
	public int RotateSequence 
	{
		get
		{
			return this.rotateSequence;
		}
		set
		{
			if (value < 0)
			{
				this.rotateSequence = 26 + value;
			}
			else if (value > 26)
			{
				this.rotateSequence = 26 - value;
			}
			else
			{
				this.rotateSequence = value;
			}
		}
	}
	// empty constructor for reflector.
	public Rotor()
	{
		var rand = new Random();
		RotateSequence = rand.Next(0, 26);
		ExchangeTable = PopulateTable();
	}

	public Rotor(int[] exchangeTable)
	{
		var rand = new Random();
		RotateSequence = rand.Next(0, 26);
		ExchangeTable =  exchangeTable;
	}

	// Rotor COnstructor, select the sequence for cascade and generate a random table.
	public Rotor(int rotateSequence)
	{
		if (RotateSequence < 0 || rotateSequence > 26)
		{
			this.RotateSequence = 0;
		}

		this.RotateSequence = rotateSequence;
		this.ExchangeTable = PopulateTable();
	}

	// Rotor Constructor, select the sequence for cascade and preset table.
	public Rotor(int rotateSequence, int[] exchangeTable)
	{
		if (RotateSequence < 0 || rotateSequence > 26)
		{
			this.RotateSequence = 0;
		}

		this.RotateSequence = rotateSequence;
		this.ExchangeTable = exchangeTable;
	}

	// Query exchange table and return the interger(alphabet) back.
	public int Encode (int requestInput)
	{
		return ExchangeTable[requestInput];
	}

	//back tracing roto table.
	public int Decode (int requestInput)
	{
		return Array.IndexOf(ExchangeTable, requestInput);
	}

	//generating randomized rotor table
	public static int[] PopulateTable()
	{
		var rand = new Random();
		int[] intTable = Enumerable.Range(0, 26).ToArray();
		int[] table = new int[26];

		for (int i = 0; i < 26; i++)
		{
			int extract = rand.Next(intTable.Length);
			// Console.WriteLine($"{intTable[extract]}"); //To generate Tables for set rotors
			table[i] = intTable[extract];

			for (int j = extract; j < intTable.Length - 1; j++)
			{
				intTable[j] = intTable[j + 1];
			}

			Array.Resize<int>(ref intTable, intTable.Length - 1);
		}
		Console.WriteLine("\n");

		return table;
	}

	public void AlterAlphabet(int length)
	{
		int holder;
		for (int i = 0; i < ExchangeTable.Length; i++)
		{
			holder = ExchangeTable[i] - length;
			ExchangeTable[i] = (holder < 0) ? 26 + holder : holder;
		}
		// holder = RotateSequence - length;
		// RotateSequence = (holder < 0) ? 26 + holder : holder;
	}

	//alter rotor starting position
	public void ForwardRotate(int length)
	{
		if (length > 1)
		{
			int[] longRotate = new int[length];
			for (int i = 0; i < length; i++)
			{
				longRotate[i] = ExchangeTable[i];
			}

			for (int i = length; i < 26; i++)
			{
				ExchangeTable[i - length] = ExchangeTable[i];
			}

			int spacer = 26 - length;
			for (int i = spacer; i < 26; i++)
			{
				ExchangeTable[i] = longRotate[i - spacer];
			}
		}
		else
		{
			int zeroIndex = ExchangeTable[0];
			for (int i = 0; i < 25; i++)
			{
				ExchangeTable[i] = ExchangeTable[i + 1];
			}
			ExchangeTable[25] = zeroIndex;
		}
		RotateSequence = (RotateSequence - length < 0) ? 26 + RotateSequence - length : RotateSequence - length;
	}

	//alter rotor starting position
	public void BackwardRotate(int length)
	{
		if (length > 1)
		{
			int spacer = 25 - length;
			int[] longRotate = new int[length];
			for (int i = 0; i < length; i++)
			{
				longRotate[i] = ExchangeTable[spacer + i + 1];
			}

			for (int i = spacer; i >= 0; i--)
			{
				ExchangeTable[i + length] = ExchangeTable[i];
			}

			for (int i = 0; i < length; i++)
			{
				ExchangeTable[i] = longRotate[i];
			}
		}
		else
		{
			int zeroIndex = ExchangeTable[25];
			for (int i = 25; i >= 0; i--)
			{
				ExchangeTable[i] = ExchangeTable[i-1];
			}
			ExchangeTable[0] = zeroIndex;
		}
	}

// Rotate the current Rotor and check if the cascade is needed (next rotor rotation) need to consider !!! Double stepping of the middle rotor !!!
	public bool RotateTable(bool inCascade)
	{
		bool cascadeSignal = RotateSequence == 1;

		if (inCascade || RotateSequence == 1)
		{
			int initial = (ExchangeTable[0] != 0) ? ExchangeTable[0] - 1 : 25 ;
			for (int i = 0; i < 25; i++)
			{
				ExchangeTable[i] = (ExchangeTable[i+1] != 0) ? ExchangeTable[i+1] - 1 : 25;
			}
			ExchangeTable[25] = initial;
			RotateSequence = (cascadeSignal) ? 26 : RotateSequence - 1;
		}
		return cascadeSignal;
	}
}