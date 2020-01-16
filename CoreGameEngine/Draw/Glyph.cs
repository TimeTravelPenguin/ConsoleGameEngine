using System;
using System.Drawing;

namespace CoreGameEngine.Draw
{
  public class Glyph
  {
    public ConsoleColor Color { get; set; }
    public char Character { get; set; }
    public Point Pos { get; set; }

    public Glyph(char character, Point pos, ConsoleColor color = ConsoleColor.White)
    {
      Character = character;
      Color = color;
      Pos = pos;
    }

    public Glyph(char character, ConsoleColor color = ConsoleColor.White)
    {
      Character = character;
      Color = color;
      Pos = new Point(0, 0);
    }

    public Glyph() : this((char) 32)
    {
    }
  }
}