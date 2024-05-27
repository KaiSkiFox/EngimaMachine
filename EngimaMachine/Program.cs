EngimaMachine engimaMachine;
List<int[]> refRandomRotor = new List<int[]>
{
	Rotor.PopulateTable(),
	Rotor.PopulateTable(),
	Rotor.PopulateTable(),
	Reflector.PopulateTable()
};
int[] rotorSet = new int[] {1, 2, 3, 3};
int[] rotorPos = new int[] {0, 0, 0}; 
int[] rotorNotch = new int[] {0, 0, 0,};
int[][] Rotors = new int[][] {rotorSet, rotorPos, rotorNotch};

string[] rotorLabels = new string[] {"No Label", "Rotor I", "Rotor II", "Rotor III", "Rotor IV", "Rotor V", "Random Wiring", "Random Wiring", "RandomWiring"};
string[] reflectorLabels = new string[] {"No Label", "UKW-A", "UKW-B", "UKW-C", "Random UKW"};
string[][] Labels = new string[][] {rotorLabels, reflectorLabels};
bool exit = true;
Char input;

do
{
	Console.Clear();
	Console.WriteLine("\n                         ######################################");
	Console.WriteLine("                         #     Welcome to Engima Machine.     #");
	Console.WriteLine("                         #   Willkommen bei Engima Machine.   #");
	Console.WriteLine("                         ######################################\n\n");
	Console.WriteLine("Press (a)lter rotor settings, (d)ecode, (e)ncode, (w)iring table export or (Esc) to exit:");

	switch (Console.ReadKey(true).Key)
	{
		case ConsoleKey.A:
			Console.WriteLine("Entering customization Mode...");
			int[] rotorSelection = new int[] {0, 0, 0, 0, 0, 0, 0, 0};
			rotorSet[3] = 0;
			bool breaker = true;
			bool parser = false;
			int useRotorSelection;
			//Rotor selection, 1 = slot A, 2 = slot B, 3 = slot C [0] = Rotor I - [4] = Rotor V [5] Random A [6] Random B [7] Random C
			//[[2],[1],[0],[0],[0],[0],[0],[3]] = slot A [Rotor II] | slot B [Rotor I] | slot C [Random C]

			do
			{
				Console.Clear();
				Console.WriteLine("(a) Rotor slot 1 | (b) Rotor slot 2 | (c) Rotor slot 3 | (d) Reflector | (any key) finish and exit rotor customization");

				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.A: 
						RoterPositionDisect(0, rotorSelection, Rotors, Labels);
						break;

					case ConsoleKey.B:
						RoterPositionDisect(1, rotorSelection, Rotors, Labels);
						break;

					case ConsoleKey.C:
						RoterPositionDisect(2, rotorSelection, Rotors, Labels);
						break;

					case ConsoleKey.D:
						while(true)
						{
							Console.WriteLine("");
							Console.WriteLine("[0] = UKW-A [1] = UKW-B [2] = UKW-C | [3] Random UKW");
							parser = int.TryParse(Console.ReadLine(), out useRotorSelection);
							if (parser && useRotorSelection >= 0 && useRotorSelection < 4)
							{
								rotorSet[3] = useRotorSelection + 1;
								break;
							}
							Console.WriteLine("Invaild Input - Try again.");
						}
						break;

					default: // update to rotor matrix 
						rotorSet[0] = Array.IndexOf(rotorSelection, 1);
						rotorSet[1] = Array.IndexOf(rotorSelection, 2);
						rotorSet[2] = Array.IndexOf(rotorSelection, 3);

						if (rotorSet[0] != -1 && rotorSet[1] != -1 && rotorSet[2] != -1 && rotorSet[3] != 0)
						{
							Console.WriteLine("Machine rotor set complete");
                            Console.WriteLine($"Slot A: {rotorLabels[rotorSet[0]]} - {rotorPos[0] + 1}| Slot B: {rotorLabels[rotorSet[1]]} - {rotorPos[1] + 1} | Slot C: {rotorLabels[rotorSet[2]]} - {rotorPos[2] + 1} | Reflector: {reflectorLabels[reflectorSelection--]}");
							breaker = false;
							Console.WriteLine("Press Enter to continue...");
                            while (Console.ReadKey(true).Key != ConsoleKey.Enter) {}
							break;
						}

						rotorSet[0] = (rotorSet[0] != -1) ? rotorSet[0] : 0;
						rotorSet[1] = (rotorSet[1] != -1) ? rotorSet[1] : 0;
						rotorSet[2] = (rotorSet[2] != -1) ? rotorSet[2] : 0;

						Console.Clear();
						Console.WriteLine("Machine rotor set misconfigured.");
						Console.WriteLine($"Slot A: {rotorLabels[rotorSet[0]]} - {rotorPos[0] + 1}| Slot B: {rotorLabels[rotorSet[1]]} - {rotorPos[1] + 1} | Slot C: {rotorLabels[rotorSet[2]]} - {rotorPos[2] + 1} | Reflector: {reflectorLabels[reflectorSelection]}");
						Thread.Sleep(4000);
						break;
				}
			}while (breaker);
			break;

		case ConsoleKey.E:
			engimaMachine = new EngimaMachine(Rotors[0][0], Rotors[0][1], Rotors[0][2], Rotors[0][3], refRandomRotor);
			engimaMachine.AlterAllRotorPos(Rotors[1][0], Rotors[1][1], Rotors[1][2]);
			engimaMachine.AlterAllRotorNotch(Rotors[2][0], Rotors[2][1], Rotors[2][2]);
			Console.WriteLine("Starting encription. Press Enter to complete:\n");
			
			while ((input = Console.ReadKey(true).KeyChar) != (char)13)
			{
				Console.Write(engimaMachine.RotorSetQuery(input.ToString()));
			}
			Console.WriteLine("\n\n        Encoding complete.\n ##!Pass auf deinen Feind auf!##");
			Console.WriteLine("Press Enter to exit...");
			while (Console.ReadKey(true).Key != ConsoleKey.Enter) {}
			break;

		case ConsoleKey.D:
			engimaMachine = new EngimaMachine(Rotors[0][0], Rotors[0][1], Rotors[0][2], Rotors[0][3], refRandomRotor);
			engimaMachine.AlterAllRotorPos(Rotors[1][0], Rotors[1][1], Rotors[1][2]);
			engimaMachine.AlterAllRotorNotch(Rotors[2][0], Rotors[2][1], Rotors[2][2]);
			Console.WriteLine("Starting decription. Press Enter to complete:\n");

			while ((input = Console.ReadKey(true).KeyChar) != (char)13)
			{
				Console.Write(engimaMachine.RotorSetQuery(input.ToString()));
			}
			Console.WriteLine("\n\n        Decoding complete.\n ##!Pass auf deinen Feind auf!##");
			Console.WriteLine("Press Enter to exit...");
			while (Console.ReadKey(true).Key != ConsoleKey.Enter) {}
			break;

		case ConsoleKey.W:
			engimaMachine = new EngimaMachine(Rotors[0][0], Rotors[0][1], Rotors[0][2], Rotors[0][3], refRandomRotor);
			engimaMachine.AlterAllRotorPos(Rotors[1][0], Rotors[1][1], Rotors[1][2]);
			engimaMachine.AlterAllRotorNotch(Rotors[2][0], Rotors[2][1], Rotors[2][2]);
			engimaMachine.RotorSettingExport(rotorLabels[Rotors[0][0]], rotorLabels[Rotors[0][1]], rotorLabels[Rotors[0][2]], reflectorLabels[Rotors[0][3]]);
			Console.WriteLine("Press Enter to exit...");
			while (Console.ReadKey(true).Key != ConsoleKey.Enter) {}
			break;

		case ConsoleKey.Escape:
			exit = false;
			break;

	}

} while(exit);


/// Adjust rotor information
static void RoterPositionDisect (int altering, int[] rotorSelection, int[][] rotors, string[][] labels)
{

	int checkLetter;
	bool breaker = true;
	int userinput;
	do
	{
		Console.Clear();
		Console.WriteLine($"Altering Rotor slot {altering}, {labels[0][rotors[altering][0]]} Ring set to {labels[0][rotors[altering][1]]}, notch set to {labels[0][rotors[altering][2]]}.");
		Console.WriteLine("(a) Alter rotor set, (b) Alter Starting Position, (c) Alter Table Position. (any key) to exit");

		checkLetter = Array.IndexOf(rotorSelection, altering + 1);
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.A:
				if (checkLetter != -1){ rotorSelection[checkLetter] = 0; }
				Console.WriteLine("\n [1] = Rotor I - [5] = Rotor V | [0] Random Wiring");
				
				while (!int.TryParse(Console.ReadLine(), out userinput) || userinput < 0 || userinput > 5)
				{
					Console.WriteLine("Not a vaild rotor.");
				} 

				rotorSelection[altering] = userinput;
				rotors[altering][0] = userinput;
				break;

			case ConsoleKey.B:
				if (checkLetter == -1)
				{ 
					Console.WriteLine("Rotor position have no vaild rotor table.");
					break;
				}
				Console.WriteLine("Please enter new rotor starting position. (A: 1 - Z: 26)");
				while(!Int32.TryParse(Console.ReadLine(), out userinput) || userinput > 26 || userinput < 1)
				{
					Console.WriteLine("Not a vaild starting position.");
				}
				rotors[altering][1] = userinput - 1;
				break;

			case ConsoleKey.C:
				Console.WriteLine("NOTICE: \n Normal rotor table have preset rotor notch position, altering it will significantly impact encoding result.\n\n Continue?(Y)");
				if (Console.ReadKey(true).Key != ConsoleKey.Y){break;}
				Console.WriteLine("Continuing...");
				if (checkLetter == -1)
				{ 
					Console.WriteLine("Rotor position have no vaild rotor table.");
					break;
				}
				Console.WriteLine("Please enter new Rotor notch position. (A: 1 - Z: 26)");
				while(!Int32.TryParse(Console.ReadLine(), out userinput) || userinput > 25 || userinput < 0)
				{
					Console.WriteLine("Not a vaild starting position");
				}
				rotors[altering][2] = userinput - 1;
				break;

			default:
				breaker = false;
				break;
		}
	} while (breaker);
}