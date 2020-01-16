using System.Collections.Generic;
using System.Drawing;

namespace CoreGameEngine.Draw.Builder
{
  internal interface IGlyphBuilder
  {
    IList<Glyph> Glyphs { get; set; }
    void ConstructGlyph(IEnumerable<string> pattern, char tile, Point pos);
    IList<Glyph> GetGlyph();
  }
}