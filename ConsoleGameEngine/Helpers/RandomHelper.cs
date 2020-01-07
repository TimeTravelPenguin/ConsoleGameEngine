using System;

namespace ConsoleGameEngine.Helpers
{
  public static class RandomHelper
  {
    public static Random Random = new Random();
    private static object _syncLock = new object();

    // TODO MOOOORRREEEE
    public static int Rand(int min, int max)
    {
      return Random.Next(min, max);
    }
  }
}