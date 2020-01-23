using System.Collections.Generic;
using System.Drawing;

namespace CoreGameEngine.Shapes.Builder
{
  public interface IShapeBuilder
  {
    IDictionary<Point, Glyph> Glyphs { get; }
    void ConstructGlyph(IEnumerable<string> pattern, char tile);
    IDictionary<Point, Glyph> GetGlyph();
  }
}