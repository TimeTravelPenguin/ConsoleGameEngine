using System;

namespace ConsoleGameEngine.Draw
{
  public class Glyph
  {
    public Point Pos { get; set; }
    public char Value { get; set; }
    public ConsoleColor Color { get; set; }

    public Glyph(Point pos, char value, ConsoleColor color = ConsoleColor.White)
    {
      Pos = pos;
      Value = value;
      Color = color;
    }

    public static Glyph RectangleGlyph(Point pos, ConsoleColor color = ConsoleColor.White)
    {
      return new Glyph(pos, Characters.Rectangle, color);
    }
  }
}