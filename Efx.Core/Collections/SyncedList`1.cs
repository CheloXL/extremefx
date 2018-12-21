// Decompiled with JetBrains decompiler
// Type: Efx.Core.Collections.SyncedList`1
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Efx.Core.Collections
{
  public sealed class SyncedList<T> : IEnumerable<T>, IEnumerable, IList<T>, ICollection<T>, IList, IEnumerablePlus<T>, ICollection
  {
    private readonly LockSlim _lock = new LockSlim(LockRecursionPolicy.NoRecursion);
    private readonly List<T> _list;

    public SyncedList()
    {
      this._list = new List<T>();
    }

    public SyncedList(IEnumerable<T> enumerable)
    {
      this._list = new List<T>(enumerable);
    }

    public SyncedList(int capacity)
    {
      this._list = new List<T>(capacity);
    }

    public IEnumerator<T> GetEnumerator()
    {
      using (this._lock.ReadLock)
        return (IEnumerator<T>) new List<T>((IEnumerable<T>) this._list).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public void Add(T item)
    {
      using (this._lock.WriteLock)
        this._list.Add(item);
    }

    public int Add(object value)
    {
      this.Add((T) value);
      return this._list.Count - 1;
    }

    public bool Contains(object value)
    {
      return this.Contains((T) value);
    }

    public void Clear()
    {
      using (this._lock.WriteLock)
        this._list.Clear();
    }

    public int IndexOf(object value)
    {
      return this.IndexOf((T) value);
    }

    public void Insert(int index, object value)
    {
      this.Insert(index, (T) value);
    }

    public void Remove(object value)
    {
      this.Remove((T) value);
    }

    public bool Contains(T item)
    {
      using (this._lock.ReadLock)
        return this._list.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      using (this._lock.ReadLock)
      {
        for (int index = 0; index < this.Count; ++index)
          array[index + arrayIndex] = this._list[index];
      }
    }

    public bool Remove(T item)
    {
      using (this._lock.WriteLock)
        return this._list.Remove(item);
    }

    public void CopyTo(Array array, int index)
    {
      throw new NotImplementedException();
    }

    public int Count
    {
      get
      {
        return this._list.Count;
      }
    }

    public object SyncRoot
    {
      get
      {
        return (object) null;
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return true;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public bool IsFixedSize
    {
      get
      {
        return false;
      }
    }

    public int IndexOf(T item)
    {
      using (this._lock.ReadLock)
        return this._list.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
      using (this._lock.WriteLock)
        this._list.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
      using (this._lock.WriteLock)
        this._list.RemoveAt(index);
    }

    object IList.this[int index]
    {
      get
      {
        using (this._lock.ReadLock)
          return (object) this._list[index];
      }
      set
      {
        using (this._lock.WriteLock)
          this._list[index] = (T) value;
      }
    }

    public T this[int index]
    {
      get
      {
        using (this._lock.ReadLock)
          return this._list[index];
      }
      set
      {
        using (this._lock.WriteLock)
          this._list[index] = value;
      }
    }
  }
}
