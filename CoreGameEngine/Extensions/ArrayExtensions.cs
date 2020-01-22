using System.Collections.Generic;
using System.Linq;

namespace CoreGameEngine.Extensions
{
  public static class ArrayExtensions
  {
    public static int Range(this IEnumerable<int> enumerable)
    {
      var ints = enumerable as int[] ?? enumerable.ToArray();
      return ints.Max() - ints.Min();
    }
  }
}