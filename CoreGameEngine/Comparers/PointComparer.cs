using System.Collections.Generic;
using System.Drawing;

namespace CoreGameEngine.Comparers
{
  public class PointComparer : IComparer<Point>
  {
    public int Compare(Point left, Point right)
    {
      if (left.Equals(right))
      {
        return 0;
      }

      return left.Y.CompareTo(right.Y) != 0 
        ? left.Y.CompareTo(right.Y) 
        : left.X.CompareTo(right.X);
    }
  }
}