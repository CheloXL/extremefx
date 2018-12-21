// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.KyoceraMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class KyoceraMatcher : MatcherBase
  {
    public KyoceraMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
    }

    public override bool CanMatch(string request)
    {
      if (!request.StartsWith("Kyocera") && !request.StartsWith("QC-"))
        return request.StartsWith("KWC-");
      return true;
    }

    protected override string LookForMatchingUserAgent(string userAgent)
    {
      return LongestCommonPrefixMatcher.Match((IEnumerable<string>) this.UserAgents, userAgent, MatcherBase.FirstSlash(userAgent));
    }
  }
}
