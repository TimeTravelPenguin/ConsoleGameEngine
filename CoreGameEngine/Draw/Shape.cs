using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using CoreGameEngine.Draw.Builder;
using CoreGameEngine.Extensions;
using CoreGameEngine.Resources;
using CoreGameEngine.Structs;

namespace CoreGameEngine.Draw
{
  public class Shape : IShape
  {
    private static readonly Regex Validator = new Regex(RegexPatterns.ValidShape, RegexOptions.IgnoreCase);
    public IDictionary<Point, Glyph> Glyphs { get; }
    public Point3D Position { get; set; }

    public void Rotate(Rotation rotation)
    {
      Glyphs.RotateMatrix(rotation);
    }

    public Shape(IDictionary<Point, Glyph> glyphs, Point3D position)
    {
      Glyphs = glyphs;
      Position = position;
    }

    public static Shape New(string shape, char tile, Point3D pos)
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
      creator.CreateGlyph(matches, tile);

      return new Shape(creator.GetGlyph(), pos);
    }
  }
}