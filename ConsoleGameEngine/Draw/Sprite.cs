using System;
using ConsoleGameEngine.Extensions;

namespace ConsoleGameEngine.Draw
{
  public class Sprite
  {
    public int Height { get; set; }
    public int Width { get; set; }
    public Glyph[,] Glyphs { get; set; }
    public ConsoleColor Color { get; set; }
    public Point Origin { get; set; }

    public Sprite(int height, int width, Point origin, Glyph[,] glyphs = null, ConsoleColor color = ConsoleColor.White)
    {
      Height = height;
      Width = width;
      Origin = origin;
      Glyphs = glyphs ?? new Glyph[0, 0];
      Color = color;
    }

    public static Sprite NewRectangle(int width, int height, Point origin, ConsoleColor color = ConsoleColor.White)
    {
      var glyphs = new Glyph[height, width];
      for (var y = 0; y < height; y++)
      {
        for (var x = 0; x < width; x++)
        {
          glyphs[y, x] = Glyph.RectangleGlyph(Point.Zero, color);
        }
      }

      return new Sprite(height, width, origin, glyphs, color);
    }

    public void UpdatePos(int xPos, int yPos)
    {
      for (var x = 0; x < Glyphs.GetLength(0); x++)
      {
        for (var y = 0; y < Glyphs.GetLength(1); y++)
        {
          Glyphs[x, y].Erase();

          Glyphs[x, y].Pos.X = xPos + x;
          Glyphs[x, y].Pos.Y = yPos + y;

          Glyphs[x, y].Draw();
        }
      }
    }
  }
}