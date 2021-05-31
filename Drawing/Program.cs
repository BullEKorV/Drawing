using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

class Program
{
    public static List<Stroke> allStrokes = new List<Stroke>();
    public static Vector2 tempVector = new Vector2();
    public static bool secondPos = false;
    static void Main(string[] args)
    {
        Raylib.InitWindow(1900, 1000, "Test");
        while (!Raylib.WindowShouldClose())
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                tempVector = Raylib.GetMousePosition();
            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
            {
                if (secondPos == false)
                    secondPos = true;
                else if (secondPos == true)
                {
                    Vector2[] vectors = new Vector2[2];
                    vectors[0] = tempVector;
                    vectors[1] = Raylib.GetMousePosition();

                    secondPos = false;

                    allStrokes.Add(new Stroke(vectors, 5, Color.BLACK));

                    tempVector = Raylib.GetMousePosition();
                }
            }
            Console.WriteLine(allStrokes.Count);

            Raylib.ClearBackground(Color.WHITE);

            for (int i = 0; i < allStrokes.Count; i++)
            {
                Raylib.DrawCircleV(allStrokes[i].points[0], allStrokes[i].strokeWidth / 2, allStrokes[i].color);
                Raylib.DrawCircleV(allStrokes[i].points[1], allStrokes[i].strokeWidth / 2, allStrokes[i].color);
                Raylib.DrawLineEx(allStrokes[i].points[0], allStrokes[i].points[1], allStrokes[i].strokeWidth, allStrokes[i].color);
            }

            Raylib.EndDrawing();
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