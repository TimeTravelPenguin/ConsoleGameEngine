using System;
using ConsoleGameEngine.Sprites;

namespace ConsoleGameEngine.Tiles
{
  public class Tile<T>
  {
    public char Value;

    public ConsoleColor Color { get; set; } = ConsoleColor.White;

    public Point Pos { get; set; }
    public T TileUp { get; set; }
    public T TileDown { get; set; }
    public T TileLeft { get; set; }
    public T TileRight { get; set; }

    public void DrawTile()
    {
      DrawTile(Value);
    }

    public void DrawTile(char character)
    {
      Console.SetCursorPosition(Pos.X, Pos.Y);
      Console.ForegroundColor = Color;
      Console.Write(character);
    }
  }
}