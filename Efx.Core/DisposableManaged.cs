// Decompiled with JetBrains decompiler
// Type: Efx.Core.DisposableManaged
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Threading;

namespace Efx.Core
{
  public abstract class DisposableManaged : IDisposable
  {
    private int _disposed;

    protected bool Disposed
    {
      get
      {
        return this._disposed != 0;
      }
    }

    public void Dispose()
    {
      if (Interlocked.CompareExchange(ref this._disposed, 1, 0) != 0)
        return;
      GC.SuppressFinalize((object) this);
      this.DisposeManagedResources();
    }

    protected abstract void DisposeManagedResources();
  }
}
