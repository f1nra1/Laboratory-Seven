#include <GLUT/glut.h>
#include <cmath>
using namespace std;

int windowWidth = 800;
int windowHeight = 600;
int depth = 0;
int maxDepth = 5;

const float PI = 3.1415f;

void krivayaKocha(float x1, float y1, float x2, float y2, int currentDepth) {
    if (currentDepth >= depth) {
        glVertex2f(x1, y1);
        glVertex2f(x2, y2);
        return;
    }

    float dx = (x2 - x1) / 3;
    float dy = (y2 - y1) / 3;

    float x3 = x1 + dx;
    float y3 = y1 + dy;
    float x4 = x1 + 2 * dx;
    float y4 = y1 + 2 * dy;

    float angle = PI / 3.0f;
    float x5 = x3 + cos(angle) * dx - sin(angle) * dy;
    float y5 = y3 + sin(angle) * dx + cos(angle) * dy;

    krivayaKocha(x1, y1, x3, y3, currentDepth + 1);
    krivayaKocha(x3, y3, x5, y5, currentDepth + 1);
    krivayaKocha(x5, y5, x4, y4, currentDepth + 1);
    krivayaKocha(x4, y4, x2, y2, currentDepth + 1);
}

void okno() {
    glClear(GL_COLOR_BUFFER_BIT);
    glColor3f(0.0, 1.0, 0.0); // Зеленый цвет

    glBegin(GL_LINES);
    krivayaKocha(100, 300, 700, 300, 0);
    glEnd();

    glutSwapBuffers();
}

void timer(int) {
    if (depth < maxDepth) {
        depth++;
        glutPostRedisplay();
    }
    glutTimerFunc(500, timer, 0); // Обновление каждые пол секунды
}



void init() {
    glClearColor(0.0, 0.0, 0.0, 1.0); // Черный фон
    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();
    gluOrtho2D(0, windowWidth, 0, windowHeight);
}

int main(int argc, char** argv) {
    glutInit(&argc, argv);
    glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB);
    glutInitWindowSize(windowWidth, windowHeight);
    glutCreateWindow("Кривая Коха");

    init();
    glutDisplayFunc(okno);
    glutTimerFunc(1000, timer, 0);
    glutMainLoop();

    return 0;
}