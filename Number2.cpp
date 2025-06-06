#include <iostream>
#include <vector>

using namespace std;

void findMaxBobScore(int numArrows, const vector<int>& aliceArrows, vector<int>& current,
                     vector<int>& best, int& maxScore, int section = 11, int remainingArrows = 0, int currentScore = 0){
    if (section < 0) {
        if (currentScore > maxScore || (currentScore == maxScore && remainingArrows == 0)) {
            maxScore = currentScore;
            best = current;
            if (remainingArrows > 0) best[0] += remainingArrows;
        }
        return;
    }

    int arrowsNeeded = aliceArrows[section] + 1;
    if (arrowsNeeded <= remainingArrows) {
        current[section] = arrowsNeeded;
        findMaxBobScore(numArrows, aliceArrows, current, best, maxScore,
                        section - 1, remainingArrows - arrowsNeeded,
                        currentScore + section);
        current[section] = 0;
    }

    findMaxBobScore(numArrows, aliceArrows, current, best, maxScore,
                    section - 1, remainingArrows, currentScore);
}

vector<int> maximumBobPoints(int numArrows, vector<int>& aliceArrows) {
    vector<int> best(12, 0), current(12, 0);
    int maxScore = 0;
    findMaxBobScore(numArrows, aliceArrows, current, best, maxScore, 11, numArrows);
    return best;
}

int main() {
    int numArrows;
    vector<int> aliceArrows(12);

    // Ввод данных в две строки
    cout << "Введите количество стрел для Боба (numArrows): ";
    cin >> numArrows;

    cout << "Введите стрелы Алисы для 12 секций (через пробел): ";
    for (int i = 0; i < 12; i++) {
        cin >> aliceArrows[i];
    }

    // Вычисление
    vector<int> bobArrows = maximumBobPoints(numArrows, aliceArrows);

    // Вывод результата
    cout << "Оптимальное распределение стрел Боба: ";
    for (int arrows : bobArrows) {
        cout << arrows << " ";
    }

    // Подсчёт очков
    int bobScore = 0;
    for (int i = 0; i < 12; i++) {
        if (bobArrows[i] > aliceArrows[i]) {
            bobScore += i;
        }
    }
    cout << "\nСчёт Боба: " << bobScore << endl;

    return 0;
}