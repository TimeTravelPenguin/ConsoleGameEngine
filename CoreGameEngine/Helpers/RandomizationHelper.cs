﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoreGameEngine.Extensions;
using CoreGameEngine.Resources;

namespace CoreGameEngine.Helpers
{
  /// <summary>
  ///   Helpful methods to perform random operations
  /// </summary>
  public static class RandomizationHelper
  {
    private static readonly Random Random = new Random();
    private static readonly object SyncLock = new object();

    public static double RandRange(double min, double max)
    {
      min.SwapIfGreaterThan(ref max);

      Func<double> action = () => Random.NextDouble() * (max - min) + min;

      return action.InvokeInSyncLock();
    }

    public static int RandRange(int min, int max)
    {
      min.SwapIfGreaterThan(ref max);

      Func<int> action = () => Random.Next(min, max);

      return action.InvokeInSyncLock();
    }

    private static T InvokeInSyncLock<T>(this Func<T> action)
    {
      lock (SyncLock)
      {
        return action.Invoke();
      }
    }

    public static T RandomIn<T>(this IEnumerable<T> collection)
    {
      if (collection is null)
      {
        throw new ArgumentNullException(nameof(collection));
      }

      var enumerable = collection.ToList();

      if (!enumerable.Any())
      {
        throw new ArgumentException(Exceptions.Collection_CannotBeEmpty, nameof(collection));
      }

      return enumerable[RandRange(0, enumerable.Count)];
    }

    public static T RandomIn<T>(this T[] collection)
    {
      if (collection is null)
      {
        throw new ArgumentNullException(nameof(collection));
      }

      if (!collection.Any())
      {
        throw new ArgumentException(Exceptions.Collection_CannotBeEmpty, nameof(collection));
      }

      return collection[RandRange(0, collection.Length)];
    }
  }
}