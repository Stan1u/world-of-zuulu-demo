namespace WorldOfZuul;

public static class Ticktack
{
    static bool draw;
        static char[,] NewGame()
        {
            char[,] field = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    field[i, j] = ' ';
                }
            }
            return field;
        }

        static void Move(char[,] field, char player)
        {
            if (player == 'e')
            {
                MoveEasy(field, 'X');
                return;
            }
            if (player == 'E')
            {
                MoveEasy(field, 'O');
                return;
            }

            Console.Write("\nEnter the coordinates: ");
            int y, x;
            while (true)
            {
                string input = Console.ReadLine();
                string[] parts = input.Split(' ');

                if (parts.Length != 2 || !int.TryParse(parts[0], out y) || !int.TryParse(parts[1], out x))
                {
                    Console.WriteLine("You should enter numbers in correct format!");
                    Console.Write("Enter the coordinates: ");
                    continue;
                }

                if (x < 1 || x > 3 || y < 1 || y > 3)
                {
                    Console.WriteLine("Coordinates should be from 1 to 3!");
                    Console.Write("Enter the coordinates: ");
                    continue;
                }

                y -= 1;
                x = x == 1 ? 2 : x == 3 ? 0 : 1;

                if (field[x, y] != ' ')
                {
                    Console.WriteLine("This cell is occupied! Choose another one!");
                    Console.Write("Enter the coordinates: ");
                    continue;
                }

                field[x, y] = player;
                break;
            }
        }

        static void MoveEasy(char[,] field, char player)
        {
            Random random = new Random();
            while (true)
            {
                int y = random.Next(3);
                int x = random.Next(3);

                if (field[x, y] != ' ') continue;

                Console.WriteLine("Making move");
                field[x, y] = player;
                break;
            }
        }

        static void PrintGame(char[,] field)
        {
            Console.WriteLine("   -----------");
            Console.WriteLine("3 | {0} | {1} | {2} |", field[0, 0], field[0, 1], field[0, 2]);
            Console.WriteLine("   -----------");
            Console.WriteLine("2 | {0} | {1} | {2} |", field[1, 0], field[1, 1], field[1, 2]);
            Console.WriteLine("   -----------");
            Console.WriteLine("1 | {0} | {1} | {2} |", field[2, 0], field[2, 1], field[2, 2]);
            Console.WriteLine("   -----------");
            Console.WriteLine("    1   2   3");
        }

        static bool Analyze(char[,] field)
        {
            int xSum = 'X' * 3;
            int oSum = 'O' * 3;
            int howManyX = 0;
            int howManyO = 0;
            bool notFinished = false;
            bool xWins = false;
            bool oWins = false;

            int[] sums = new int[8];
            sums[0] = field[0, 0] + field[0, 1] + field[0, 2];
            sums[1] = field[1, 0] + field[1, 1] + field[1, 2];
            sums[2] = field[2, 0] + field[2, 1] + field[2, 2];
            sums[3] = field[0, 0] + field[1, 0] + field[2, 0];
            sums[4] = field[0, 1] + field[1, 1] + field[2, 1];
            sums[5] = field[0, 2] + field[1, 2] + field[2, 2];
            sums[6] = field[0, 0] + field[1, 1] + field[2, 2];
            sums[7] = field[0, 2] + field[1, 1] + field[2, 0];

            foreach (var cell in field)
            {
                if (cell == 'X') howManyX++;
                if (cell == 'O') howManyO++;
                if (cell == ' ') notFinished = true;
            }

            foreach (var sum in sums)
            {
                if (sum == xSum) xWins = true;
                if (sum == oSum) oWins = true;
            }

            if ((xWins && oWins) || !(howManyO == howManyX || howManyO == howManyX - 1 || howManyO == howManyX + 1))
            {
                Console.WriteLine("Impossible");
                return true;
            }
            if (xWins)
            {
                return true;
            }
            if (oWins)
            {
                return true;
            }
            if (!notFinished)
            {
                Console.WriteLine("\nDraw! Lets have one more game!\n");
                draw = true;
                return true;
            }

            return false;
        }

        public static bool PlayTickTack()
        {
           while(true)
           {
               draw = false;
               char[] players = { 'X', 'E' };
               char[,] field = NewGame();
               PrintGame(field);

               while (true)
               {
                   Move(field, players[0]);
                   PrintGame(field);
                   if (Analyze(field)&&!draw) return true;
                   if (draw) break;

                   Move(field, players[1]);
                   PrintGame(field);
                   if (Analyze(field)&&!draw) return false;
                   if (draw) break;
               }
           }
        }
    }