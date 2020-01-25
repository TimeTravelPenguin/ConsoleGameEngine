using System.Drawing;

namespace CoreGameEngine.Extensions
{
  public static class PointExtensions
  {
    public static Point Multiply(this Point left, Point right)
    {
      return new Point(unchecked(left.X * right.X), unchecked(left.Y * right.Y));
    }
  }
}