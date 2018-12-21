// Decompiled with JetBrains decompiler
// Type: Efx.Core.Debug.InvariantStringArray
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections;

namespace Efx.Core.Debug
{
  internal static class InvariantStringArray
  {
    private static IComparer invariantComparer
    {
      get
      {
        return (IComparer) Comparer.DefaultInvariant;
      }
    }

    public static void Sort(string[] pKeys, int pIndex, int pLength)
    {
      InvariantStringArray.sort(pKeys, (Array) null, pIndex, pLength);
    }

    private static void sort(string[] pKeys, Array pItems, int pIndex, int pLength)
    {
      Array.Sort((Array) pKeys, pItems, pIndex, pLength, InvariantStringArray.invariantComparer);
    }
  }
}
