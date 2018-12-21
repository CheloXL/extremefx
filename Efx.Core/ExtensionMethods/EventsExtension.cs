// Decompiled with JetBrains decompiler
// Type: Efx.Core.ExtensionMethods.EventsExtension
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;

namespace Efx.Core.ExtensionMethods
{
  public static class EventsExtension
  {
    public static void Raise<TEventArgs>(
      this EventHandler<TEventArgs> pEventHandler,
      object pSender,
      TEventArgs pArgs)
      where TEventArgs : EventArgs
    {
      if (pEventHandler == null)
        return;
      pEventHandler(pSender, pArgs);
    }
  }
}
