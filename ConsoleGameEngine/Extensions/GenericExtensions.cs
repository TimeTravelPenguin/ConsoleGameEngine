using System;

namespace ConsoleGameEngine.Extensions
{
  public static class GenericExtensions
  {
    public static void Swap<T>(this ref T first, ref T second) where T : struct
    {
      var temp = second;
      second = first;
      first = temp;
    }

    public static void SwapIfGreaterThan<T>(this ref T first, ref T second) where T : struct, IComparable
    {
      if (first.IsGreaterThan(second))
      {
        first.Swap(ref second);
      }
    }

    public static bool IsGreaterThan<T>(this T first, T second) where T : IComparable
    {
      return first.CompareTo(second) > 0;
    }
  }
}