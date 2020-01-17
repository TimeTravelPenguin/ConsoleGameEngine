using System.Collections.Generic;
using System.Drawing;

namespace CoreGameEngine.Draw.Builder
{
  internal class GlyphCreator
  {
    private readonly IGlyphBuilder _shapeBuilder;

    public GlyphCreator(IGlyphBuilder shapeBuilder)
    {
      _shapeBuilder = shapeBuilder;
    }

    public void CreateGlyph(IEnumerable<string> pattern, char tile, Point pos)
    {
      _shapeBuilder.ConstructGlyph(pattern, tile, pos);
    }

    public IList<Glyph> GetGlyph()
    {
      return _shapeBuilder.Glyphs;
    }
  }
}