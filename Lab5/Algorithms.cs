
namespace Lab5
{
    internal static class Algorithms
    {
        public static int MaxDepth = 4;
        public static (int, int) AlphaBetaPruning(Board board, bool isMaximizingPlayer)
        {
            int alpha = int.MinValue, beta = int.MaxValue;
            (int, int) bestMove = (-1, -1);
            List<(int, int)> bestMoves = new();
            if (isMaximizingPlayer)
            {
                int bestVal = int.MinValue;
                List<(int, int)> availableMoves = Processing.GetAvailableMoves(board.BoardState);
                foreach (var move in availableMoves)
                {
                    var boardCopy = Processing.CreateCopyOfMatrix(board.BoardState);
                    Move(boardCopy, move);
                    int val = AlphaBetaPruning(boardCopy, 1, false, alpha, beta);
                    if (val > bestVal)
                    {
                        bestVal = val;
                        bestMove = move;
                        bestMoves.Clear();
                        bestMoves.Add(move);
                    }
                    else if (val == bestVal)
                    {
                        bestMoves.Add(move);
                    }
                    if (bestVal > alpha) alpha = bestVal;
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }
            else
            {
                int bestVal = int.MaxValue;
                List<(int, int)> availableMoves = Processing.GetAvailableMoves(board.BoardState);
                foreach (var move in availableMoves)
                {
                    var boardCopy = Processing.CreateCopyOfMatrix(board.BoardState);
                    Move(boardCopy, move);
                    int val = AlphaBetaPruning(boardCopy, 1, true, alpha, beta);
                    if (val < bestVal)
                    {
                        bestVal = val;
                        bestMove = move;
                        bestMoves.Clear();
                        bestMoves.Add(move);
                    }
                    else if (val == bestVal)
                    {
                        bestMoves.Add(move);
                    }
                    if (bestVal < beta) beta = bestVal;
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }
            bestMove = Processing.GetRandomOne(bestMoves);
            board.Move(bestMove);
            return bestMove;
        }
        public static int AlphaBetaPruning(bool[,] boardMatrix, int depth, bool isMaximizingPlayer, int alpha, int beta)
        {
            if (Processing.IsTerminalState(boardMatrix))
            {
                if (isMaximizingPlayer) return - 20 - depth;
                else return depth + 20;
            }
            else if (depth == MaxDepth)
            {
                if (isMaximizingPlayer) return -depth;
                else return depth;
            }
            else
            {
                List<(int, int)> availableMoves = Processing.GetAvailableMoves(boardMatrix);
                if (isMaximizingPlayer)
                {
                    int bestVal = int.MinValue;
                    foreach (var move in availableMoves)
                    {
                        var boardCopy = Processing.CreateCopyOfMatrix(boardMatrix);
                        Move(boardCopy, move);
                        int val = AlphaBetaPruning(boardCopy, depth + 1, false, alpha, beta);
                        if (val > bestVal) bestVal = val;
                        if (bestVal > alpha) alpha = bestVal;
                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                    return bestVal;
                }
                else
                {
                    int bestVal = int.MaxValue;
                    foreach (var move in availableMoves)
                    {
                        var boardCopy = Processing.CreateCopyOfMatrix(boardMatrix);
                        Move(boardCopy, move);
                        int val = AlphaBetaPruning(boardCopy, depth + 1, true, alpha, beta);
                        if (val < bestVal) bestVal = val;
                        if (bestVal < beta) beta = bestVal;
                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                    return bestVal;
                }
            }
        }

        public static (int, int) FindBestMove(Board board, bool isMaximizingPlayer)
        {
            (int, int) bestMove = (-1, -1);
            List<(int, int)> bestMoves = new();
            if (isMaximizingPlayer)
            {
                int bestVal = int.MinValue;
                List<(int, int)> availableMoves = Processing.GetAvailableMoves(board.BoardState);
                foreach (var move in availableMoves)
                {
                    var boardCopy = Processing.CreateCopyOfMatrix(board.BoardState);
                    Move(boardCopy, move);
                    int val = MiniMax(boardCopy, 1, false);
                    if (val > bestVal)
                    {
                        bestVal = val;
                        bestMove = move;
                        bestMoves.Clear();
                        bestMoves.Add(move);
                    }
                    else if (val == bestVal)
                    {
                        bestMoves.Add(move);
                    }
                }
            }
            else
            {
                int bestVal = int.MaxValue;
                List<(int, int)> availableMoves = Processing.GetAvailableMoves(board.BoardState);
                foreach (var move in availableMoves)
                {
                    var boardCopy = Processing.CreateCopyOfMatrix(board.BoardState);
                    Move(boardCopy, move);
                    int val = MiniMax(boardCopy, 1, true);
                    if (val < bestVal)
                    {
                        bestVal = val;
                        bestMove = move;
                        bestMoves.Clear();
                        bestMoves.Add(move);
                    }
                    else if (val == bestVal)
                    {
                        bestMoves.Add(move);
                    }
                }
            }
            bestMove = Processing.GetRandomOne(bestMoves);
            board.Move(bestMove);
            return bestMove;
        }
        public static int MiniMax(bool[,] boardMatrix, int depth, bool isMaximizingPlayer)
        {
            if (Processing.IsTerminalState(boardMatrix))
            {
                if (isMaximizingPlayer) return 20 - depth;
                else return depth - 20;
            }
            else if (depth == MaxDepth)
            {
                if (isMaximizingPlayer) return -depth;
                else return depth;
            }
            else
            {
                List<(int, int)> availableMoves = Processing.GetAvailableMoves(boardMatrix);
                if (isMaximizingPlayer)
                {
                    int bestVal = int.MinValue;
                    foreach(var move in availableMoves)
                    {
                        var boardCopy = Processing.CreateCopyOfMatrix(boardMatrix);
                        Move(boardCopy, move);
                        int val = MiniMax(boardCopy, depth + 1, false);
                        if (val > bestVal) bestVal = val;
                    }
                    return bestVal;
                }
                else
                {
                    int bestVal = int.MaxValue;
                    foreach (var move in availableMoves)
                    {
                        var boardCopy = Processing.CreateCopyOfMatrix(boardMatrix);
                        Move(boardCopy, move);
                        int val = MiniMax(boardCopy, depth + 1, true);
                        if (val < bestVal) bestVal = val;
                    }
                    return bestVal;
                }
            }
        }
        public static void Move(bool[,] board, (int, int) point)
        {
            board[point.Item1, point.Item2] = false;
            int x = point.Item1, y = point.Item2;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Math.Abs(x - i) <= 1 && Math.Abs(y - j) <= 1) board[i, j] = false;
                }
            }
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
    }
}
