using AllOverIt.Fixture;
using CoreGameEngine.Structs;
using FluentAssertions;
using Xunit;

namespace CoreGameEngine.xUnit.Tests.ObjectsTests
{
  public class Point3DTests : AoiFixtureBase
  {
    public class InitializationTests
    {
      [Fact]
      public void EmptyReturnsCorrectInstance()
      {
        var point = Point3D.Empty;

        point.Should()
          .Be(new Point3D(0, 0, 0));
      }
    }

    public class AdditionTests : Point3DTests
    {
      [Fact]
      public void AddNonStatic()
      {
        var left = Create<Point3D>();
        var right = Create<Point3D>();

        var actual = new Point3D(left.X, left.Y, left.Z);
        actual.Add(right);

        actual.Should()
          .Be(new Point3D(left.X + right.X, left.Y + right.Y, left.Z + right.Z));
      }

      [Fact]
      public void AddStatic()
      {
        var left = Create<Point3D>();
        var right = Create<Point3D>();

        var actual = new Point3D(left.X, left.Y, left.Z);
        actual += right;

        actual.Equals(left + right)
          .Should()
          .BeTrue();

        actual.Should()
          .Be(new Point3D(left.X + right.X, left.Y + right.Y, left.Z + right.Z));
      }
    }

    public class SubtractionTests : Point3DTests
    {
      [Fact]
      public void SubtractNonStatic()
      {
        var left = Create<Point3D>();
        var right = Create<Point3D>();

        var actual = new Point3D(left.X, left.Y, left.Z);
        actual.Subtract(right);

        actual.Should()
          .Be(new Point3D(left.X - right.X, left.Y - right.Y, left.Z - right.Z));
      }

      [Fact]
      public void SubtractStatic()
      {
        var left = Create<Point3D>();
        var right = Create<Point3D>();

        var actual = new Point3D(left.X, left.Y, left.Z);
        actual -= right;

        actual.Should()
          .Be(new Point3D(left.X - right.X, left.Y - right.Y, left.Z - right.Z));
      }
    }

    public class EqualityTests : Point3DTests
    {
      [Fact]
      public void AreEqual()
      {
        var left = Create<Point3D>();
        var right = new Point3D(left.X, left.Y, left.Z);

        left.Equals(right)
          .Should()
          .BeTrue();
      }

      [Fact]
      public void AreNotEqual()
      {
        var left = Create<Point3D>();
        Point3D right;

        do
        {
          right = Create<Point3D>();
        } while (right.Equals(left));

        left.Equals(right)
          .Should()
          .BeFalse();

        (left != right).Should()
          .BeTrue();
      }
    }

    public class ToStringTests : Point3DTests
    {
      [Fact]
      public void ToStringTest()
      {
        var point = Create<Point3D>();
        var actual = point.ToString();

        var expected = $"{{X={point.X},Y={point.Y},Z={point.Z}}}";

        actual.Should()
          .Be(expected);
      }
    }
  }
}