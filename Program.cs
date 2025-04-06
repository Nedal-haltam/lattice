
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using static System.Formats.Asn1.AsnWriter;

namespace lattice
{
    internal class Program
    {
        static int w = 1600;
        static int h = 800;
        static List<Vector2> update(List<Vector2> basisVec, int N, float scale)
        {
            List<Vector2> outPutSet = [];
            if (basisVec.Count != 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error in parameter basisVec number of basis vectors is `{basisVec.Count}` rather the `2`\n");
                Environment.Exit(1);
            }
            for (int i = -N; i < N; i++)
            {
                for (int j = -N; j < N; j++)
                {
                    outPutSet.Add(scale * (i * basisVec[0] + j * basisVec[1]) + new Vector2(w / 2, h / 2));
                }
            }
            return outPutSet;
        }
        static void render(List<Vector2> set, float r, Color c)
        {
            for (int i = 0; i < set.Count(); i++)
            {
                Raylib.DrawCircleV(set[i], r, c);
            }
        }
        static void updateAndRender(List<Vector2> basisVec, int N, float scale, float r, Color c)
        {
            List<Vector2> set = update(basisVec, N, scale);
            render(set, r, c);
        }
        static void TakeScreenShotAndSave(int i, List<Vector2> basisVec)
        {
            // ffmpeg -framerate 60 -i image%d.png -c:v libx264 -pix_fmt yuv420p output.mp4
            string dirpath = $"./pics/";
            string filepath = $"{i}-X{basisVec[0].X}Y{basisVec[0].Y}-X{basisVec[1].X}Y{basisVec[1].Y}.png";
            string destination = dirpath + $"{filepath}";
            if (!Directory.Exists(dirpath))
                Directory.CreateDirectory(dirpath);
            Image image = Raylib.LoadImageFromScreen();
            Raylib.ExportImage(image, filepath);
            Raylib.UnloadImage(image);
            while (!File.Exists(filepath));
            File.Move(filepath, destination);
        }
        static void Main(string[] args)
        {
            Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow | ConfigFlags.ResizableWindow);
            Raylib.SetTargetFPS(60);
            Raylib.InitWindow(w, h, "lattice");

            int N = 50;
            float r = 2.0f;
            int scale = 3;
            Color c = Color.White;
            int i = 0;
            List<Vector2> basis = [new(15, 23), new(27, 30)];
            List<Vector2> lattice = update(basis, N, scale);
            while (!Raylib.WindowShouldClose())
            {
                w = Raylib.GetScreenWidth();
                h = Raylib.GetScreenHeight();
                Raylib.BeginDrawing();
                Raylib.ClearBackground(new(0x18, 0x18, 0x18));


                render(lattice, r, c);
                //TakeScreenShotAndSave(i++, basis);
                //Console.WriteLine("vector finished redering and image is stored\n");

                Raylib.DrawFPS(0, 0);
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }
    }
}
