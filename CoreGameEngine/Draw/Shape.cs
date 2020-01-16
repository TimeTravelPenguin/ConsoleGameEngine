using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using CoreGameEngine.Draw.Builder;
using CoreGameEngine.Resources;

namespace CoreGameEngine.Draw
{
  public class Shape
  {
    private static readonly Regex Validator = new Regex(RegexPatterns.ValidShape, RegexOptions.IgnoreCase);
    private readonly IList<Glyph> _glyphs;

    public Shape(IList<Glyph> glyphs)
    {
      _glyphs = glyphs;
    }

    public static Shape New(string shape, char tile, Point glyphOrigin)
    {
      string[] matches;
      if (Validator.IsMatch(shape))
      {
        matches = Validator.Matches(shape)
          .Select(m => m.Value.ToUpperInvariant().Trim())
          .ToArray();
      }
      else
      {
        throw new ArgumentException(Exceptions.Shape_InvalidShapeRegex, nameof(shape));
      }

      var creator = new GlyphCreator(new GlyphBuilder());
      creator.CreateGlyph(matches, tile, glyphOrigin);

      return new Shape(creator.GetGlyph());
    }

    public void Draw(bool drawFromCursor = false)
    {
      foreach (var glyph in _glyphs)
      {
        if (drawFromCursor)
        {
          Console.SetCursorPosition(Console.CursorLeft + glyph.Pos.X, Console.CursorTop + glyph.Pos.Y);
        }
        else
        {
          Console.SetCursorPosition(glyph.Pos.X, glyph.Pos.Y);
        }

        Console.ForegroundColor = glyph.Color;
        Console.Write(glyph.Character);
      }
    }
  }
}