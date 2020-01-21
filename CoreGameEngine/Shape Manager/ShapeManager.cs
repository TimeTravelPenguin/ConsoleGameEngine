using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CoreGameEngine.Draw;
using CoreGameEngine.Extensions;
using CoreGameEngine.Resources;
using CoreGameEngine.Structs;

namespace CoreGameEngine.Shape_Manager
{
  public class ShapeManager
  {
    public IDictionary<IShape, Point3D> VirtualReverse { get; }
    public IDictionary<Point3D, IShape> VirtualLocations { get; }

    public ShapeManager([NotNull] IDictionary<Point3D, IShape> virtualField,
      [NotNull] IDictionary<IShape, Point3D> shapeLocations)
    {
      VirtualLocations = virtualField ??
                         throw new ArgumentNullException(nameof(virtualField), Exceptions.Argument_IsNull);

      VirtualReverse = shapeLocations ??
                       throw new ArgumentNullException(nameof(shapeLocations), Exceptions.Argument_IsNull);
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

      if (VirtualLocations.ContainsKey(shape.Position))
      {
        throw new InvalidOperationException(Exceptions.Dictionary_KeyAlreadyExists);
      }

      if (VirtualReverse.ContainsKey(shape))
      {
        throw new InvalidOperationException(Exceptions.Dictionary_KeyAlreadyExists);
      }

      VirtualLocations.Add(shape.Position, shape);
      VirtualReverse.Add(shape, shape.Position);
    }

    public void Remove(Point3D key)
    {
      if (!VirtualLocations.ContainsKey(key))
      {
        throw new InvalidOperationException(Exceptions.Dictionary_InvalidKey);
      }

      VirtualReverse.Remove(VirtualLocations[key]);
      VirtualLocations.Remove(key);
    }

    public void UpdateShape([NotNull] IShape newShape)
    {
      if (newShape is null)
      {
        throw new ArgumentNullException(nameof(newShape), Exceptions.Argument_IsNull);
      }

      if (!VirtualReverse.ContainsKey(newShape))
      {
        throw new InvalidOperationException(Exceptions.Dictionary_InvalidKey);
      }

      VirtualLocations.UpdateKey(newShape.Position, VirtualReverse[newShape]);
      VirtualReverse[newShape] = newShape.Position;
    }
  }
}