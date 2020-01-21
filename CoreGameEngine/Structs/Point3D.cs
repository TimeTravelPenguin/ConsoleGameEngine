using System;

namespace CoreGameEngine.Structs
{
  [Serializable]
  public struct Point3D : IEquatable<Point3D>
  {
    public int X { get; }
    public int Y { get; }
    public int Z { get; }

    public Point3D(int x, int y, int z)
    {
      X = x;
      Y = y;
      Z = z;
    }

    public static Point3D Empty => new Point3D(0, 0, 0);

    public void Add(Point3D point)
    {
      this += point;
    }

    public static Point3D Add(Point3D left, Point3D right)
    {
      return new Point3D(unchecked(left.X + right.X), unchecked(left.Y + right.Y), unchecked(left.Z + right.Z));
    }

    public void Subtract(Point3D point)
    {
      this -= point;
    }

    public static Point3D Subtract(Point3D left, Point3D right)
    {
      return new Point3D(unchecked(left.X - right.X), unchecked(left.Y - right.Y), unchecked(left.Z - right.Z));
    }

    public override string ToString()
    {
      return $"{{X={X},Y={Y},Z={Z}}}";
    }

    public bool Equals(Point3D other)
    {
      return this == other;
    }

    public override bool Equals(object obj)
    {
      return obj is Point3D point3D && Equals(point3D);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(X, Y, Z);
    }

    public static Point3D operator +(Point3D left, Point3D right)
    {
      return Add(left, right);
    }

    public static Point3D operator -(Point3D left, Point3D right)
    {
      return Subtract(left, right);
    }

    public static bool operator ==(Point3D left, Point3D right)
    {
      return left.X == right.X &&
             left.Y == right.Y &&
             left.Z == right.Z;
    }

    public static bool operator !=(Point3D left, Point3D right)
    {
      return !(left == right);
    }
  }
}