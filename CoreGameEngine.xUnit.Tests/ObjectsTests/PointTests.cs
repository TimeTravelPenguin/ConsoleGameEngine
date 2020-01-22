using System.Collections.Generic;
using System.Drawing;
using AllOverIt.Fixture;
using CoreGameEngine.Comparers;
using Xunit;

namespace CoreGameEngine.xUnit.Tests.ObjectsTests
{
  public class PointTests : AoiFixtureBase
  {
    public class ComparerTests
    {
      [Fact]
      public void IsSorted()
      {
        var sorted = new SortedDictionary<Point, int>(new PointComparer())
        {
          [new Point(0, 1)] = 2,
          [new Point(1, 1)] = 3,
          [new Point(1, 0)] = 1,
          [new Point(2, 1)] = 4,
          [new Point(2, 2)] = 6,
          [new Point(3, 1)] = 5,
          [new Point(1, 3)] = 7
        };

        // TODO

        Assert.False(true);
      }
    }
  }
}