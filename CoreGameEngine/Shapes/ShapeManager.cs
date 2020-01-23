using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CoreGameEngine.Extensions;
using CoreGameEngine.Resources;
using CoreGameEngine.Structs;

namespace CoreGameEngine.Shapes
{
  public class ShapeManager
  {
    private IDictionary<IShape, Point3D> ReferenceLookup { get; }
    public IDictionary<Point3D, IShape> ShapeLocations { get; }

    private ShapeManager([NotNull] IDictionary<Point3D, IShape> shapeLocations,
      [NotNull] IDictionary<IShape, Point3D> referenceLookup)
    {
      ShapeLocations = shapeLocations ??
                       throw new ArgumentNullException(nameof(shapeLocations), Exceptions.Argument_IsNull);

      ReferenceLookup = referenceLookup ??
                        throw new ArgumentNullException(nameof(referenceLookup), Exceptions.Argument_IsNull);
    }

    public static ShapeManager NewManager()
    {
      return new ShapeManager(new Dictionary<Point3D, IShape>(), new Dictionary<IShape, Point3D>());
    }

    public void Add([NotNull] IShape shape)
    {
      if (shape is null)
      {
        throw new ArgumentNullException(nameof(shape), Exceptions.Argument_IsNull);
      }

      if (ShapeLocations.ContainsKey(shape.Position))
      {
        throw new InvalidOperationException(Exceptions.Dictionary_KeyAlreadyExists);
      }

      if (ReferenceLookup.ContainsKey(shape))
      {
        throw new InvalidOperationException(Exceptions.Dictionary_KeyAlreadyExists);
      }

      ShapeLocations.Add(shape.Position, shape);
      ReferenceLookup.Add(shape, shape.Position);
    }

    public void Remove(Point3D key)
    {
      if (!ShapeLocations.ContainsKey(key))
      {
        throw new InvalidOperationException(Exceptions.Dictionary_InvalidKey);
      }

      ReferenceLookup.Remove(ShapeLocations[key]);
      ShapeLocations.Remove(key);
    }

    public void UpdateShape([NotNull] IShape newShape, Point3D newPoint)
    {
      if (newShape is null)
      {
        throw new ArgumentNullException(nameof(newShape), Exceptions.Argument_IsNull);
      }

      if (!ReferenceLookup.ContainsKey(newShape))
      {
        throw new InvalidOperationException(Exceptions.Dictionary_InvalidKey);
      }

      ShapeLocations.UpdateKey(newPoint, ReferenceLookup[newShape]);
      ReferenceLookup[newShape] = newPoint;
    }

    public void UpdateShape([NotNull] IShape newShape, int newX, int newY, int newZIndex)
    {
      var newPoint = new Point3D(newX, newY, newZIndex);
      UpdateShape(newShape, newPoint);
    }

    public void UpdateScreen()
    {
      foreach (var (shape, point3D) in ReferenceLookup)
      {
        if (shape.Updated)
        {
          Erase(shape);
          Draw(shape);

          shape.Updated = false;
        }
      }
    }

    public static void Erase(IShape shape)
    {
      if (shape is null)
      {
        throw new ArgumentNullException(nameof(shape), Exceptions.Argument_IsNull);
      }

      foreach (var (point, _) in shape.Glyphs)
      {
        Console.SetCursorPosition(shape.Position.X + point.X, shape.Position.Y + point.Y);
        Console.Write(' ');
      }

      Console.ForegroundColor = ConsoleColor.White;
    }

    public static void Draw(IShape shape)
    {
      if (shape is null)
      {
        throw new ArgumentNullException(nameof(shape), Exceptions.Argument_IsNull);
      }

      foreach (var (point, glyph) in shape.Glyphs)
      {
        Console.SetCursorPosition(shape.Position.X + point.X, shape.Position.Y + point.Y);
        Console.ForegroundColor = glyph.Color;
        Console.Write(glyph.Character);
      }

      Console.ForegroundColor = ConsoleColor.White;
    }
  }
}