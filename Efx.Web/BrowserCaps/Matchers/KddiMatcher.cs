// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.KddiMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;
using System;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class KddiMatcher : MatcherBase
  {
    public KddiMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
    }

    public override bool CanMatch(string request)
    {
      return request.Contains("KDDI");
    }

    protected override string ApplyRecoveryMatch(string request)
    {
      return request.IndexOf("Opera", StringComparison.Ordinal) == -1 ? "opwv_v62_generic" : "opera";
    }

    protected override string LookForMatchingUserAgent(string userAgent)
    {
      return LongestCommonPrefixMatcher.Match((IEnumerable<string>) this.UserAgents, userAgent, KddiMatcher.Tolerance(userAgent));
    }

    private static int Tolerance(string userAgent)
    {
      if (userAgent.StartsWith("KDDI/"))
        return MatcherBase.SecondSlash(userAgent);
      if (!userAgent.StartsWith("KDDI"))
        return MatcherBase.IndexOfOrLength(userAgent, ")", 0);
      return MatcherBase.FirstSlash(userAgent);
    }
  }
}
