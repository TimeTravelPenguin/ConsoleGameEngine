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
            /* Rotation Logic

                  Clockwise rotation is
                  last y -> first y,
                  first x->last x

                  C - Clockwise rotation is
                  first y -> last y,
                  last x->first x

            */

            return rotation switch
            {
                Rotation.Clockwise => sorted.RotateClockwise(),
                Rotation.CounterClockwise => sorted.RotateCounterClockwise(),
                _ => throw new ArgumentOutOfRangeException(nameof(rotation), rotation, null)
            };
        }

        private static IDictionary<Point, T> RotateClockwise<T>(this IDictionary<Point, T> points)
        {
            return Rotate(points, p => p.Y, (point, max) => (max - point.Y, point.X));
        }

        private static IDictionary<Point, T> RotateCounterClockwise<T>(this IDictionary<Point, T> points)
        {
            return Rotate(points, p => p.X, (point, max) => (point.Y, max - point.X));
        }

        /// <summary>
        ///     Rotates an <see cref="IDictionary{TKey,TValue}" /> with <see cref="Point" /> key representing a matrix.
        /// </summary>
        /// <typeparam name="T">
        ///     The value type of the dictionary being rotated.
        /// </typeparam>
        /// <param name="points">
        ///     The <see cref="IDictionary{TKey,TValue}" /> to rotate.
        /// </param>
        /// <param name="maxSelector">
        ///     The maximum <see cref="Point.X" /> or <see cref="Point.Y" /> value.
        ///     If rotating clockwise, <see cref="Point.Y" /> is used; for counter-clockwise, <see cref="Point.X" />.
        ///     <para>
        ///         <example>
        ///             This is an example of selecting the <see cref="Point.Y" /> parameter in the event of rotating clockwise,
        ///             given a <see cref="Point" /> p:
        ///             <code>p => p.Y</code>
        ///         </example>
        ///     </para>
        /// </param>
        /// <param name="coordinateMapper">
        ///     This <see cref="Func{T1, T2, TResult}" /> takes a <see cref="Point" /> and <see cref="int" /> input representing
        ///     the current point to be mapped to the new rotated coordinate, and <paramref name="maxSelector" />.
        ///     The output is a the newly mapped coordinates.
        ///     <example>
        ///         The following is an example of rotating <paramref name="points" />
        ///         Clockwise:
        ///         <code>
        /// foreach (var p in points.Keys)
        /// {
        ///    var (x, y) = (point, max) => (max - point.Y, point.X);
        ///    var newPoint = new Point(x, y);
        ///    var value = points[new Point(p.X, p.Y)];
        ///    rotatedDictionary.Add(newPoint, value);
        /// }
        ///  </code>
        ///         Counter-clockwise:
        ///         <code>
        /// foreach (var p in points.Keys)
        /// {
        ///    var (x, y) = (point, max) => (point.Y, max - point.X);
        ///    var newPoint = new Point(x, y);
        ///    var value = points[new Point(p.X, p.Y)];
        ///    rotatedDictionary.Add(newPoint, value);
        /// }
        ///  </code>
        ///     </example>
        /// </param>
        /// <returns>
        ///     Returns a new <see cref="IDictionary{TKey,TValue}" /> with the values mapped to new keys. The keys are the newly
        ///     mapped rotated coordinate.
        /// </returns>
        private static IDictionary<Point, T> Rotate<T>(this IDictionary<Point, T> points, Func<Point, int> maxSelector,
            Func<Point, int, (int newX, int newY)> coordinateMapper)
        {
            if (points is null)
            {
                throw new ArgumentNullException(nameof(points), Exceptions.Argument_IsNull);
            }

            var rotated = new Dictionary<Point, T>();
            var max = points.Keys.Select(maxSelector).Max();

            foreach (var p in points.Keys)
            {
                var (x, y) = coordinateMapper.Invoke(p, max);
                var newPoint = new Point(x, y);
                var value = points[new Point(p.X, p.Y)];
                rotated.Add(newPoint, value);
            }

            return rotated;
        }
    }
}