using System;
using System.Threading;

EngimaMachine engimaMachine;
List<int[]> refRandomRotor = new List<int[]>
{
    Rotor.PopulateTable(),
    Rotor.PopulateTable(),
    Rotor.PopulateTable(),
    Reflector.PopulateTable()
};
int R1 = 0;
int R2 = 1;
int R3 = 2;
int R1Pos = 0, R2Pos = 0, R3Pos = 0, R1Notch = 0, R2Notch = 0, R3Notch = 0;
int reflectorSelection = 1;

string[] rotorLabels = new string[] {"Rotor I", "Rotor II", "Rotor III", "Rotor IV", "Rotor V", "Random Wiring", "Random Wiring", "RandomWiring", "No Label"};
string[] reflectorLabels = new string[] {"No Label", "UKW-A", "UKW-B", "UKW-C", "Random UKW"};
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
			reflectorSelection = 0;
			bool breaker = true;
			bool parser = false;
			int checkLetter;
			int useRotorSelection;
			//Rotor selection, 1 = slot A, 2 = slot B, 3 = slot C [0] = Rotor I - [4] = Rotor V [5] Random A [6] Random B [7] Random C
			//[[2],[1],[0],[0],[0],[0],[0],[3]] = slot A [Rotor II] | slot B [Rotor I] | slot C [Random C]

			do
			{
				Console.Clear();
				Console.WriteLine("(a) Rotor slot A | (b) Rotor slot B | (c) Rotor slot C | (d) Reflector | (any key) finish and exit rotor customization");

				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.A:
						bool alterBreaker = true;
						do
						{
							Console.Clear();
							Console.WriteLine($"Altering Rotor slot A, {rotorLabels[R1]} Ring set to {R1Pos + 1}, notch set to {R1Notch}.");
							Console.WriteLine("(a) Alter rotor set, (b) Alter Starting Position, (c) Alter Table Position. (any key) to exit");

							checkLetter = Array.IndexOf(rotorSelection, 1);
							switch (Console.ReadKey(true).Key)
							{
								case ConsoleKey.A:
									if (checkLetter != -1){ rotorSelection[checkLetter] = 0; }
									Console.WriteLine("\n [0] = Rotor I - [4] = Rotor V | [5] Random A");
									parser = int.TryParse(Console.ReadLine(), out useRotorSelection);
									if (parser && useRotorSelection >= 0 && useRotorSelection < 6)
									{
										rotorSelection[useRotorSelection] = 1;
									}
									break;

								case ConsoleKey.B:
									if (checkLetter == -1)
									{ 
										Console.WriteLine("Rotor position have no vaild rotor table.");
										break;
									}
									Console.WriteLine("Please enter new rotor starting position. (A: 1 - Z: 26)");
									int pos;
									while(!Int32.TryParse(Console.ReadLine(), out pos) || pos > 26 || pos < 1)
									{
										Console.WriteLine("Not a vaild starting position");
									}
									R1Pos = pos - 1;
									break;

								// case ConsoleKey.C:
								// 	if (checkLetter == -1)
								// 	{ 
								// 		Console.WriteLine("Rotor position have no vaild rotor table.");
								// 		break;
								// 	}
								// 	Console.WriteLine("Please enter new Rotor notch position. (A: 1 - Z: 26)");
								// 	while(!Int32.TryParse(Console.ReadLine(), out pos) || pos > 25 || pos < 0)
								// 	{
								// 		Console.WriteLine("Not a vaild starting position");
								// 	}
								// 	R1Notch = pos - 1;
								// 	break;

								default:
									alterBreaker = false;
									break;
							}
						} while (alterBreaker);
						break;

					case ConsoleKey.B:
						alterBreaker = true;
						do
						{
							Console.Clear();
							Console.WriteLine($"Altering Rotor slot B, {rotorLabels[R2]} Ring set to {R2Pos + 1}, notch set to {R2Notch}.");
							Console.WriteLine("(a) Alter rotor set, (b) Alter Starting Position, (c) Alter Table Position. (any key) to exit");

							checkLetter = Array.IndexOf(rotorSelection, 2);
							switch (Console.ReadKey(true).Key)
							{
								case ConsoleKey.A:
									if (checkLetter != -1){ rotorSelection[checkLetter] = 0; }
									Console.WriteLine("\n [0] = Rotor I - [4] = Rotor V | [5] Random A");
									parser = int.TryParse(Console.ReadLine(), out useRotorSelection);
									if (parser && useRotorSelection >= 0 && useRotorSelection < 6)
									{
										rotorSelection[useRotorSelection] = 2;
									}
									break;

								case ConsoleKey.B:
									if (checkLetter == -1)
									{ 
										Console.WriteLine("Rotor position have no vaild rotor table.");
										break;
									}
									Console.WriteLine("Please enter new rotor starting position. (A: 1 - Z: 26)");
									int pos;
									while(!Int32.TryParse(Console.ReadLine(), out pos) || pos > 26 || pos < 1)
									{
										Console.WriteLine("Not a vaild starting position");
									}
									R2Pos = pos - 1;
									break;

								// case ConsoleKey.C:
								// 	if (checkLetter == -1)
								// 	{ 
								// 		Console.WriteLine("Rotor position have no vaild rotor table.");
								// 		break;
								// 	}
								// 	Console.WriteLine("Please enter new Rotor notch position. (A: 1 - Z: 26)");
								// 	while(!Int32.TryParse(Console.ReadLine(), out pos) || pos > 25 || pos < 0)
								// 	{
								// 		Console.WriteLine("Not a vaild starting position");
								// 	}
								// 	R1Notch = pos - 1;
								// 	break;

								default:
									alterBreaker = false;
									break;
							}
						} while (alterBreaker);
						break;

					case ConsoleKey.C:
						alterBreaker = true;
						do
						{
							Console.Clear();
							Console.WriteLine($"Altering Rotor slot C, {rotorLabels[R3]} Ring set to {R3Pos + 1}, notch set to {R3Notch}.");
							Console.WriteLine("(a) Alter rotor set, (b) Alter Starting Position, (c) Alter Table Position. (any key) to exit");

							checkLetter = Array.IndexOf(rotorSelection, 3);
							switch (Console.ReadKey(true).Key)
							{
								case ConsoleKey.A:
									if (checkLetter != -1){ rotorSelection[checkLetter] = 0; }
									Console.WriteLine("\n [0] = Rotor I - [4] = Rotor V | [5] Random A");
									parser = int.TryParse(Console.ReadLine(), out useRotorSelection);
									if (parser && useRotorSelection >= 0 && useRotorSelection < 6)
									{
										rotorSelection[useRotorSelection] = 3;
									}
									break;

								case ConsoleKey.B:
									if (checkLetter == -1)
									{ 
										Console.WriteLine("Rotor position have no vaild rotor table.");
										break;
									}
									Console.WriteLine("Please enter new rotor starting position. (A: 1 - Z: 26)");
									int pos;
									while(!Int32.TryParse(Console.ReadLine(), out pos) || pos > 26 || pos < 1)
									{
										Console.WriteLine("Not a vaild starting position");
									}
									R3Pos = pos - 1;
									break;

								// case ConsoleKey.C:
								// 	if (checkLetter == -1)
								// 	{ 
								// 		Console.WriteLine("Rotor position have no vaild rotor table.");
								// 		break;
								// 	}
								// 	Console.WriteLine("Please enter new Rotor notch position. (A: 1 - Z: 26)");
								// 	while(!Int32.TryParse(Console.ReadLine(), out pos) || pos > 25 || pos < 0)
								// 	{
								// 		Console.WriteLine("Not a vaild starting position");
								// 	}
								// 	R1Notch = pos - 1;
								// 	break;

								default:
									alterBreaker = false;
									break;
							}
						} while (alterBreaker);
						break;

					case ConsoleKey.D:
						while(true)
						{
							Console.WriteLine("");
							Console.WriteLine("[0] = UKW-A [1] = UKW-B [2] = UKW-C | [3] Random UKW");
							parser = int.TryParse(Console.ReadLine(), out useRotorSelection);
							if (parser && useRotorSelection >= 0 && useRotorSelection < 4)
							{
								reflectorSelection = useRotorSelection + 1;
								break;
							}
							Console.WriteLine("Invaild Input - Try again.");
						}
						break;

					default:
						R1 = Array.IndexOf(rotorSelection, 1);
						R2 = Array.IndexOf(rotorSelection, 2);
						R3 = Array.IndexOf(rotorSelection, 3);

						if (R1 != -1 && R2 != -1 && R3 != -1 && reflectorSelection != 0)
						{
							Console.WriteLine("Machine rotor set complete");
                            Console.WriteLine($"Slot A: {rotorLabels[R1]} - {R1Pos + 1}| Slot B: {rotorLabels[R2]} - {R2Pos + 1} | Slot C: {rotorLabels[R3]} - {R3Pos + 1} | Reflector: {reflectorLabels[reflectorSelection--]}");
							breaker = false;
							Console.WriteLine("Press Enter to continue...");
                            while (Console.ReadKey(true).Key != ConsoleKey.Enter) {}
							break;
						}

						R1 = (R1 != -1) ? R1 : 8;
						R2 = (R2 != -1) ? R2 : 8;
						R3 = (R3 != -1) ? R3 : 8;

						Console.Clear();
						Console.WriteLine("Machine rotor set misconfigured.");
						Console.WriteLine($"Slot A: {rotorLabels[R1]} - {R1Pos + 1}| Slot B: {rotorLabels[R2]} - {R2Pos + 1} | Slot C: {rotorLabels[R3]} - {R3Pos + 1} | Reflector: {reflectorLabels[reflectorSelection]}");
						Thread.Sleep(4000);
						break;
				}
			}while (breaker);
			break;

		case ConsoleKey.E:
			engimaMachine = new EngimaMachine(R1, R2, R3, reflectorSelection, refRandomRotor);
			engimaMachine.AlterAllRotorPos(R1Pos, R2Pos, R3Pos);
			engimaMachine.AlterAllRotorNotch(R1Notch, R2Notch, R3Notch);
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
			engimaMachine = new EngimaMachine(R1, R2, R3, reflectorSelection, refRandomRotor);
			engimaMachine.AlterAllRotorPos(R1Pos, R2Pos, R3Pos);
			engimaMachine.AlterAllRotorNotch(R1Notch, R2Notch, R3Notch);
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
			engimaMachine = new EngimaMachine(R1, R2, R3, reflectorSelection, refRandomRotor, true);
			engimaMachine.AlterAllRotorPos(R1Pos, R2Pos, R3Pos);
			engimaMachine.AlterAllRotorNotch(R1Notch, R2Notch, R3Notch);
			engimaMachine.RotorSettingExport(rotorLabels[R1], rotorLabels[R2], rotorLabels[R3], reflectorLabels[reflectorSelection]);
			Console.WriteLine("Press Enter to exit...");
			while (Console.ReadKey(true).Key != ConsoleKey.Enter) {}
			break;

		case ConsoleKey.Escape:
			exit = false;
			break;

	}

} while(exit);
