// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Predicates
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;

namespace Efx.Web.BrowserCaps
{
  internal static class Predicates
  {
    public static Predicate<string> ContainedIn(string target)
    {
      return (Predicate<string>) (argument => target.Contains(argument));
    }

    public static Predicate<string> ContainedInCaseInsensitive(string target)
    {
      return (Predicate<string>) (argument => target.ToLower().Contains(argument.ToLower()));
    }

    public static Predicate<string> PrefixOf(string input)
    {
      return (Predicate<string>) (argument => input.StartsWith(argument));
    }

    public static Predicate<string> StartsWith(string prefix)
    {
      return (Predicate<string>) (argument => argument.StartsWith(prefix));
    }
  }
}
