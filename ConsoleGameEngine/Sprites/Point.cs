namespace ConsoleGameEngine.Sprites
{
  public class Point
  {
    public int X { get; set; }
    public int Y { get; set; }

    public static Point Zero => new Point(0, 0);

    public Point(int x, int y)
    {
      X = x;
      Y = y;
    }
  }
}