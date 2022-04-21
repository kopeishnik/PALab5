namespace Lab5
{
    internal static class Processing
    {
        public static (int, int) ReturnPlaceByButtonName(string name)
        {
            int key1 = -1;
            if (name[6] == '1')
            {
                key1 = 0;
            }
            else if (name[6] == '2')
            {
                key1 = 1;
            }
            else if (name[6] == '3')
            {
                key1 = 2;
            }
            else if (name[6] == '4')
            {
                key1 = 3;
            }
            else if (name[6] == '5')
            {
                key1 = 4;
            }
            else if (name[6] == '6')
            {
                key1 = 5;
            }
            else if (name[6] == '7')
            {
                key1 = 6;
            }
            else if (name[6] == '8')
            {
                key1 = 7;
            }

            int key2 = -1;
            if (name[7] == 'A')
            {
                key2 = 0;
            } 
            else if (name[7] == 'B')
            {
                key2 = 1;
            }
            else if (name[7] == 'C')
            {
                key2 = 2;
            }
            else if (name[7] == 'D')
            {
                key2 = 3;
            }
            else if (name[7] == 'E')
            {
                key2 = 4;
            }
            else if (name[7] == 'F')
            {
                key2 = 5;
            }
            else if (name[7] == 'G')
            {
                key2 = 6;
            }
            else if (name[7] == 'H')
            {
                key2 = 7;
            }
            return (key1, key2);
        }
        public static bool[,] CreateCopyOfMatrix(bool[,] matrix)
        {
            bool[,] result = new bool[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    result[i, j] = matrix[i, j];
                }
            }
            return result;
        }
        public static List<(int, int)> GetAvailableMoves(bool[,] board)
        {
            List<(int, int)> result = new();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == true) result.Add((i, j));
                }
            }
            return result;
        }
        public static (int, int) GetRandomOne(List<(int, int)> board)
        {
            int x = 0;
            Random random = new();
            x = random.Next(0, board.Count);
            return board[x];
        }
        public static bool IsTerminalState(bool[,] board)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j]) return false;
                }
            }
            return true;
        }
        public static bool CheckForEnd(Board board)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board.BoardState[i, j]) return false;
                }
            }
            return true;
        }
    }
}
