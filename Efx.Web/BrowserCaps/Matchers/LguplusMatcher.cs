// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.LguplusMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class LguplusMatcher : MatcherBase
  {
    public LguplusMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
    }

    public override bool CanMatch(string request)
    {
      return MatcherBase.ContainsAnyOf(request, "LGUPLUS", "lgtelecom");
    }

    protected override string ApplyConclusiveMatch(string request)
    {
      return "generic_mobile";
    }

    protected override string ApplyRecoveryMatch(string normalizedRequest)
    {
      foreach (KeyValuePair<string, string[]> lgupluse in LguplusMatcher.Lgupluses())
      {
        if (MatcherBase.ContainsAllOf(normalizedRequest, lgupluse.Value))
          return lgupluse.Key;
      }
      return "generic_lguplus";
    }

    private static IEnumerable<KeyValuePair<string, string[]>> Lgupluses()
    {
      IDictionary<string, string[]> dictionary = (IDictionary<string, string[]>) new Dictionary<string, string[]>();
      dictionary.Add("generic_lguplus_rexos_facebook_browser", new string[2]
      {
        "Windows NT 5",
        "POLARIS"
      });
      dictionary.Add("generic_lguplus_rexos_webviewer_browser", new string[1]
      {
        "Windows NT 5"
      });
      dictionary.Add("generic_lguplus_winmo_facebook_browser", new string[2]
      {
        "Windows CE",
        "POLARIS"
      });
      dictionary.Add("generic_lguplus_android_webkit_browser", new string[2]
      {
        "Android",
        "AppleWebKit"
      });
      return (IEnumerable<KeyValuePair<string, string[]>>) dictionary;
    }
  }
}
