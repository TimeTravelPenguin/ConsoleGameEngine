using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CoreGameEngine.Draw;
using CoreGameEngine.Resources;

namespace CoreGameEngine.Helpers
{
  public enum Rotation
  {
    Clockwise,
    CounterClockwise
  }

  public static class ShapeRotator
  {
    public static void Rotate(this IShape shape, Rotation rotationDirection)
    {
      if (shape is null)
      {
        throw new ArgumentNullException(nameof(shape), Exceptions.Argument_IsNull);
      }

      shape.SetGlyphs(shape.Glyphs.Rotate(rotationDirection));
    }

    private static IDictionary<Point, T> Rotate<T>(this IDictionary<Point, T> sorted, Rotation rotation)
    {
      // Clockwise rotation is
      // last y -> first y,
      // first x -> last x

      // C-Clockwise rotation is
      // first y -> last y,
      // last x -> first x

      return rotation switch
      {
        Rotation.Clockwise => sorted.RotateClockwise(),
        Rotation.CounterClockwise => sorted.RotateCounterClockwise(),
        _ => throw new ArgumentOutOfRangeException(nameof(rotation), rotation, null)
      };
    }

    private static IDictionary<Point, T> RotateClockwise<T>(this IDictionary<Point, T> points)
    {
      if (points is null)
      {
        throw new ArgumentNullException(nameof(points), Exceptions.Argument_IsNull);
      }

      var rotated = new Dictionary<Point, T>();
      var maxY = points.Keys.Select(p => p.Y).Max();

      foreach (var y in points.Keys.Select(p => p.Y).Distinct().OrderByDescending(p => p))
      {
        foreach (var x in points.Keys.Where(p => p.Y == y).Select(p => p.X).OrderBy(p => p))
        {
          var newPoint = new Point(maxY - y, x);
          var value = points[new Point(x, y)];
          rotated.Add(newPoint, value);
        }
      }

      return rotated;
    }

    private static IDictionary<Point, T> RotateCounterClockwise<T>(this IDictionary<Point, T> points)
    {
      if (points is null)
      {
        throw new ArgumentNullException(nameof(points), Exceptions.Argument_IsNull);
      }

      var rotated = new Dictionary<Point, T>();
      var maxX = points.Keys.Select(p => p.X).Max();

      foreach (var y in points.Keys.Select(p => p.Y).Distinct().OrderBy(p => p))
      {
        foreach (var x in points.Keys.Where(p => p.Y == y).Select(p => p.X).OrderByDescending(p => p))
        {
          var newPoint = new Point(y, maxX - x);
          var value = points[new Point(x, y)];
          rotated.Add(newPoint, value);
        }
      }

      return rotated;
    }
  }
}