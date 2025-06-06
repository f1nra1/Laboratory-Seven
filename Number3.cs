using System;
using System.Collections.Generic;

class Program
{
    const int N = 8;
    static int maxKnights = 0;
    static bool[,] bestBoard = new bool[N, N];

    static int[] dx = { -2, -1, 1, 2, 2, 1, -1, -2 };
    static int[] dy = { 1, 2, 2, 1, -1, -2, -2, -1 };

    // Помечаем клетки, которые бьёт конь
    static void MarkAttacks(int x, int y, int[,] attacked, int delta)
    {
        for (int i = 0; i < 8; ++i)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];
            if (nx >= 0 && nx < N && ny >= 0 && ny < N)
                attacked[nx, ny] += delta;
        }
    }

    // Рекурсивная функция с оптимизациями
    static void PlaceKnights(int x, int y, int count, bool[,] board, int[,] attacked)
    {
        if (x == N)
        {
            if (count > maxKnights)
            {
                maxKnights = count;
                Array.Copy(board, bestBoard, board.Length);
            }
            return;
        }

        // Отсечение: если даже на все оставшиеся клетки поставить коней — максимум не побьём
        int remaining = N * N - (x * N + y);
        if (count + remaining <= maxKnights)
            return;

        int nextX = x + (y + 1) / N;
        int nextY = (y + 1) % N;

        // Ставим коня, если не бьют
        if (attacked[x, y] == 0)
        {
            board[x, y] = true;
            MarkAttacks(x, y, attacked, 1);
            PlaceKnights(nextX, nextY, count + 1, board, attacked);
            MarkAttacks(x, y, attacked, -1);
            board[x, y] = false;
        }

        // Пропускаем эту клетку
        PlaceKnights(nextX, nextY, count, board, attacked);
    }

    static void Main()
    {
        bool[,] board = new bool[N, N];
        int[,] attacked = new int[N, N];

        PlaceKnights(0, 0, 0, board, attacked);

        Console.WriteLine($"Максимальное количество коней: {maxKnights}");
        Console.WriteLine("Расположение коней (1 — конь, 0 — пусто):");
        for (int i = 0; i < N; ++i)
        {
            for (int j = 0; j < N; ++j)
            {
                Console.Write(bestBoard[i, j] ? "1 " : "0 ");
            }
            Console.WriteLine();
        }
    }
}