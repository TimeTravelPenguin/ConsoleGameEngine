using System;
using System.Drawing;
using CoreGameEngine.Draw;

namespace TetrisDemo
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var myShape = Shape.New("c red r1 c white r1 c red r1 c white r1 c red r1 c blue r4 c white r1 c blue r5" +
                              "n 0 1 c red r5 c blue r7 c white r1 c blue r2" +
                              "n 0 2 c red r1 c white r1 c red r1 c white r1 c red r1 c blue r2 c white r1 c blue r3 c white r1 c blue r3" +
                              "n 0 3 c blue r2 c white r1 c blue r12" +
                              "n 0 4 r9 c white r1 c blue r5",
        'X',
        Point.Empty);

      myShape.Draw();

      Console.ReadKey(true);
    }
  }
}