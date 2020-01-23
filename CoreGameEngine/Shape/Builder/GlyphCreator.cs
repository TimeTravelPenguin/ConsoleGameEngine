using System.Collections.Generic;
using System.Drawing;

namespace CoreGameEngine.Shape.Builder
{
  internal class GlyphCreator
  {
    private readonly IGlyphBuilder _shapeBuilder;

    public GlyphCreator(IGlyphBuilder shapeBuilder)
    {
      _shapeBuilder = shapeBuilder;
    }

    public void CreateGlyph(IEnumerable<string> pattern, char tile)
    {
      _shapeBuilder.ConstructGlyph(pattern, tile);
    }

    public IDictionary<Point, Glyph> GetGlyph()
    {
      return _shapeBuilder.Glyphs;
    }
  }
}