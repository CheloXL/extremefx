// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.StringMatcher.MatchersUtil
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.StringMatcher
{
  internal static class MatchersUtil
  {
    private static readonly string[] _currentMobileBrowserKeywords = new string[27]
    {
      "cldc",
      "symbian",
      "midp",
      "j2me",
      "mobile",
      "wireless",
      "palm",
      "phone",
      "pocket pc",
      "pocketpc",
      "netfront",
      "bolt",
      "iris",
      "brew",
      "openwave",
      "windows ce",
      "wap2",
      "android",
      "opera mini",
      "opera mobi",
      "maemo",
      "fennec",
      "blazer",
      "160x160",
      "webos",
      "sony",
      "nintendo"
    };

    public static bool IsMobileBrowser(string userAgent)
    {
      return Efx.Web.BrowserCaps.Collections.Exist<string>((IEnumerable<string>) MatchersUtil._currentMobileBrowserKeywords, Predicates.ContainedInCaseInsensitive(userAgent));
    }
  }
}
