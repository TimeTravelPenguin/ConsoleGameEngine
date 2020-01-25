using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using CoreGameEngine.Extensions;
using CoreGameEngine.Resources;
using CoreGameEngine.Shapes.Builder;
using CoreGameEngine.Structs;

namespace CoreGameEngine.Shapes
{
  public class Shape : IShape
  {
    private static readonly Regex Validator = new Regex(RegexPatterns.ValidShape, RegexOptions.IgnoreCase);
    private Point3D _position;

    public IDictionary<Point, Glyph> Glyphs { get; private set; }

    public Point3D Position
    {
      get => _position;
      set
      {
        _position = value;
        Updated = true;
      }
    }

    public bool Updated { get; set; }

    public void SetGlyphs(IDictionary<Point, Glyph> glyphs)
    {
      Glyphs = new Dictionary<Point, Glyph>(glyphs);
      Updated = true;
    }

    public Shape()
    {
    }

    public Shape(IDictionary<Point, Glyph> glyphs, Point3D position)
    {
      Glyphs = glyphs;
      Position = position;
      Updated = true;
    }

    public static IShape New(IShape shape)
    {
      if (shape is null)
      {
        throw new ArgumentNullException(nameof(shape), Exceptions.ArgumentIsNull);
      }

      return shape.CreateNewShapeFrom(shape.GetType());
    }

    public static IShape New(string shape, char tile, Point3D pos)
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

      var creator = new ShapeBuilder();
      creator.ProcessGlyphPattern(matches, tile);

      return new Shape(creator.GetGlyph(), pos);
    }
  }
}