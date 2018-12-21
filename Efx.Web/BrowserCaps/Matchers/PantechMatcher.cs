// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.PantechMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class PantechMatcher : MatcherBase
  {
    private const int PANTECH_LD_TOLLERANCE = 4;

    public PantechMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
    }

    public override bool CanMatch(string request)
    {
      if (!request.StartsWith("Pantech") && !request.StartsWith("PT-") && !request.StartsWith("PANTECH"))
        return request.StartsWith("PG-");
      return true;
    }

    protected override string LookForMatchingUserAgent(string userAgent)
    {
      if (!userAgent.StartsWith("Pantech"))
        return base.LookForMatchingUserAgent(userAgent);
      return LevenshteinDistanceMatcher.Match((IEnumerable<string>) this.UserAgents, userAgent, 4);
    }
  }
}
