// Decompiled with JetBrains decompiler
// Type: Efx.Core.WriteLockSlim
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Diagnostics;
using System.Threading;

namespace Efx.Core
{
  [DebuggerStepThrough]
  public sealed class WriteLockSlim : IDisposable
  {
    private readonly ReaderWriterLockSlim _lockSlim;

    internal WriteLockSlim(ReaderWriterLockSlim pLockSlim)
    {
      if (pLockSlim == null || pLockSlim.IsWriteLockHeld)
        return;
      this._lockSlim = pLockSlim;
      bool flag = false;
      while (!flag)
        flag = this._lockSlim.TryEnterWriteLock(1);
    }

    public void Dispose()
    {
      if (this._lockSlim == null || !this._lockSlim.IsWriteLockHeld)
        return;
      this._lockSlim.ExitWriteLock();
    }
  }
}
