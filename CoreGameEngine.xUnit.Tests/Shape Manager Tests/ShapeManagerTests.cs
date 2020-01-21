using System.Drawing;
using CoreGameEngine.Draw;
using CoreGameEngine.Shape_Manager;
using CoreGameEngine.Structs;
using FluentAssertions;
using Xunit;

namespace CoreGameEngine.xUnit.Tests.Shape_Manager_Tests
{
  public class ShapeManagerTests
  {
    public class AddTests
    {
      [Fact]
      public void AddItemTest_ContainsAddedItem()
      {
        var manager = ShapeManager.NewManager();

        var point = new Point3D(1, 2, 3);
        var shape = Shape.New("d3 r1", 'X', point);

        manager.Add(shape);

        manager.VirtualLocations
          .Should()
          .ContainKey(point)
          .WhichValue
          .Should()
          .Be(shape);

        manager.VirtualReverse
          .Should()
          .ContainKey(shape)
          .WhichValue
          .Should()
          .Be(point);
      }
    }
  }
}