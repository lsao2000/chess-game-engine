using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Threading;

namespace MyFirstProject
{
	class ChessGame
	{
		public static String[,] chessPad =
		 {
			{ "B#", "B@", "B!", "B*", "B$", "B!", "B@", "B#"}, // A1 - A8
			{ "B&", "B&", "B&", "B&", "B&", "B&", "B&", "B&"}, // B1 - B8
			{ "__", "__", "__", "__", "__", "__", "__", "__"}, // C1 - C8
			{ "__", "__", "__", "__", "__", "__", "__", "__"}, // D1 - D8
			{ "__", "__", "__", "__", "__", "__", "__", "__"}, // E1 - E8
			{ "__", "__", "__", "__", "__", "__", "__", "__"}, // F1 - F8
			{ "W&", "W&", "W&", "W&", "W&", "W&", "W&", "W&"}, // G1 - G8
			{ "W#", "W@", "W!", "W$", "W*", "W!", "W@", "W#"}  // H1 - H8
		};
		
		public static Dictionary<String, int[]> computerItemsIndexes = new Dictionary<string, int[]> { };
		public static Dictionary<String, Dictionary<String, int>> bestMovementComputer = new Dictionary<String, Dictionary<String, int>>()  ;
		public static int[,] chessPadNumbers =
		 {
			{5, 3, 3, 9,84, 3, 3, 5},      // A1 - A8
			{1, 1, 1, 1, 1, 1, 1, 1},      // B1 - B8
			{0, 0, 0, 0, 0, 0, 0, 0},      // C1 - C8
			{0, 0, 0, 0, 0, 0, 0, 0},      // D1 - D8
			{0, 0, 0, 0, 0, 0, 0, 0},      // E1 - E8
			{0, 0, 0, 0, 0, 0, 0, 0},      // F1 - F8
			{1, 1, 1, 1, 1, 1, 1, 1},      // G1 - G8
			{5, 3, 3,84, 9, 3, 3, 5},      // H1 - H8
		 };
		// ------------ ONE MOVEMENT DIMENSION
		// king X, Y movement posibility
		public static int[] xKingPosibility = { -1, 1, 0, 0, -1, 1, 1, -1 };
		public static int[] yKingPosibility = { 0, 0, 1, -1, 1, 1, -1, -1 };

		// Knight X,Y movement posibility
		public static int[] xKnightPosibility = { 2, 2, -2, -2, -1, -1, 1, 1 };
		public static int[] yKnightPosobility = { 1, -1, -1, 1, -2, 2, -2, 2 };

		// ------------ TWO MOVEMENT DIMENSION
		// Quen X,Y movement posibility
		public static int[,] xQuenPosibilety = {
				{1, 2, 3, 4, 5, 6, 7},
				{-1, -2, -3, -4, -5, -6, -7},
				{0, 0, 0, 0, 0, 0, 0},
				{0, 0, 0, 0, 0, 0, 0},
				{-1, -2, -3, -4, -5, -6, -7 },
				{ -1, -2, -3, -4, -5, -6, -7},
				{ 1, 2, 3, 4, 5, 6, 7},
				{ 1, 2, 3, 4, 5, 6, 7},
			};
		public static String[] quenDirection = { "BOTTOM", "TOP", "RIGHT", "LEFT", "TOPRIGHT", "TOPLEFT", "BOTTOMLEFT", "BOTTOMRIGHT" };
		public static int[,] yQuenPosibility = {
				{0, 0, 0, 0, 0, 0, 0},
				{0, 0, 0, 0, 0, 0, 0},
				{1, 2, 3, 4, 5, 6, 7},
				{-1, -2, -3, -4, -5, -6, -7},
				{1, 2, 3, 4, 5, 6, 7 },
				{ -1, -2, -3, -4, -5, -6, -7},
				{ -1, -2, -3, -4, -5, -6, -7},
				{ 1, 2, 3, 4, 5, 6, 7},
			};

		// Rock X,Y movement posibility
		   public static int[,] xRockPosibility = {
				{1, 2, 3, 4, 5, 6, 7},
				{-1, -2, -3, -4, -5, -6, -7},
				{0, 0, 0, 0, 0, 0, 0},
				{0, 0, 0, 0, 0, 0, 0},
			};
		   public static int[,] yRockPosibility = {
				{0, 0, 0, 0, 0, 0, 0},
				{0, 0, 0, 0, 0, 0, 0},
				{1, 2, 3, 4, 5, 6, 7},
				{-1, -2, -3, -4, -5, -6, -7},
			};
		 public static String[] rockDirection = { "BOTTOM", "TOP", "RIGHT", "LEFT" };

		// Bishop X,Y movement posibility
		public static String[] bishopDirection = { "TOPRIGHT", "TOPLEFT", "BOTTOMLEFT", "BOTTOMRIGHT" };
		public static int[,] xBishopPosibilite = {
				{-1, -2, -3, -4, -5, -6, -7 },
				{ -1, -2, -3, -4, -5, -6, -7},
				{ 1, 2, 3, 4, 5, 6, 7},
				{ 1, 2, 3, 4, 5, 6, 7},
			};
		public static int[,] yBishopPosibilite = {
				{1, 2, 3, 4, 5, 6, 7 },
				{ -1, -2, -3, -4, -5, -6, -7},
				{ -1, -2, -3, -4, -5, -6, -7},
				{ 1, 2, 3, 4, 5, 6, 7},
			};

		public static String[,] chessPadOrders =
		{
			{"A1", "A2", "A3","A4", "A5", "A6", "A7", "A8"},
			{"B1", "B2", "B3","B4", "B5", "B6", "B7", "B8"},
			{"C1", "C2", "C3","C4", "C5", "C6", "C7", "C8"},
			{"D1", "D2", "D3","D4", "D5", "D6", "D7", "D8"},
			{"E1", "E2", "E3","E4", "E5", "E6", "E7", "E8"},
			{"F1", "F2", "F3","F4", "F5", "F6", "F7", "F8"},
			{"G1", "G2", "G3","G4", "G5", "G6", "G7", "G8"},
			{"H1", "H2", "H3","H4", "H5", "H6", "H7", "H8"},
		};
		public static String[] placeOrder = {"A", "B", "C", "D", "E", "F", "G", "H"};
		public static bool gameOver = false;
		public static String msg;
		public static void Main(String[] args)
		{

			while (true)
            {
                Console.SetCursorPosition(0, 0);
                displayView();
                Console.Write("Enter which one you want to move: ");
                String item = Console.ReadLine();
                changingPlace(item);
                Console.WriteLine();
                if (gameOver)
                {
					break;
                }
                calculateComputerMovement();
                bestMovementComputer.Clear();
                computerItemsIndexes.Clear();
                if (gameOver)
                {
					break;
                }

            }
			clearLinesAtBottom();
            Console.WriteLine(msg);
        }
        private static void moveValidationOfTheSlave(List<String> chances, int[] currentIndex ){
			String choices = chessPadOrders[currentIndex[0] - 1, currentIndex[1]];
			if (chessPad[currentIndex[0] - 1, currentIndex[1]] == "__")
			{
				chances.Add(choices);
				if (currentIndex[0] == 6 && chessPad[currentIndex[0] - 2, currentIndex[1]].StartsWith("_"))
				{
					chances.Add( chessPadOrders[currentIndex[0] - 2, currentIndex[1]]);
				}
			}
			switch (currentIndex[1])
			{
				case 0:
					if (chessPad[currentIndex[0] - 1, currentIndex[1] + 1].StartsWith("B"))
					{
						chances.Add(chessPadOrders[currentIndex[0] - 1, currentIndex[1] + 1]);
					}
					break;
				case 7:
					if (chessPad[currentIndex[0] - 1, currentIndex[1] - 1].StartsWith("B"))
					{
						chances.Add(chessPadOrders[currentIndex[0] - 1, currentIndex[1] - 1]);
					}
					break;
				default:
					if (chessPad[currentIndex[0] - 1, currentIndex[1] + 1].StartsWith("B"))
					{
						chances.Add(chessPadOrders[currentIndex[0] - 1, currentIndex[1] + 1]);
					}
					if (chessPad[currentIndex[0] - 1, currentIndex[1] - 1].StartsWith("B"))
					{
						chances.Add(chessPadOrders[currentIndex[0] - 1, currentIndex[1] - 1]);
					}
					break;
			}

			Console.Write("Chosse which One you want to move to it: ");
			for (int i = 0; i < chances.Count; i++)
			{
				Console.Write("{0} ", chances[i]);
			}
			Console.WriteLine();
		}
		public static String[] getNextMoveChances(String item)
		{
			int[] currentIndex = indexOf(item);
			List<String> chances = new List<String> { };
			switch(chessPad[currentIndex[0], currentIndex[1]])
			{
				case "W&":
					moveValidationOfTheSlave(chances, currentIndex);
					break;
				case "W!":
					//moveValidationOfTheBishop(chances, currentIndex);
					twoDimensionMoveValidation(chances, currentIndex, xBishopPosibilite, yBishopPosibilite, bishopDirection);
					break;
				case "W#":
					//moveValidationOfTheRock(chances, currentIndex);
					twoDimensionMoveValidation(chances, currentIndex, xRockPosibility, yRockPosibility, rockDirection);
					break;
				case "W$":
					// moveValidationOfTheKing(chances, currentIndex);
					oneDimensionMoveValidation(chances, currentIndex, xKingPosibility, yKingPosibility);
					break;
				case "W@":
				   // moveValidationOfTheKnight(chances, currentIndex);
					oneDimensionMoveValidation(chances, currentIndex, xKnightPosibility, yKnightPosobility);
					break;
				case "W*":
					twoDimensionMoveValidation(chances, currentIndex, xQuenPosibilety, yQuenPosibility, quenDirection);
				   // moveValidationOfTheQueen(chances, currentIndex);
					break;
			}
			return chances.ToArray();
		}
		private static void getMoveScoreValue()
		{
			List<int> allMoves = new List<int> { };
			List<String> allItems = new List<string> { };
			List<String> nextMoves = new List<string> { };
			foreach (KeyValuePair<String, Dictionary<String,int>> item in bestMovementComputer)
			{
                if (item.Value.Count > 0)
                {
                    foreach (KeyValuePair<String, int> item1 in item.Value)
					{
						allMoves.Add(item1.Value);
						allItems.Add(item.Key);
						nextMoves.Add(item1.Key);
					}
                }
            }
			if (checkIfAllItemsAreSame(allMoves))
			{
				Random rand = new Random();
				int randomNumber = rand.Next(allMoves.Count);
				String itemChanging = allItems[randomNumber];
				String placeToBeChanged = nextMoves[randomNumber];
				changeComputerItem(itemChanging, placeToBeChanged);
			}
			else
			{
				int bestItem = allMoves.Max();
				int indexMove = allMoves.IndexOf(bestItem);
				String itemChanging = allItems[indexMove];
				String placeToBeChanged = nextMoves[indexMove];
				changeComputerItem(itemChanging, placeToBeChanged);
			}
		}
		private static bool checkIfAllItemsAreSame(List<int> allMoves)
		{
            int firstElement = allMoves[0];
			for(int i = 0; i < allMoves.Count; i++)
			{
				if (allMoves[i] != firstElement)
				{
					return false;
				}
			}
			return true;
		}
		private static void changeComputerItem(String item, String nexPlace)
		{
			Thread.Sleep(250);
			int[] currentIndex = computerItemsIndexes[item];
			int[] nexIndex = indexOf(nexPlace);
			// change on chessPad
			if (chessPad[nexIndex[0], nexIndex[1]].EndsWith("$"))
			{
				Console.WriteLine("game over");
			}
			String currentPlace = chessPad[currentIndex[0], currentIndex[1]];
            chessPad[currentIndex[0], currentIndex[1]] = "__";
			chessPad[nexIndex[0], nexIndex[1]] = currentPlace;
            if (nexIndex[0] == 7 && currentPlace.EndsWith("&"))
            {
                chessPad[nexIndex[0], nexIndex[1]] = "B*";
            }
			// change on chessPadNumbers
			int currentPlaceNumber = chessPadNumbers[currentIndex[0], currentIndex[1]];
			int nextPlaceNumber = chessPadNumbers[nexIndex[0], nexIndex[1]];
            if (nextPlaceNumber > 0)
            {
				chessPadNumbers[currentIndex[0], currentIndex[1]] = 0;
				chessPadNumbers[nexIndex[0], nexIndex[1]] = currentPlaceNumber;
			}
			else
			{
				//	chessPad[currentIndex[0], currentIndex[1]] = chessPad[nexIndex[0], nexIndex[1]];
				chessPadNumbers[currentIndex[0], currentIndex[1]] = nextPlaceNumber;
				chessPadNumbers[nexIndex[0], nexIndex[1]] = currentPlaceNumber;
              //  chessPad[nexIndex[0], nexIndex[1]] = currentPlace;
            }
            //chessPad[currentIndex[0], currentIndex[1]];	




        }

		private static void calculateComputerMovement()
		{
			for (int i = 0; i < chessPad.GetLength(0); i++)
			{
                Random rand = new Random();
                for (global::System.Int32 j = 0; j < chessPad.GetLength(1); j++)
				{
                    String item = chessPad[i, j];
					int[] currentIndex = { i, j};
					try
					{
						if (item.StartsWith("B"))
						{
                            String[] itemSplited = { item.Substring(0, 1), item.Substring(1, 1) };
                            String itemInDictionary = itemSplited[0] + (currentIndex[1] + 1) + itemSplited[1] + (currentIndex[0] + 1);
                            computerItemsIndexes.Add(itemInDictionary, currentIndex);
							if (item.EndsWith("#"))
							{
								twoDimensionComputerMovement(xRockPosibility, yRockPosibility, currentIndex, item);
							}
							else if (item.EndsWith("@"))
							{
								oneDimensionComputerMovement(xKnightPosibility, yKnightPosobility, currentIndex, item);
							}
							else if (item.EndsWith("!"))
							{
								twoDimensionComputerMovement(xBishopPosibilite, yBishopPosibilite, currentIndex, item);
							}
							else if (item.EndsWith("&"))
							{
								slaveComputerMovement(currentIndex, item); 
							}
							else if (item.EndsWith("*"))
							{
								twoDimensionComputerMovement(xQuenPosibilety, yQuenPosibility, currentIndex, item);
							}
							else
							{
								oneDimensionComputerMovement(xKingPosibility, yKingPosibility, currentIndex, item);
							}

                            //String[] itemSplited = { item.Substring(0, 1), item.Substring(1, 1) };
                            // String itemInDictionary = itemSplited[0] + (currentIndex[1] + 1) + itemSplited[1] + (currentIndex[0] + 1);
                            if (bestMovementComputer[itemInDictionary].Count > 0)
                            {
                                List<int> allValues = bestMovementComputer[itemInDictionary].Values.ToList();
                                List<String> allKeys = bestMovementComputer[itemInDictionary].Keys.ToList();
                                if (checkIfAllItemsAreSame(allValues))
                                {
                                    int someItem = rand.Next(allValues.Count);
                                    Dictionary<String, int> placing = new Dictionary<string, int>() { { allKeys[someItem], allValues[someItem] } };
                                    bestMovementComputer[itemInDictionary] = placing;

                                }
                                else
                                {
                                    var bestPlace = bestMovementComputer[itemInDictionary].FirstOrDefault(c => c.Value == bestMovementComputer[itemInDictionary].Values.Max());
                                    Dictionary<String, int> placing = new Dictionary<string, int>() { { bestPlace.Key, bestPlace.Value } };
                                    bestMovementComputer[itemInDictionary] = placing;
                                }
                            }
                        }
                    }
                    catch (IndexOutOfRangeException) { }
                }

            }
            getMoveScoreValue();
        }
		private static void slaveComputerMovement(int[] currentIndex, String item)
		{
			String[] itemSplited = { item.Substring(0, 1), item.Substring(1, 1)};
			String itemInDictionary = itemSplited[0] + (currentIndex[1] + 1) +itemSplited[1] + (currentIndex[0] + 1);
			bestMovementComputer.Add(itemInDictionary, new Dictionary<string, int>());
			if (chessPad[currentIndex[0] + 1, currentIndex[1]] == "__")
			{
				String minNewPlace = chessPadOrders[currentIndex[0] + 1, currentIndex[1]];
				int firstScore = chessPadNumbers[currentIndex[0] + 1, currentIndex[1]];
				bestMovementComputer[itemInDictionary].Add(minNewPlace, firstScore);
				if (currentIndex[0] == 1 && chessPad[currentIndex[0] + 2, currentIndex[1]].StartsWith("_"))
				{
					String newPlace = chessPadOrders[currentIndex[0] + 2, currentIndex[1]];
					int score = chessPadNumbers[currentIndex[0] + 2, currentIndex[1]];
					bestMovementComputer[itemInDictionary].Add(newPlace, score);
				}
			}
			try
			{
				if (chessPad[currentIndex[0] + 1, currentIndex[1] + 1].StartsWith("W"))
				{
					String newPlace = chessPadOrders[currentIndex[0] + 1, currentIndex[1] + 1];
					int score = chessPadNumbers[currentIndex[0] + 1, currentIndex[1] + 1];
					bestMovementComputer[itemInDictionary].Add(newPlace, score);
				}
			}
			catch (IndexOutOfRangeException e) { }
			try
			{
				if (chessPad[currentIndex[0] + 1, currentIndex[1] - 1].StartsWith("W"))
				{
					String newPlace = chessPadOrders[currentIndex[0] + 1, currentIndex[1] - 1];
					int score = chessPadNumbers[currentIndex[0] + 1, currentIndex[1] - 1];
					bestMovementComputer[itemInDictionary].Add(newPlace, score);
				}
			}catch(IndexOutOfRangeException) { }
		}

		private static void oneDimensionComputerMovement(int[] xPosibility, int[] yPosibility, int[] currentIndex, String item)
		{
			String[] itemSplited = { item.Substring(0, 1), item.Substring(1, 1) };
			String itemInDictionary =itemSplited[0] + (currentIndex[1] + 1) +itemSplited[1] + (currentIndex[0] + 1);
			bestMovementComputer.Add(itemInDictionary, new Dictionary<string, int>());
			for (int i = 0; i < xPosibility.Length; i++)
			{
				int newXAxis = currentIndex[0] + xPosibility[i];
				int newYAxis = currentIndex[1] + yPosibility[i];
				try
				{
					if (chessPad[newXAxis, newYAxis].StartsWith("W") || chessPad[newXAxis, newYAxis].StartsWith("_"))
					{
						String newPlace = chessPadOrders[newXAxis, newYAxis];
						int score = chessPadNumbers[newXAxis, newYAxis];
						bestMovementComputer[itemInDictionary].Add(newPlace, score);
					}
				}
				catch (IndexOutOfRangeException e) { }
			}
		}
		private static void twoDimensionComputerMovement(int[,] xPosibility, int[,] yPosibility, int[] currentIndex, String item)
		{
			String[] itemSplited = { item.Substring(0, 1), item.Substring(1, 1) };
			String itemInDictionary = itemSplited[0] + (currentIndex[1] + 1) + itemSplited[1] + (currentIndex[0] + 1);
			bestMovementComputer.Add(itemInDictionary, new Dictionary<string, int>());
			for (int i = 0; i < xPosibility.GetLength(0); i++)
			{
				for (global::System.Int32 j = 0; j < xPosibility.GetLength(1); j++)
				{
					try
					{
						int newXAxis = currentIndex[0] + xPosibility[i, j];
						int newYAxis = currentIndex[1] + yPosibility[i, j];
						if (chessPad[newXAxis, newYAxis].StartsWith("W") || chessPad[newXAxis, newYAxis].StartsWith("_"))
						{
							String newPlace = chessPadOrders[newXAxis, newYAxis];
							int score = chessPadNumbers[newXAxis, newYAxis];
							bestMovementComputer[itemInDictionary].Add(newPlace, score);
							if (chessPad[newXAxis, newYAxis].StartsWith("W"))
							{
								break;
							}
						}else 
						{
								break;
						}
					} catch(IndexOutOfRangeException e) { }
				}
			}
			//            List<String> lis = computerItemsMovingPosibility[itemInDictionary];
			//            if (lis.Count > 0)
			//            {
			//                for (int i = 0; i < lis.Count; i++)
			//                {
			//                    Console.WriteLine(lis[i]);
			//                }
			//
			//            }
		}

		private static void twoDimensionMoveValidation(List<String> chances, int[] currentIndex, int[,] xPosibility, int[,] yPosibility, String[] directions)
		{
			List<List<String>> chancesDirection = new List<List<string>> { }; 
			for (int i = 0; i < xPosibility.GetLength(0); i++)
			{
				chancesDirection.Add(new List<string> { });
				for (global::System.Int32 j = 0; j < xPosibility.GetLength(1); j++)
				{
					try
					{
						int newXAxis = currentIndex[0] + xPosibility[i, j];
						int newYAxis = currentIndex[1] + yPosibility[i, j];
						
						if (chessPad[newXAxis, newYAxis].StartsWith("B") || chessPad[newXAxis, newYAxis].StartsWith("_"))
						{
							String newPlace = chessPadOrders[newXAxis, newYAxis];
							chancesDirection[i].Add(newPlace);
							chances.Add(newPlace);
							if (chessPad[newXAxis, newYAxis].StartsWith("B"))
							{
								break;
							}
						}else 
						{
								break;
						}
					} catch(IndexOutOfRangeException e) { }
				}
			}
			for (int i = 0; i < chancesDirection.Count; i++)
			{
				int listCount = chancesDirection[i].Count;
				if (listCount > 0)
				{
					Console.Write("{0}: ", directions[i]);
					for (global::System.Int32 j = 0; j < listCount; j++)
					{
						if (j < listCount - 1)
						{
							Console.Write("{0} - ",chancesDirection[i][j]);
						}
						else
						{
							Console.Write("{0} ",chancesDirection[i][j]);
						}
					}
					Console.Write("|");
				}
			}
			Console.WriteLine();
		}
		private static void oneDimensionMoveValidation(List<String> chances, int[] currentIndex, int[] xPosibility, int[] yPosibility) {
			for (int i = 0; i < xPosibility.Length; i++)
			{
				int newXAxis = currentIndex[0] + xPosibility[i];
				int newYAxis = currentIndex[1] + yPosibility[i];
				try
				{
					if (chessPad[newXAxis, newYAxis].StartsWith("B") || chessPad[newXAxis, newYAxis].StartsWith("_"))
					{
						String newPlace = chessPadOrders[newXAxis, newYAxis];
						chances.Add(newPlace);
					}
				}
				catch (IndexOutOfRangeException e) { }
			}
			Console.Write("Chosse which One you want to move to it: ");
			for (int i = 0; i < chances.Count; i++)
			{
				if (i == chances.Count -1 )
				{
					Console.Write("{0} ", chances[i]); 
				}
				else
				{
					Console.Write("{0} - ", chances[i]); 
				}
			}
			Console.WriteLine();
		}
		static int[] indexOf(String item)
		{
			for (int i = 0; i < chessPad.GetLength(0); i++)
			{
				for (int j = 0; j < chessPad.GetLength(1); j++)
				{
					if (chessPadOrders[i, j] == item)
					{
						int[] indexOfItem = new int[] { i, j};
						return indexOfItem;
					}
				}
			}
			return null;
		}
		static void clearLinesAtBottom()
		{
			int lineNumber = Console.WindowHeight - 5;
			Console.SetCursorPosition(0, lineNumber);
			for (int i = lineNumber - 1; i < Console.WindowHeight; i++)
			{
				Console.SetCursorPosition(0, i);
				Console.Write(new string(' ', Console.WindowWidth));
			}
			// Move the cursor back to the cleared line
			Console.SetCursorPosition(0, lineNumber - 1);
		}
		static void changingPlace( String item)
		{
			String[] moveChances = getNextMoveChances(item);
			if (moveChances.Length == 0)
			{
				Console.Write("There is no available movement for this item Please Press [ENTER]");
				Console.ReadLine();
				clearLinesAtBottom();
			}
			else
			{
				Console.Write("Enter to Where you want to change: ");
				String nextPlace = Console.ReadLine();
				Console.WriteLine();
				if (moveChances.Contains(nextPlace))
				{
					int[] indexes = indexOf(item);
					int[] newIndexes = indexOf(nextPlace);
					if (indexes != null & newIndexes != null)
					{
                        if (chessPad[newIndexes[0], newIndexes[1]].EndsWith("$"))
                        {
							gameOver = true;
							msg = "You win The Game";
                        }
                        String itemMark = chessPad[indexes[0], indexes[1]];
						chessPad[indexes[0], indexes[1]] = "__";
						chessPad[newIndexes[0], newIndexes[1]] = itemMark;
						if (newIndexes[0] == 0 && itemMark == "W&")
						{
							chessPad[newIndexes[0], newIndexes[1]] = "W*";
						}

                        // change on chessPadNumbers
                        int currentPlaceNumber = chessPadNumbers[indexes[0], indexes[1]];
                        int nextPlaceNumber = chessPadNumbers[newIndexes[0], newIndexes[1]];
                        if (nextPlaceNumber > 0)
                        {
							chessPadNumbers[indexes[0], indexes[1]] = 0;
							chessPadNumbers[newIndexes[0], newIndexes[1]] = currentPlaceNumber;
                           // chessPadNumbers[currentIndex[0], currentIndex[1]] = 0;
                           // chessPadNumbers[nexIndex[0], nexIndex[1]] = currentPlaceNumber;
                        }
                        else
                        {
							chessPadNumbers[indexes[0], indexes[1]] = nextPlaceNumber;
							chessPadNumbers[newIndexes[0], newIndexes[1]] = currentPlaceNumber;
                           // chessPadNumbers[currentIndex[0], currentIndex[1]] = nextPlaceNumber;
                          //  chessPadNumbers[nexIndex[0], nexIndex[1]] = currentPlaceNumber;
                        }
                        // these line below for clearing the screen and display the view and after amount of time changing items place 
                        // to be looking like a real game.
                        clearLinesAtBottom();
					}
				}
				else
				{
					clearLinesAtBottom();
					Console.SetCursorPosition(0, 0);
					displayView();
					Console.WriteLine("Not Valid move please try again.");
					Console.Write("Which one you want to move: ");
					String currentPlace = Console.ReadLine();
					changingPlace(currentPlace);

				}
			}
		}
		// handling display the game on the console.
		static void displayView()
		{
			Console.WriteLine("/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\ Chess Game /\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\");
			Console.WriteLine();
			Console.Write("                            ");
			Console.WriteLine("   1     2     3     4     5     6     7     8 ");
			for (int i = 0; i < chessPad.GetLength(0) ; i++)
			{
				Console.Write("                            ");
				Console.WriteLine(" ----------------------------------------------");
				Console.Write("                            ");
				for(int j = 0; j < chessPad.GetLength(1); j++)
				{
					Console.Write("| {0} |", chessPad[i,j]);
				}
				Console.Write("       //  {0}1 - {0}8", placeOrder[i]);
				Console.WriteLine();
				Console.Write("                            ");
				Console.WriteLine(" ----------------------------------------------");
			}
		}
	}
}
