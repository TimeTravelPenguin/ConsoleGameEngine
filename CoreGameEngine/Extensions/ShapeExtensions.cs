using System;
using System.Collections.Generic;
using System.Drawing;
using CoreGameEngine.Resources;
using CoreGameEngine.Shapes;

namespace CoreGameEngine.Extensions
{
  public static class ShapeExtensions
  {
    public static IShape CreateNewShapeFrom<TType>(this IShape shape)
      where TType : IShape
    {
      return shape.CreateNewShapeFrom(typeof(TType));
    }

    public static IShape CreateNewShapeFrom(this IShape shape, Type toType)
    {
      if (shape is null)
      {
        throw new ArgumentNullException(nameof(shape), Exceptions.ArgumentIsNull);
      }

      if (toType is null)
      {
        throw new ArgumentNullException(nameof(toType), Exceptions.ArgumentIsNull);
      }

      if (!typeof(IShape).IsAssignableFrom(toType))
      {
        throw new InvalidOperationException();
      }

      var newShape = (IShape) Activator.CreateInstance(toType);
      newShape.SetGlyphs(new Dictionary<Point, Glyph>(shape.Glyphs));

      return newShape;
    }
  }
}