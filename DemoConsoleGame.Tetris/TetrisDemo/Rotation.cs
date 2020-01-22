using System;
using System.Collections.Generic;

namespace RotationQuestion
{
  /*
   * Example Shape:
   *
   *    | X - X - |
   *    | X - X X |
   *    | - X - - |
   *
   * ( "X" represents the drawn object, "-" represents nothing)
   * Therefore, the ShapeGlyphs Dictionary contains keys:
   * (0,0), (2,0),
   * (0,1), (2,1), (3, 1)
   * (1,2)
   *
   * Now, rotating it clockwise yields:
   *
   *    | - X X |
   *    | X - - |
   *    | - X X |
   *    | - X - |
   *
   * With new keys:
   * (1,0), (2,0),
   * (0,1),
   * (1,2), (2,2),
   * (1, 3)
   *
   * Mathematically, the result of rotating either clockwise/counter-clockwise is simply taking the transpose of the matrix,
   * and then reversing either the rows or the columns, depending on the direction of rotation.
   *
   * The transpose of a matrix can be obtained by reflecting the elements along its main diagonal (main diagonal being (0,0), (1,1), etc.).
   *
   * Algorithmically, as an alternative, rotation can be made by starting at the bottom left or right of the matrix, depending on desired
   * rotation direction, and from bottom-to-top, from one side of the matrix, to the other, filling in a new array from row by row, starting from the top.
   *
   * This algorithmic method may be more optimal if the intended method of "rotation" was to be performed on the dictionary WITHOUT converting into
   * a 2-dimensional array. You would need to order the keys by their y-values, and then follow the algorithm, filling a new dictionary, being sure to
   * add with the new key, while maintaining the same value, so to keep the same instance of the object.
   */

  // The following code is analogous to code in a main project.

  public enum Rotation
  {
    Clockwise,
    CounterClockwise
  }

  public class Shape
  {
    // This dictionary is used to represent the objects that make up a shape
    public IDictionary<(int x, int y), object> ShapeGlyphs { get; set; }

    public Shape(IDictionary<(int x, int y), object> glyphs = null)
    {
      ShapeGlyphs = glyphs ?? new Dictionary<(int x, int y), object>();
    }
  }

  public static class ShapeManager
  {
    public static void Rotate(this Shape shape, Rotation rotation)
    {
      shape.ShapeGlyphs = Rotate(shape.ShapeGlyphs);
    }

    private static IDictionary<(int x, int y), object> Rotate(IDictionary<(int x, int y), object> shapeGlyphs)
    {
      throw new NotImplementedException();
    }
  }

  public static class ProgramTest
  {
    // This serves as an example with expected result
    public static void DemoMain()
    {
      /*
       * Shape:
       *
       *    X
       *    X X
       *
       * Rotated:
       *          
       *    X X     or      X
       *    X               X X
       */

      var glyphs = new Dictionary<(int x, int y), object>
      {
        [(0, 0)] = new object(),
        [(0, 1)] = new object(),
        [(1, 1)] = new object()
      };

      var shape = new Shape(glyphs);
      shape.Rotate(Rotation.Clockwise);

      var expectedClockwiseRotation = new Dictionary<(int x, int y), object>
      {
        [(0, 0)] = glyphs[(0, 1)],
        [(1, 0)] = glyphs[(0, 0)],
        [(0, 1)] = glyphs[(1, 1)]
      };
    }
  }
}