using System;
using System.Collections.Generic;
using CoreGameEngine.Resources;

namespace CoreGameEngine.Factory
{
  public abstract class FactoryBase<TKey, TValue> : IFactory<TKey, TValue>
  {
    private readonly IDictionary<TKey, Func<TValue>> _registry = new Dictionary<TKey, Func<TValue>>();

    public void Register(TKey key, Func<TValue> value)
    {
      if (_registry.ContainsKey(key))
      {
        throw new ArgumentException(Exceptions.Factory_KeyAlreadyExists, nameof(key));
      }

      _registry.Add(key, value);
    }

    public TValue Create(TKey key)
    {
      if (!_registry.ContainsKey(key))
      {
        throw new ArgumentException(Exceptions.Factory_InvalidKey, nameof(key));
      }

      return _registry[key].Invoke();
    }

    public bool ContainsKey(TKey key)
    {
      return _registry.ContainsKey(key);
    }
  }
}