using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using CoreGameEngine.Resources;

namespace CoreGameEngine.Shapes.Builder
{
  public static class ShapeCreator
  {
    public static void ProcessGlyphPattern([NotNull] this IShapeBuilder builder, [NotNull] IEnumerable<string> pattern,
      char tile)
    {
      if (builder is null)
      {
        throw new ArgumentNullException(nameof(builder), Exceptions.ArgumentIsNull);
      }

      builder.ConstructGlyph(pattern, tile);
    }

    public static IDictionary<Point, Glyph> GetGlyph([NotNull] this IShapeBuilder builder)
    {
      if (builder is null)
      {
        throw new ArgumentNullException(nameof(builder), Exceptions.ArgumentIsNull);
      }

      return builder.Glyphs;
    }
  }
}