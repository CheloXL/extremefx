// Decompiled with JetBrains decompiler
// Type: Efx.Core.Data.Pager
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;

namespace Efx.Core.Data
{
  public static class Pager
  {
    public static PagerData GetPagerData(int pTotalItems, int pMaxItems, int pCurrentPage)
    {
      double num1 = Math.Floor((double) pTotalItems / (double) pMaxItems);
      double num2 = ((double) pTotalItems / (double) pMaxItems - num1) * (double) pMaxItems;
      double num3 = Math.Round((double) pMaxItems / 2.0) + 1.0;
      double num4 = num2 < num3 ? num1 : num1 + 1.0;
      double num5 = num2 == 0.0 || (double) pCurrentPage != num4 ? (double) pMaxItems : (num2 < num3 ? (double) pMaxItems + num2 : num2);
      int num6 = pMaxItems * (pCurrentPage - 1);
      return new PagerData() { FirstVisibleItem = num6, ItemsPerPage = (int) num5, LastVisibleItem = num6 + (int) num5 };
    }
  }
}
