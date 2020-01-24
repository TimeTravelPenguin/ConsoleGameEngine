using System;
using System.Collections.Generic;
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
  }
}