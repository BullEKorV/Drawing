using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

class Program
{
    public static bool isDrawing = false;
    static void Main(string[] args)
    {
        Raylib.InitWindow(1900, 1000, "Test");
        while (!Raylib.WindowShouldClose())
        {
            Raylib.ClearBackground(Color.WHITE);

            if (isDrawing)
            {

            }

            Raylib.EndDrawing();
        }
    }
}

class Object
{
    public List<Object> allObjects = new List<Object>();
    public List<Vector2> points = new List<Vector2>();
    public Object(List<Vector2> points)
    {
        this.points = points;
        allObjects.Add(this);
    }
}