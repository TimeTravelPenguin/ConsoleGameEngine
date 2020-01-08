using System;
using System.Collections.Generic;
using ConsoleGameEngine.Extensions;

namespace ConsoleGameEngine.Helpers
{
  public static class RandomHelper
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

    public static T RandomIn<T>(this IList<T> collection)
    {
      return collection[RandRange(0, collection.Count)];
    }

    public static T RandomIn<T>(this T[] collection)
    {
      return collection[RandRange(0, collection.Length)];
    }
  }
}