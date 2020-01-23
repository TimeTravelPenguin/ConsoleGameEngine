using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using CoreGameEngine.Extensions;

namespace CoreGameEngine.Shapes.Builder
{
  internal class GlyphBuilder : IGlyphBuilder
  {
    private ConsoleColor _color;
    private Point _pos;

    public IDictionary<Point, Glyph> Glyphs { get; set; } = new Dictionary<Point, Glyph>();

    public void ConstructGlyph(IEnumerable<string> pattern, char tile)
    {
      _color = ConsoleColor.White;
      _pos = Point.Empty;

      foreach (var p in pattern)
      {
        if (p.StartsWith(new[] {"U", "D", "L", "R"}))
        {
          AddGlyphs(p, tile);
        }
        else if (p.StartsWith("N", StringComparison.InvariantCulture))
        {
          SetPos(p);
        }
        else if (p.StartsWith("C", StringComparison.InvariantCulture))
        {
          SetColour(p);
        }
      }
    }

    public IDictionary<Point, Glyph> GetGlyph()
    {
      return Glyphs;
    }

    private void SetColour(string colour)
    {
      var p = Regex.Matches(colour, @"\w+").Select(m => m.Value).ToArray();
      _color = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), p[1].ToLowerInvariant().FirstCharToUpper());
    }

    private void SetPos(string pos)
    {
      var p = Regex.Matches(pos, @"\d+").Select(m => m.Value).ToArray();

      var x = int.Parse(p[0], NumberStyles.Integer, CultureInfo.InvariantCulture);
      var y = int.Parse(p[1], NumberStyles.Integer, CultureInfo.InvariantCulture);

      _pos = new Point(x, y);
    }

    private void AddGlyphs(string pattern, char tile)
    {
      var p = Regex.Matches(pattern, @"[UDLR]|\d+").Select(m => m.Value).ToArray();
      var amount = int.Parse(p[1], NumberStyles.Integer, CultureInfo.InvariantCulture);

      for (var i = 0; i < amount; i++)
      {
        var newPos = p[0] switch
        {
          "U" => new Point(_pos.X, _pos.Y - 1),
          "D" => new Point(_pos.X, _pos.Y + 1),
          "L" => new Point(_pos.X - 1, _pos.Y),
          "R" => new Point(_pos.X + 1, _pos.Y),
          _ => throw new InvalidOperationException()
        };

        Glyphs.Add(newPos, new Glyph(tile, _color));

        _pos = newPos;
      }
    }
  }
}