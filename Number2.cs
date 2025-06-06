using System;
using System.Collections.Generic;

class Program
{
    static void FindMaxBobScore(int numArrows, List<int> aliceArrows, List<int> current,
                               ref List<int> best, ref int maxScore, int section = 11, int remainingArrows = 0, int currentScore = 0)
    {
        if (section < 0)
        {
            if (currentScore > maxScore || (currentScore == maxScore && remainingArrows == 0))
            {
                maxScore = currentScore;
                best = new List<int>(current);
                if (remainingArrows > 0) best[0] += remainingArrows;
            }
            return;
        }

        int arrowsNeeded = aliceArrows[section] + 1;
        if (arrowsNeeded <= remainingArrows)
        {
            current[section] = arrowsNeeded;
            FindMaxBobScore(numArrows, aliceArrows, current, ref best, ref maxScore,
                           section - 1, remainingArrows - arrowsNeeded,
                           currentScore + section);
            current[section] = 0;
        }

        FindMaxBobScore(numArrows, aliceArrows, current, ref best, ref maxScore,
                       section - 1, remainingArrows, currentScore);
    }

    static List<int> MaximumBobPoints(int numArrows, List<int> aliceArrows)
    {
        List<int> best = new List<int>(new int[12]);
        List<int> current = new List<int>(new int[12]);
        int maxScore = 0;
        FindMaxBobScore(numArrows, aliceArrows, current, ref best, ref maxScore, 11, numArrows);
        return best;
    }

    static void Main()
    {
        Console.Write("Введите количество стрел для Боба (numArrows): ");
        int numArrows = int.Parse(Console.ReadLine());

        Console.Write("Введите стрелы Алисы для 12 секций (через пробел): ");
        string[] input = Console.ReadLine().Split(' ');
        List<int> aliceArrows = new List<int>();
        foreach (string s in input)
        {
            aliceArrows.Add(int.Parse(s));
        }

        List<int> bobArrows = MaximumBobPoints(numArrows, aliceArrows);

        Console.Write("Оптимальное распределение стрел Боба: ");
        foreach (int arrows in bobArrows)
        {
            Console.Write(arrows + " ");
        }

        int bobScore = 0;
        for (int i = 0; i < 12; i++)
        {
            if (bobArrows[i] > aliceArrows[i])
            {
                bobScore += i;
            }
        }
        Console.WriteLine("\nСчёт Боба: " + bobScore);
    }
}