// Decompiled with JetBrains decompiler
// Type: Efx.Core.TimeStamp
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Diagnostics;

namespace Efx.Core
{
  public static class TimeStamp
  {
    private static readonly Stopwatch _stopWatch;

    static TimeStamp()
    {
      if (!Stopwatch.IsHighResolution)
        return;
      TimeStamp._stopWatch = new Stopwatch();
      TimeStamp._stopWatch.Start();
    }

    public static long ElapsedTicks
    {
      get
      {
        if (TimeStamp._stopWatch == null)
          return DateTime.UtcNow.Ticks;
        return TimeStamp._stopWatch.ElapsedTicks;
      }
    }
  }
}
