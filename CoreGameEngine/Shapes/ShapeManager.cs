using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CoreGameEngine.Extensions;
using CoreGameEngine.Resources;
using CoreGameEngine.Structs;

namespace CoreGameEngine.Shapes
{
  public class ShapeManager
  {
    private IDictionary<Point3D, IShape> History { get; }
    private IDictionary<IShape, Point3D> ReferenceLookup { get; }
    public IDictionary<Point3D, IShape> ShapeLocations { get; }

    private ShapeManager([NotNull] IDictionary<Point3D, IShape> shapeLocations,
      [NotNull] IDictionary<IShape, Point3D> referenceLookup)
    {
      ShapeLocations = shapeLocations ??
                       throw new ArgumentNullException(nameof(shapeLocations), Exceptions.ArgumentIsNull);

      ReferenceLookup = referenceLookup ??
                        throw new ArgumentNullException(nameof(referenceLookup), Exceptions.ArgumentIsNull);

      History = new Dictionary<Point3D, IShape>();
      foreach (var (point, shape) in shapeLocations)
      {
        var newShape = shape.CreateNewShapeFrom<IShape>();
        History.Add(point, newShape);
      }
    }

    public static ShapeManager NewManager()
    {
      return new ShapeManager(new Dictionary<Point3D, IShape>(), new Dictionary<IShape, Point3D>());
    }

    public void Add<T>([NotNull] IShape shape) where T : IShape
    {
      if (shape is null)
      {
        throw new ArgumentNullException(nameof(shape), Exceptions.ArgumentIsNull);
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
      History.Add(shape.Position, shape.CreateNewShapeFrom<T>());
    }

    public void Remove(Point3D key)
    {
      if (!ShapeLocations.ContainsKey(key))
      {
        throw new InvalidOperationException(Exceptions.Dictionary_InvalidKey);
      }

      ReferenceLookup.Remove(ShapeLocations[key]);
      ShapeLocations.Remove(key);
      History.Remove(key);
    }

    public void UpdateShape(IShape shape)
    {
      if (shape is null)
      {
        throw new ArgumentNullException(nameof(shape), Exceptions.ArgumentIsNull);
      }

      if (!ReferenceLookup.ContainsKey(shape))
      {
        throw new InvalidOperationException(Exceptions.Dictionary_InvalidKey);
      }

      History[ReferenceLookup[shape]] = shape.CreateNewShapeFrom(shape.GetType());
      ShapeLocations.UpdateKey(ReferenceLookup[shape], shape.Position);
      ReferenceLookup[ShapeLocations[shape.Position]] = shape.Position;

      shape.Updated = false;
    }

    public void UpdateScreen()
    {
      var shapes = ReferenceLookup
        .Select(r => r.Value)
        .OrderBy(v => v.Z);

      foreach (var shape in shapes)
      {
        var current = ShapeLocations[shape];
        if (current.Updated)
        {
          Erase(current);
          Draw(current);
          UpdateShape(current);
        }
      }
    }


    public void Erase(IShape shape)
    {
      if (shape is null)
      {
        throw new ArgumentNullException(nameof(shape), Exceptions.ArgumentIsNull);
      }

      var oldShape = History[ReferenceLookup[shape]];

      foreach (var (point, _) in oldShape.Glyphs)
      {
        Write(' ', shape.Position.Add(point));
      }
    }

    public static void Draw(IShape shape)
    {
      if (shape is null)
      {
        throw new ArgumentNullException(nameof(shape), Exceptions.ArgumentIsNull);
      }

      foreach (var (point, glyph) in shape.Glyphs)
      {
        Write(glyph.Character, shape.Position.Add(point), glyph.Color);
      }
    }

    private static void Write(char character, Point3D position, ConsoleColor color = ConsoleColor.White)
    {
      Console.SetCursorPosition(position.X, position.Y);
      Console.ForegroundColor = color;
      Console.Write(character);

      Console.ForegroundColor = ConsoleColor.White;
    }
  }
}