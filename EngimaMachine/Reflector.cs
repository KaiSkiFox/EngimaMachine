class Reflector : Rotor
{
	/// <summary>
	/// constructor that generate its own reflector exchange table.
	/// </summary>
	public Reflector()
	{
		this.ExchangeTable = Reflector.PopulateTable();
	}

	/// <summary>
	/// Constructor that takes in preset table.
	/// </summary>
	public Reflector(int[] exchangeTable)
	{
		this.ExchangeTable = exchangeTable;
	}
	
	/// <summary>
	/// Simple look up for the numbered pair.
	/// </summary>
	public int Query (int requestInput)
	{
		return ExchangeTable[requestInput];
	}

	/// <summary>
	/// A table generator that generate a table of randomly selected number pairs
	/// </summary>
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