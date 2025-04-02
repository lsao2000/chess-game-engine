using System;


namespace MyFirstProject
{
    class Program
    {
       static bool win = true;
    // change main to Main to run this class and dont forget to change Main to main in ChessGame.cs
        static void main(string[] args)
        {

            Console.WriteLine("--------------- Tic Tac Toe -----------");
            int[,] ls =
            {
                { 0, 0, 0 },
                { 0, 0, 0 },
                { 0, 0, 0 }
            };
            int[] gamePad = {0, 0, 0, 0, 0, 0, 0, 0, 0};
            int[,] probability =
            {
                 // =
                {0, 1, 2},
                {3, 4, 5},
                {6, 7, 8},
                 //  ||
                {0, 3, 6},
                {1, 4, 7},
                {2, 5, 8},
                 // X
                {0, 4, 8},
                {2, 4, 6},
            };
            Random rand = new Random();
            while (win)
            {
                Console.WriteLine("############    Computer Turn    ##############");
                int computerRow = rand.Next(3) + 1;
                int computerColumn = rand.Next(3) + 1;
                chagingOrders(gamePad, computerRow, computerColumn, 2, ls);
                displayView(ls);
                checkWinner(probability, gamePad, " Computer win", 2);
                if (!win)
                {
                    break;
                }
                Console.WriteLine("############    Your Turn    ##############");
                Console.Write("Enter row number between 1 and 3: ");
                int row = int.Parse(Console.ReadLine());
                Console.Write("Enter column number between 1 and 3: ");
                int column = int.Parse(Console.ReadLine());
                chagingOrders(gamePad, row, column, 1, ls);
                displayView(ls);
                checkWinner(probability, gamePad, "You win", 1);
            }
        }

        public static void chagingOrders(int[] gamePad, int row, int column, int orderNumber, int[,] ls)
        {
            if (ls[row-1, column - 1] == 0)
            {
            int countColumn = 1;
            int countRow = 1;
            for(int i = 0; i < gamePad.Length; i++) {
                if (countRow == row)
                {
                    if(countColumn == column)
                    {
                        if (gamePad[i] == 0)
                        {
                            gamePad[i] = orderNumber;
                        }
                    }
                }
                if(countColumn == 3)
                {
                    countColumn = 0;
                    countRow++;
                }
                countColumn++;
            }
            ls[row - 1, column - 1] = orderNumber;
            }
            else
            {
                if (orderNumber == 1)
                {
                    Console.WriteLine("Thr row is already ordered");
                    Console.Write("Enter row number between 1 and 3: ");
                    int nRow = int.Parse(Console.ReadLine());
                    Console.Write("Enter column number between 1 and 3: ");
                    int nColumn = int.Parse(Console.ReadLine());
                    chagingOrders(gamePad, nRow, nColumn, 1, ls);
                }
                else
                {

                    Random rand = new Random();
                    while (true)
                    {
                        int computerRow = rand.Next(3) + 1;
                        int computerColumn = rand.Next(3) + 1;
                        if (ls[computerRow - 1, computerColumn - 1] == 0)
                        {
                            chagingOrders(gamePad, computerRow, computerColumn, 2, ls);
                            break;
                        }
                    }
                }
            }
        }
        public static void displayView(int[,] ls)
        {
            for(int i = 0; i < ls.GetLength(0) ; i++)
            {
                for(int j = 0; j < ls.GetLength(1)  ; j++)
                {
                    switch (j)
                    {
                        case 2:
                            consoleView(i, j, ls);
                            Console.Write("|");
                            break;
                        default:
                            consoleView(i, j, ls);     
                            break;
                    }
                }
                Console.WriteLine();
            }
        }
        public static bool checkWinner(int[,] probability, int[] gamePad, String winMessage, int winnerNumber) {
            for(int i= 0; i < probability.GetLength(0); i++)
            {
                if (gamePad[probability[i, 0]] == winnerNumber && gamePad[probability[i, 1]] == winnerNumber && gamePad[probability[i, 2]] == winnerNumber) {
                    Console.WriteLine(winMessage);
                    win = false;
                    return win;
                }else if (checkIfItsDraw(gamePad))
                {
                    Console.WriteLine("############ ITS A DRAW ###########");
                    win = false;
                    return win;
                }
            }
            return win;
        }
        public static bool checkIfItsDraw(int[] ls)
        {
            for (int i = 0; i <  ls.Length; i++)
            {
                if (ls[i] == 0)
                {
                    return false;
                }
            }
            return true;
        }

       public static void consoleView(int i, int j, int[,]ls)
        {
            switch (ls[i,j]) {
                case 1:
                    Console.Write("|  X  ");
                    break;
                case 2:
                    Console.Write("|  O  ");
                    break;
                default:
                    Console.Write("|  _  ");
                    break; 
            }
        }
    }

}
