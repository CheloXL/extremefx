// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.StringMatcher.LongestCommonPrefixMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.StringMatcher
{
  internal static class LongestCommonPrefixMatcher
  {
    private static int LongestCommonPrefixLength(string stringOne, string stringTwo)
    {
      int num = Math.Min(stringOne.Length, stringTwo.Length);
      int index = 0;
      while (index < num && (int) stringOne[index] == (int) stringTwo[index])
        ++index;
      return index;
    }

    public static string Match(IEnumerable<string> candidates, string needle, int tolerance)
    {
      string str1 = (string) null;
      int length = needle.Length;
      IList<string> stringList = (IList<string>) new List<string>(candidates);
      int index1 = -1;
      int num1 = -1;
      int num2 = 0;
      int num3 = stringList.Count - 1;
      while (num2 <= num3 && num1 < length)
      {
        int index2 = (num2 + num3) / 2;
        string str2 = stringList[index2];
        int num4 = LongestCommonPrefixMatcher.LongestCommonPrefixLength(needle, str2);
        if (num4 > num1)
        {
          index1 = index2;
          num1 = num4;
        }
        int num5 = string.Compare(str2, needle, StringComparison.Ordinal);
        if (num5 < 0)
          num2 = index2 + 1;
        else if (num5 > 0)
          num3 = index2 - 1;
        else
          break;
      }
      if (num1 >= tolerance)
      {
        int num4 = num1;
        while (index1 > 0 && num4 == num1)
        {
          string stringTwo = stringList[index1 - 1];
          num4 = LongestCommonPrefixMatcher.LongestCommonPrefixLength(needle, stringTwo);
          if (num4 == num1)
            --index1;
        }
        str1 = stringList[index1];
      }
      return str1;
    }
  }
}
