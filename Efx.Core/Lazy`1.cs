// Decompiled with JetBrains decompiler
// Type: Efx.Core.Lazy`1
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Threading;

namespace Efx.Core
{
  public class Lazy<T> : DisposableManaged
  {
    private readonly ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();
    private readonly Func<T> _delegate;
    private readonly bool _isThreadSafe;
    private Lazy<T>.Boxed _value;

    public Lazy(Func<T> pFunc, bool pIsThreadSafe = false)
    {
      this._isThreadSafe = pIsThreadSafe;
      this._delegate = pFunc;
    }

    public void Invalidate()
    {
      if (this._isThreadSafe)
      {
        this._value = (Lazy<T>.Boxed) null;
      }
      else
      {
        try
        {
          this._readWriteLock.EnterWriteLock();
          this._value = (Lazy<T>.Boxed) null;
        }
        finally
        {
          this._readWriteLock.ExitWriteLock();
        }
      }
    }

    public T Value
    {
      get
      {
        if (this._value != null)
          return this._value._mValue;
        if (this._isThreadSafe)
        {
          this._value = new Lazy<T>.Boxed(this._delegate());
          return this._value._mValue;
        }
        try
        {
          this._readWriteLock.EnterReadLock();
          if (this._value != null)
            return this._value._mValue;
        }
        finally
        {
          this._readWriteLock.ExitReadLock();
        }
        try
        {
          this._readWriteLock.EnterWriteLock();
          this._value = new Lazy<T>.Boxed(this._delegate());
          return this._value._mValue;
        }
        finally
        {
          this._readWriteLock.ExitWriteLock();
        }
      }
    }

    protected override void DisposeManagedResources()
    {
      this._readWriteLock.Dispose();
    }

    [Serializable]
    private sealed class Boxed
    {
      internal readonly T _mValue;

      internal Boxed(T pValue)
      {
        this._mValue = pValue;
      }
    }
  }
}
