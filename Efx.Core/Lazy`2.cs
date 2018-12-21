// Decompiled with JetBrains decompiler
// Type: Efx.Core.Lazy`2
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;
using System.Threading;

namespace Efx.Core
{
  public class Lazy<TKey, TValue>
  {
    private readonly ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();
    private readonly Dictionary<TKey, TValue> _values = new Dictionary<TKey, TValue>();
    private readonly Func<TValue> _delegate;
    private readonly bool _isThreadSafe;

    public Lazy(Func<TValue> pFunc, bool pIsThreadSafe = false)
    {
      this._isThreadSafe = pIsThreadSafe;
      this._delegate = pFunc;
    }

    public void Invalidate(TKey pIndexer)
    {
      if ((object) pIndexer == null)
        return;
      if (this._isThreadSafe)
      {
        this._values.Remove(pIndexer);
      }
      else
      {
        this._readWriteLock.EnterWriteLock();
        try
        {
          this._values.Remove(pIndexer);
        }
        finally
        {
          this._readWriteLock.ExitWriteLock();
        }
      }
    }

    public TValue this[TKey pIndexer]
    {
      get
      {
        if ((object) pIndexer == null)
          throw new ArgumentNullException(nameof (pIndexer));
        TValue obj1;
        if (this._values.TryGetValue(pIndexer, out obj1))
          return obj1;
        if (this._isThreadSafe)
        {
          TValue obj2 = this._delegate();
          this._values[pIndexer] = obj2;
          return obj2;
        }
        this._readWriteLock.EnterUpgradeableReadLock();
        try
        {
          TValue obj2;
          if (this._values.TryGetValue(pIndexer, out obj2))
            return obj2;
          this._readWriteLock.EnterWriteLock();
          try
          {
            TValue obj3 = this._delegate();
            this._values.Add(pIndexer, obj3);
            return obj3;
          }
          finally
          {
            this._readWriteLock.ExitWriteLock();
          }
        }
        finally
        {
          this._readWriteLock.ExitUpgradeableReadLock();
        }
      }
    }
  }
}
