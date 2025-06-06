#include <iostream>
#include <vector>
using namespace std;

const int N = 8;

int maxKnights = 0;
vector<vector<bool>> bestBoard(N, vector<bool>(N, false));

int dx[8] = {-2, -1, 1, 2, 2, 1, -1, -2};
int dy[8] = {1, 2, 2, 1, -1, -2, -2, -1};

// Помечаем клетки, которые бьёт конь
void markAttacks(int x, int y, vector<vector<int>> &attacked, int delta) {
    for (int i = 0; i < 8; ++i) {
        int nx = x + dx[i];
        int ny = y + dy[i];
        if (nx >= 0 && nx < N && ny >= 0 && ny < N)
            attacked[nx][ny] += delta;
    }
}

// Рекурсивная функция с оптимизациями
void placeKnights(int x, int y, int count, vector<vector<bool>> &board, vector<vector<int>> &attacked) {
    if (x == N) {
        if (count > maxKnights) {
            maxKnights = count;
            bestBoard = board;
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
    if (attacked[x][y] == 0) {
        board[x][y] = true;
        markAttacks(x, y, attacked, 1);
        placeKnights(nextX, nextY, count + 1, board, attacked);
        markAttacks(x, y, attacked, -1);
        board[x][y] = false;
    }

    // Пропускаем эту клетку
    placeKnights(nextX, nextY, count, board, attacked);
}

int main() {
    vector<vector<bool>> board(N, vector<bool>(N, false));
    vector<vector<int>> attacked(N, vector<int>(N, 0));

    placeKnights(0, 0, 0, board, attacked);

    cout << "Максимальное количество коней: " << maxKnights << endl;
    cout << "Расположение коней (1 — конь, 0 — пусто):\n";
    for (int i = 0; i < N; ++i) {
        for (int j = 0; j < N; ++j) {
            cout << bestBoard[i][j] << " ";
        }
        cout << endl;
    }

    return 0;
}
