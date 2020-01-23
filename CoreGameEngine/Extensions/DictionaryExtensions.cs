using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using CoreGameEngine.Resources;

namespace CoreGameEngine.Extensions
{
  public static class DictionaryExtensions
  {
    public static void UpdateKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
      TKey fromKey, TKey toKey)
    {
      if (dictionary is null)
      {
        throw new ArgumentNullException(nameof(dictionary), Exceptions.ArgumentIsNull);
      }

      if (dictionary.TryGetValue(fromKey, out var value))
      {
        dictionary.Remove(fromKey);
        dictionary.Add(toKey, value);
      }
      else
      {
        throw new InvalidOperationException(Exceptions.Dictionary_InvalidKey);
      }
    }

    public static (Point, T)[][] PointDictionaryToArray<T>([NotNull] this IDictionary<Point, T> dictionary)
      where T : class
    {
      if (dictionary is null)
      {
        throw new ArgumentNullException(nameof(dictionary), Exceptions.ArgumentIsNull);
      }

      if (dictionary.Values.Contains(null))
      {
        throw new NullReferenceException(Exceptions.Dictionary_CannotContainNullValues);
      }

      var minX = dictionary.Keys.Select(point => point.X).Min();
      var maxX = dictionary.Keys.Select(point => point.X).Max();
      var minY = dictionary.Keys.Select(point => point.Y).Min();
      var maxY = dictionary.Keys.Select(point => point.Y).Max();

      var matrixArr = new (Point, T)[maxY - minY + 1][];

      for (var y = minY; y <= maxY; y++)
      {
        for (var x = minX; x <= maxX; x++)
        {
          var p = new Point(x, y);
          var value = dictionary.ContainsKey(p) ? dictionary[p] : null;

          matrixArr[y][x] = (p, value);
        }
      }

      return matrixArr;
    }
  }
}