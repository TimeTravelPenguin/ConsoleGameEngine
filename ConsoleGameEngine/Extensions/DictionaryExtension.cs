using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleGameEngine.Extensions
{
  public static class DictionaryExtension
  {
    public static void DictAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
      if (dictionary.ContainsKey(key))
      {
        throw new ArgumentException("Key already exists", nameof(key));
      }

      dictionary.Add(key, value);
    }

    public static void DictAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
      KeyValuePair<TKey, TValue> keyValuePair)
    {
      if (dictionary.Contains(keyValuePair))
      {
        throw new ArgumentException("KeyValuePair already exists", nameof(keyValuePair));
      }

      dictionary.Add(keyValuePair);
    }

    public static void DictRemove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
    {
      if (!dictionary.ContainsKey(key))
      {
        throw new ArgumentException("Key does not exist", nameof(key));
      }

      dictionary.Remove(key);
    }

    public static void DictRemove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
      where TValue : class
    {
      if (!dictionary.Values.Contains(value))
      {
        throw new ArgumentException("Value does not exist", nameof(value));
      }


      foreach (var item in dictionary.Where(
        kvp => EqualityComparer<TValue>.Default.Equals(kvp.Value, value)).ToList())
      {
        dictionary.Remove(item.Key);
      }
    }
  }
}