// Decompiled with JetBrains decompiler
// Type: Efx.Core.Events.CancelEventArgs`1
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System.ComponentModel;

namespace Efx.Core.Events
{
  public class CancelEventArgs<T> : CancelEventArgs, IEventArgs<T>
  {
    public CancelEventArgs(T pValue)
      : this(pValue, false)
    {
    }

    public CancelEventArgs(T pValue, bool pCancel)
      : base(pCancel)
    {
      this.Value = pValue;
    }

    public T Value { get; private set; }
  }
}
