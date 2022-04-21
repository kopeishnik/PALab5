namespace Lab5
{
    internal class Board
    {
        public bool[,] BoardState { get; }
        public Board()
        {
            BoardState = new bool[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    BoardState[i, j] = true;
                }
            }
        }
        public void Move((int, int) point)
        {
            BoardState[point.Item1, point.Item2] = false;
            int x = point.Item1, y = point.Item2;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Math.Abs(x - i) <= 1 && Math.Abs(y - j) <= 1) BoardState[i, j] = false;
                }
            }
        }
    }
}
