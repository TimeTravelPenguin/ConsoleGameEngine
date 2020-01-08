using System.Collections.Generic;
using System.Linq;

namespace ConsoleGameEngine.Extensions
{
  public static class IntegerExtensions
  {
    public static bool IsEven(this int integer)
    {
      return integer % 2 == 0;
    }

    public static bool IsEven(this IEnumerable<int> integer)
    {
      return integer.All(i => i.IsEven());
    }
  }
}