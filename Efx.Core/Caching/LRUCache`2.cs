// Decompiled with JetBrains decompiler
// Type: Efx.Core.Caching.LRUCache`2
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;

namespace Efx.Core.Caching
{
  public sealed class LRUCache<TKey, TValue> : ICache<TKey, TValue>
  {
    private const int MAX_SIZE = 3000;
    private readonly LinkedList<LRUCache<TKey, TValue>.CacheEntry> _cacheEntries;
    private readonly IDictionary<TKey, LinkedListNode<LRUCache<TKey, TValue>.CacheEntry>> _keyToCacheEntry;
    private readonly int _maxSize;
    private readonly object _mutex;

    public LRUCache()
      : this(3000)
    {
    }

    public LRUCache(int maxSize)
    {
      this._maxSize = maxSize;
      this._cacheEntries = new LinkedList<LRUCache<TKey, TValue>.CacheEntry>();
      this._keyToCacheEntry = (IDictionary<TKey, LinkedListNode<LRUCache<TKey, TValue>.CacheEntry>>) new Dictionary<TKey, LinkedListNode<LRUCache<TKey, TValue>.CacheEntry>>();
      this._mutex = (object) this;
    }

    public void Clear()
    {
      lock (this._mutex)
      {
        this._keyToCacheEntry.Clear();
        this._cacheEntries.Clear();
      }
    }

    public bool Contains(TKey key)
    {
      if (this._keyToCacheEntry.ContainsKey(key))
        return true;
      lock (this._mutex)
        return this._keyToCacheEntry.ContainsKey(key);
    }

    public TValue Get(TKey key)
    {
      LinkedListNode<LRUCache<TKey, TValue>.CacheEntry> node;
      if (!this._keyToCacheEntry.TryGetValue(key, out node))
        return default (TValue);
      lock (this._mutex)
      {
        if (!this._keyToCacheEntry.TryGetValue(key, out node))
          return default (TValue);
        this._cacheEntries.Remove(node);
        this._cacheEntries.AddFirst(node);
        return node.Value.CacheValue;
      }
    }

    public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
    {
      LinkedListNode<LRUCache<TKey, TValue>.CacheEntry> linkedListNode;
      if (!this._keyToCacheEntry.TryGetValue(key, out linkedListNode))
      {
        lock (this._mutex)
        {
          if (!this._keyToCacheEntry.TryGetValue(key, out linkedListNode))
            this.Set(key, valueFactory(key));
        }
      }
      return this.Get(key);
    }

    public void Remove(TKey key)
    {
      LinkedListNode<LRUCache<TKey, TValue>.CacheEntry> node;
      if (!this._keyToCacheEntry.TryGetValue(key, out node))
        return;
      lock (this._mutex)
      {
        if (!this._keyToCacheEntry.TryGetValue(key, out node))
          return;
        this._cacheEntries.Remove(node);
        this._keyToCacheEntry.Remove(key);
      }
    }

    public void Set(TKey key, TValue value)
    {
      lock (this._mutex)
      {
        if (!object.Equals((object) this.Get(key), (object) default (TValue)))
          return;
        LinkedListNode<LRUCache<TKey, TValue>.CacheEntry> node = new LinkedListNode<LRUCache<TKey, TValue>.CacheEntry>(new LRUCache<TKey, TValue>.CacheEntry(key, value));
        this._cacheEntries.AddFirst(node);
        this._keyToCacheEntry.Add(key, node);
        if (this._cacheEntries.Count <= this._maxSize)
          return;
        this._keyToCacheEntry.Remove(this._cacheEntries.Last.Value.CacheKey);
        this._cacheEntries.RemoveLast();
      }
    }

    private sealed class CacheEntry
    {
      internal readonly TKey CacheKey;
      internal readonly TValue CacheValue;

      internal CacheEntry(TKey k, TValue v)
      {
        this.CacheKey = k;
        this.CacheValue = v;
      }
    }
  }
}
