// Decompiled with JetBrains decompiler
// Type: Efx.Core.LockSlim
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Diagnostics;
using System.Threading;

namespace Efx.Core
{
  [DebuggerStepThrough]
  public sealed class LockSlim : IDisposable
  {
    private readonly ReaderWriterLockSlim _lockSlim;

    public LockSlim(LockRecursionPolicy pLockRecursionPolicy = LockRecursionPolicy.NoRecursion)
    {
      this._lockSlim = new ReaderWriterLockSlim(pLockRecursionPolicy);
    }

    public ReadLockSlim ReadLock
    {
      get
      {
        return new ReadLockSlim(this._lockSlim);
      }
    }

    public ReadOnlyLockSlim ReadOnlyLock
    {
      get
      {
        return new ReadOnlyLockSlim(this._lockSlim);
      }
    }

    public WriteLockSlim WriteLock
    {
      get
      {
        return new WriteLockSlim(this._lockSlim);
      }
    }

    public void Dispose()
    {
      this._lockSlim.Dispose();
    }
  }
}
