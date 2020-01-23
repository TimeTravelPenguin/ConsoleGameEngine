#region Usings

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CoreGameEngine.Resources;
using CoreGameEngine.Shapes;

#endregion

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

            foreach (var p in points.Keys)
            {
                var newPoint = new Point(maxY - p.Y, p.X);
                var value = points[new Point(p.X, p.Y)];
                rotated.Add(newPoint, value);
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

            foreach (var p in points.Keys)
            {
                var newPoint = new Point(p.Y, maxX - p.X);
                var value = points[new Point(p.X, p.Y)];
                rotated.Add(newPoint, value);
            }

            return rotated;
        }
    }
}