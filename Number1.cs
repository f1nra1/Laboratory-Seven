using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;

namespace KochCurve
{
    class Program : GameWindow
    {
        int depth = 0;
        int maxDepth = 3;
        const float PI = 3.1415f;

        public Program() : base(800, 600, GraphicsMode.Default, "Кривая Коха")
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f); // Черный фон
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Width, 0, Height, -1, 1);
        }

        void KrivayaKocha(float x1, float y1, float x2, float y2, int currentDepth)
        {
            if (currentDepth >= depth)
            {
                GL.Vertex2(x1, y1);
                GL.Vertex2(x2, y2);
                return;
            }

            float dx = (x2 - x1) / 3;
            float dy = (y2 - y1) / 3;

            float x3 = x1 + dx;
            float y3 = y1 + dy;
            float x4 = x1 + 2 * dx;
            float y4 = y1 + 2 * dy;

            float angle = PI / 3.0f;
            float x5 = x3 + (float)(Math.Cos(angle) * dx - Math.Sin(angle) * dy);
            float y5 = y3 + (float)(Math.Sin(angle) * dx + Math.Cos(angle) * dy);

            KrivayaKocha(x1, y1, x3, y3, currentDepth + 1);
            KrivayaKocha(x3, y3, x5, y5, currentDepth + 1);
            KrivayaKocha(x5, y5, x4, y4, currentDepth + 1);
            KrivayaKocha(x4, y4, x2, y2, currentDepth + 1);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Color3(0.0, 1.0, 0.0); // Зеленый цвет

            GL.Begin(PrimitiveType.Lines);
            KrivayaKocha(100, 300, 700, 300, 0);
            GL.End();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (depth < maxDepth)
            {
                System.Threading.Thread.Sleep(500); // Задержка в полсекунды
                depth++;
            }
        }

        [STAThread]
        static void Main()
        {
            using (Program program = new Program())
            {
                program.Run(60.0); // 60 FPS
            }
        }
    }
}