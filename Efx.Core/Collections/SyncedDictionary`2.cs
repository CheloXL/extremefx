// Decompiled with JetBrains decompiler
// Type: Efx.Core.Collections.SyncedDictionary`2
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.ExtensionMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Efx.Core.Collections
{
  public sealed class SyncedDictionary<TKey, TValue> : IEnumerable, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerablePlus<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>
  {
    private readonly LockSlim _lock = new LockSlim(LockRecursionPolicy.NoRecursion);
    private readonly Dictionary<TKey, TValue> _dictionary;

    public SyncedDictionary()
    {
      this._dictionary = new Dictionary<TKey, TValue>();
    }

    public SyncedDictionary(IDictionary<TKey, TValue> dictionary)
    {
      this._dictionary = new Dictionary<TKey, TValue>(dictionary);
    }

    public SyncedDictionary(
      IDictionary<TKey, TValue> dictionary,
      IEqualityComparer<TKey> keyComparer)
    {
      this._dictionary = new Dictionary<TKey, TValue>(dictionary, keyComparer);
    }

    public SyncedDictionary(IEqualityComparer<TKey> keyComparer)
    {
      this._dictionary = new Dictionary<TKey, TValue>(keyComparer);
    }

    public SyncedDictionary(int capacity)
    {
      this._dictionary = new Dictionary<TKey, TValue>(capacity);
    }

    public SyncedDictionary(int capacity, IEqualityComparer<TKey> keyComparer)
    {
      this._dictionary = new Dictionary<TKey, TValue>(capacity, keyComparer);
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
      using (this._lock.WriteLock)
        this._dictionary.Add(item.Key, item.Value);
    }

    public void Clear()
    {
      using (this._lock.WriteLock)
        this._dictionary.Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
      if (item.IsNullOrDefault<KeyValuePair<TKey, TValue>>())
        return false;
      using (this._lock.ReadLock)
        return !item.Value.IsNullOrDefault<TValue>() && this._dictionary.Contains<KeyValuePair<TKey, TValue>>(item);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      int num = 0;
      using (this._lock.ReadLock)
      {
        foreach (KeyValuePair<TKey, TValue> keyValuePair in this._dictionary)
        {
          array[num + arrayIndex] = new KeyValuePair<TKey, TValue>(keyValuePair.Key, keyValuePair.Value);
          ++num;
        }
      }
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
      using (this._lock.WriteLock)
        return this._dictionary.Remove(item.Key);
    }

    public int Count
    {
      get
      {
        return this._dictionary.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public bool ContainsKey(TKey key)
    {
      using (this._lock.ReadLock)
        return this._dictionary.ContainsKey(key);
    }

    public void Add(TKey key, TValue value)
    {
      using (this._lock.WriteLock)
        this._dictionary.Add(key, value);
    }

    public bool Remove(TKey key)
    {
      using (this._lock.WriteLock)
        return this._dictionary.Remove(key);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      using (this._lock.ReadLock)
        return this._dictionary.TryGetValue(key, out value);
    }

    public TValue this[TKey key]
    {
      get
      {
        using (this._lock.ReadLock)
          return this._dictionary[key];
      }
      set
      {
        using (this._lock.WriteLock)
          this._dictionary[key] = value;
      }
    }

    public ICollection<TKey> Keys
    {
      get
      {
        using (this._lock.ReadLock)
          return (ICollection<TKey>) this._dictionary.Keys;
      }
    }

    public ICollection<TValue> Values
    {
      get
      {
        using (this._lock.ReadLock)
          return (ICollection<TValue>) this._dictionary.Values;
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<TKey, TValue>>) new SyncedDictionary<TKey, TValue>.DicEnumerator((ICollection<KeyValuePair<TKey, TValue>>) this._dictionary);
    }

    private sealed class DicEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
    {
      private readonly IEnumerator _enumerator;

      public DicEnumerator(ICollection<KeyValuePair<TKey, TValue>> pList)
      {
        KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[pList.Count];
        pList.CopyTo(array, 0);
        this._enumerator = array.GetEnumerator();
      }

      public void Dispose()
      {
      }

      public bool MoveNext()
      {
        return this._enumerator.MoveNext();
      }

      public void Reset()
      {
        this._enumerator.Reset();
      }

      public KeyValuePair<TKey, TValue> Current
      {
        get
        {
          return (KeyValuePair<TKey, TValue>) this._enumerator.Current;
        }
      }

      object IEnumerator.Current
      {
        get
        {
          return this._enumerator.Current;
        }
      }
    }
  }
}
