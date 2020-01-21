using System.Collections.Generic;
using System.Drawing;

namespace CoreGameEngine.Draw.Builder
{
  internal interface IGlyphBuilder
  {
    IDictionary<Point, Glyph> Glyphs { get; set; }
    void ConstructGlyph(IEnumerable<string> pattern, char tile);
    IDictionary<Point, Glyph> GetGlyph();
  }
}