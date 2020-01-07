using ConsoleGameEngine.Draw;
using ConsoleGameEngine.Helpers;

namespace ConsoleGameEngine.Extensions
{
  public static class GlyphExtensions
  {
    public static void Draw(this Glyph glyph)
    {
      ConsoleHelper.Draw(glyph.Pos.X, glyph.Pos.Y, glyph.Value);
    }

    public static void Erase(this Glyph glyph)
    {
      ConsoleHelper.Draw(glyph.Pos.X, glyph.Pos.Y);
    }
  }
}