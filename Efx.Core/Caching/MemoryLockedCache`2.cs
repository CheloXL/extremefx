// Decompiled with JetBrains decompiler
// Type: Efx.Core.Caching.MemoryLockedCache`2
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;

namespace Efx.Core.Caching
{
  public sealed class MemoryLockedCache<TKey, TValue> : ICache<TKey, TValue>
  {
    private readonly object _lock = new object();
    private readonly Dictionary<TKey, TValue> _dictionary;

    public MemoryLockedCache()
    {
      this._dictionary = new Dictionary<TKey, TValue>();
    }

    public void Set(TKey key, TValue value)
    {
      lock (this._lock)
        this._dictionary[key] = value;
    }

    public TValue Get(TKey key)
    {
      TValue obj;
      if (!this._dictionary.TryGetValue(key, out obj))
        return default (TValue);
      return obj;
    }

    public bool Contains(TKey key)
    {
      return this._dictionary.ContainsKey(key);
    }

    public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueCreator)
    {
      TValue obj;
      if (this._dictionary.TryGetValue(key, out obj))
        return obj;
      lock (this._lock)
      {
        if (this._dictionary.TryGetValue(key, out obj))
          return obj;
        obj = valueCreator(key);
        this._dictionary.Add(key, obj);
        return obj;
      }
    }

    public void Remove(TKey key)
    {
      lock (this._lock)
        this._dictionary.Remove(key);
    }

    public void Clear()
    {
      lock (this._lock)
        this._dictionary.Clear();
    }
  }
}
