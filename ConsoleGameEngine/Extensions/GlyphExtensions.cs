using System;
using ConsoleGameEngine.Helpers;
using ConsoleGameEngine.Sprites;

namespace ConsoleGameEngine.Extensions
{
  public static class GlyphExtensions
  {
    public static void Draw(this Glyph glyph)
    {
      Console.ForegroundColor = glyph.Color;
      ConsoleHelper.Draw(glyph.Pos.X, glyph.Pos.Y, glyph.Value);
    }

    public static void Erase(this Glyph glyph)
    {
      Console.ForegroundColor = GameConfiguration.BackgroundColor;
      ConsoleHelper.Draw(glyph.Pos.X, glyph.Pos.Y);
    }
  }
}