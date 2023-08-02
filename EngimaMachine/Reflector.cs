class Reflector : Rotor
{
	// constructor that generate its own reflector exchange table.
	public Reflector()
	{
		this.ExchangeTable = Reflector.PopulateTable();
	}

	// Constructor that takes in preset table.

	public Reflector(int[] exchangeTable)
	{
		this.ExchangeTable = exchangeTable;
	}
	

	// Simple look up for the numbered pair.
	public int Query (int requestInput)
	{
		return ExchangeTable[requestInput];
	}

	// A table generator that generate a table of randomly selected number pairs

	public static int[] PopulateTable()
	{
		var rand = new Random();
		int[] inTable = Enumerable.Range(13, 13).ToArray();
		int[] outTable = Enumerable.Range(0, 13).ToArray();
		int[] table = new int[26];

		for (int i = 0; i < 13; i++)
		{
			int insert = rand.Next(inTable.Length);
			int extract = rand.Next(outTable.Length);
			table[outTable[insert]] = inTable[extract];
			table[inTable[extract]] = outTable[insert];

			for (int j = insert; j < outTable.Length - 1; j++)
			{
				outTable[j] = outTable[j + 1];
			}

			for (int j = extract; j < inTable.Length - 1; j++)
			{
				inTable[j] = inTable[j + 1];
			}

			Array.Resize<int>(ref inTable, inTable.Length - 1);
			Array.Resize<int>(ref outTable, outTable.Length - 1);
		}
		return table;
	}
}