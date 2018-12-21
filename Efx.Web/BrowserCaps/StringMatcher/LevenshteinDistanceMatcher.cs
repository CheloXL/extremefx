// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.StringMatcher.LevenshteinDistanceMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.StringMatcher
{
  internal static class LevenshteinDistanceMatcher
  {
    private static int GetLevenshteinDistance(string s, string t, int tolerance)
    {
      if (s == null || t == null)
        throw new ArgumentException("Strings must not be null");
      if (tolerance == 0)
        return !s.Equals(t) ? int.MaxValue : 0;
      int length1 = s.Length;
      int length2 = t.Length;
      if (length1 == 0)
        return length2;
      if (length2 == 0)
        return length1;
      int[] numArray1 = new int[length1 + 1];
      int[] numArray2 = new int[length1 + 1];
      for (int index = 0; index <= length1; ++index)
        numArray1[index] = index;
      int[] numArray3 = numArray1;
      for (int index1 = 1; index1 <= length2; ++index1)
      {
        char ch = t[index1 - 1];
        numArray2[0] = index1;
        for (int index2 = 1; index2 <= length1; ++index2)
        {
          int num = (int) s[index2 - 1] == (int) ch ? 0 : 1;
          numArray2[index2] = Math.Min(Math.Min(numArray2[index2 - 1] + 1, numArray1[index2] + 1), numArray1[index2 - 1] + num);
          if (index2 == index1 && numArray2[index2] > tolerance + 3)
            return int.MaxValue;
        }
        numArray1 = numArray2;
        numArray2 = numArray3;
      }
      return numArray1[length1];
    }

    public static string Match(IEnumerable<string> candidates, string needle, int tolerance)
    {
      string str = (string) null;
      int num1 = tolerance;
      int num2 = needle.Length;
      IEnumerator<string> enumerator = candidates.GetEnumerator();
      while (enumerator.MoveNext() && num2 > 0)
      {
        string current = enumerator.Current;
        if (Math.Abs(current.Length - needle.Length) <= tolerance)
        {
          num2 = LevenshteinDistanceMatcher.GetLevenshteinDistance(current, needle, tolerance);
          if (num2 < num1 || num2 == 0)
          {
            num1 = num2;
            str = current;
          }
        }
      }
      return str;
    }
  }
}
