﻿// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.SonyEricssonMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class SonyEricssonMatcher : MatcherBase
  {
    public SonyEricssonMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
    }

    public override bool CanMatch(string request)
    {
      return request.Contains("SonyEricsson");
    }

    protected override string LookForMatchingUserAgent(string userAgent)
    {
      int tolerance = userAgent.StartsWith("SonyEricsson") ? MatcherBase.FirstSlash(userAgent) : MatcherBase.SecondSlash(userAgent);
      return LongestCommonPrefixMatcher.Match((IEnumerable<string>) this.UserAgents, userAgent, tolerance);
    }
  }
}
