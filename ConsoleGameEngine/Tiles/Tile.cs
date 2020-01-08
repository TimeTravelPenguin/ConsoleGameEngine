using System;
using ConsoleGameEngine.Sprites;

namespace ConsoleGameEngine.Tiles
{
  public class Tile
  {
    public string Value;

    public ConsoleColor Color { get; set; } = ConsoleColor.White;

    public Point Pos { get; set; }
    public Tile TileUp { get; set; }
    public Tile TileDown { get; set; }
    public Tile TileLeft { get; set; }
    public Tile TileRight { get; set; }

    public void DrawTile()
    {
      Console.SetCursorPosition(Pos.X, Pos.Y);
      Console.ForegroundColor = Color;
      Console.Write(Value);
    }
  }
}