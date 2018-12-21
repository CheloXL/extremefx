// Decompiled with JetBrains decompiler
// Type: Efx.Core.Events.ChangingData`1
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;

namespace Efx.Core.Events
{
  public class ChangingData<T> : EventArgs, IEventArgs<T>
  {
    public ChangingData(T pOldData, T pNewData)
    {
      this.OldData = pOldData;
      this.NewData = pNewData;
    }

    public T OldData { get; private set; }

    public T NewData { get; private set; }

    public T Value
    {
      get
      {
        return this.NewData;
      }
    }
  }
}
