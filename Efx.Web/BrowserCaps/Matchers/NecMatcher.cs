// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.NecMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class NecMatcher : MatcherBase
  {
    private const int NEC_LD_TOLERANCE = 2;

    public NecMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
    }

    public override bool CanMatch(string request)
    {
      if (!request.StartsWith("NEC"))
        return request.StartsWith("KGT");
      return true;
    }

    protected override string LookForMatchingUserAgent(string userAgent)
    {
      if (!userAgent.StartsWith("NEC"))
        return LevenshteinDistanceMatcher.Match((IEnumerable<string>) this.UserAgents, userAgent, 2);
      return base.LookForMatchingUserAgent(userAgent);
    }
  }
}
