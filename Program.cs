
using Raylib_cs;
using System.Numerics;

namespace lattice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int w = 1600;
            int h = 800;
            Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow | ConfigFlags.ResizableWindow);
            Raylib.SetTargetFPS(60);
            Raylib.InitWindow(w, h, "lattice");

            List<Vector2> basisVec = 
                [
                    new(1, 0), 
                    new(0, 1),
                ];
            List<Vector2> set = [];
            int N = 20;
            float r = 3.0f;
            Color c = Color.White;
            int scale = 10;
            for (int i = -N; i < N; i++)
            {
                for (int j = -N; j < N; j++)
                {
                    set.Add(scale * (i * basisVec[0] + j * basisVec[1]) + new Vector2(w / 2, h / 2));
                }
            }
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(new(0x18, 0x18, 0x18));



                for (int i = 0; i < set.Count(); i++)
                {
                    Raylib.DrawCircleV(set[i], r, c);
                }




                Raylib.DrawFPS(0, 0);
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }
    }
}
