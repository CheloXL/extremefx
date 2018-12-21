// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.SamsungMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;
using System;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class SamsungMatcher : MatcherBase
  {
    public SamsungMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
    }

    public override bool CanMatch(string request)
    {
      if (MatcherBase.ContainsAnyOf(request, "Samsung/SGH", "Samsung"))
        return true;
      return MatcherBase.StartsWithAnyOf(request, "SEC-", "SAMSUNG", "SPH", "SGH", "SCH");
    }

    protected override string LookForMatchingUserAgent(string userAgent)
    {
      return LongestCommonPrefixMatcher.Match((IEnumerable<string>) this.UserAgents, userAgent, SamsungMatcher.Tolerance(userAgent));
    }

    private static int SecondSlashAfterSamsungOrLength(string userAgent)
    {
      return MatcherBase.OrdinalIndexOfOrLength(userAgent, "/", 2, userAgent.IndexOf("Samsung", StringComparison.Ordinal));
    }

    private static int Tolerance(string userAgent)
    {
      if (userAgent.StartsWith("SEC-") || userAgent.StartsWith("SAMSUNG-") || userAgent.StartsWith("SCH"))
        return MatcherBase.FirstSlash(userAgent);
      if (userAgent.StartsWith("Samsung") || userAgent.StartsWith("SPH") || userAgent.StartsWith("SGH"))
        return MatcherBase.FirstSpace(userAgent);
      if (userAgent.StartsWith("SAMSUNG/"))
        return MatcherBase.SecondSlash(userAgent);
      if (!userAgent.Contains("Samsung/SGH"))
        return userAgent.Length;
      return SamsungMatcher.SecondSlashAfterSamsungOrLength(userAgent);
    }
  }
}
