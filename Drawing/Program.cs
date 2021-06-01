using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

class Program
{
    public static List<Stroke> allStrokes = new List<Stroke>();
    public static Color[] allColors = new Color[] { Color.BLACK, Color.BLUE, Color.RED, Color.GREEN, Color.PINK };
    static void Main(string[] args)
    {
        Raylib.InitWindow(1900, 1000, "Elias Paint");
        Raylib.SetTargetFPS(240);

        Color[,] canvas = new Color[Raylib.GetScreenWidth(), Raylib.GetScreenHeight()];

        Vector2 tempVector = new Vector2();
        bool secondPos = false;
        Color currentColor = Color.BLACK;

        int colorPickerWidth = 80;

        while (!Raylib.WindowShouldClose())
        {
            if (Raylib.GetMouseX() < colorPickerWidth)
                currentColor = GetColor(currentColor);

            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON))
            {
                for (int i = 0; i < allStrokes.Count; i++)
                {
                    if (isPointOnLine(Raylib.GetMousePosition(), allStrokes[i].points[0], allStrokes[i].points[1]))
                        allStrokes.RemoveAt(i);
                }
            }

            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                tempVector = Raylib.GetMousePosition();
            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
            {
                if (secondPos == false)
                    secondPos = true;
                else if (secondPos == true)
                {
                    Vector2[] vectors = new Vector2[2] { tempVector, Raylib.GetMousePosition() };

                    secondPos = false;

                    if (Vector2.Distance(vectors[0], vectors[1]) > 0 && vectors[0].X > colorPickerWidth && vectors[1].X > colorPickerWidth)
                        allStrokes.Add(new Stroke(vectors, 5, currentColor));

                    tempVector = Raylib.GetMousePosition();
                }
            }

            Raylib.ClearBackground(Color.WHITE);

            // for (int i = 100; i < canvas.GetLength(0); i++)
            // {
            //     for (int x = 100; x < canvas.GetLength(1); x++)
            //     {
            //         if (canvas[i, x].Equals(Color.WHITE))
            //             Raylib.DrawPixel(i, x, canvas[i, x]);
            //     }
            // }
            DrawScene();
            Raylib.EndDrawing();
        }
    }
    static Color GetColor(Color currentColor)
    {
        for (int i = 0; i < allColors.Length; i++)
        {
            if (Raylib.GetMouseX() > 20 && Raylib.GetMouseX() < 20 + 40 && Raylib.GetMouseY() > 20 + i * 70 && Raylib.GetMouseY() < 20 + i * 70 + 40)
            {
                Raylib.DrawRectangle(60, 20 + i * 70, 10, 40, allColors[i]);
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                    return allColors[i];
            }

        }
        return currentColor;
    }
    static bool isPointOnLine(Vector2 v1, Vector2 v2, Vector2 vm)
    {
        const float EPSILON = 0.01f;


        // float a = (v2.Y - v1.Y) / (v2.X - v1.X);
        // float b = v1.Y - a * v1.X;
        // if (Math.Abs(vm.Y - (a * vm.X + b)) < EPSILON)
        // {
        //     return true;
        // }

        // return false;
        float gradient = (v1.Y - v2.Y) / (v2.X - v1.X);

        float c = v1.Y - gradient * v1.X;

        double d = Math.Abs((v2.X / v1.X) * vm.X + (v1.Y / v2.Y) * vm.Y + c) / Math.Sqrt((v2.X / v1.X) * (v2.X / v1.X) + (v1.Y / v2.Y) * (v1.Y / v2.Y));

        bool isClose = d < 5;

        Console.WriteLine(d);

        return isClose;
    }
    static void DrawScene()
    {
        for (int i = 0; i < allStrokes.Count; i++)
        {
            Raylib.DrawCircleV(allStrokes[i].points[0], allStrokes[i].strokeWidth / 2, allStrokes[i].color);
            Raylib.DrawCircleV(allStrokes[i].points[1], allStrokes[i].strokeWidth / 2, allStrokes[i].color);
            Raylib.DrawLineEx(allStrokes[i].points[0], allStrokes[i].points[1], allStrokes[i].strokeWidth, allStrokes[i].color);
        }
        for (int i = 0; i < allColors.Length; i++)
        {
            Raylib.DrawRectangle(20, 20 + i * 70, 40, 40, allColors[i]);
        }

    }
}
struct Stroke
{
    public Vector2[] points { get; set; }
    public int strokeWidth { get; set; }
    public Color color { get; set; }
    public Stroke(Vector2[] points, int strokeWidth, Color color)
    {
        this.points = points;
        this.strokeWidth = strokeWidth;
        this.color = color;
    }
}