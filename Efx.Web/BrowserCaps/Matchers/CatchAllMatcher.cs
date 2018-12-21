// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.CatchAllMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class CatchAllMatcher : MatcherBase
  {
    private const int MOZILLA_TOLERANCE = 5;

    public CatchAllMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
    }

    public override bool CanMatch(string normalziedRequest)
    {
      return true;
    }

    protected override string LookForMatchingUserAgent(string userAgent)
    {
      if (!userAgent.StartsWith("Mozilla"))
        return base.LookForMatchingUserAgent(userAgent);
      if (!userAgent.StartsWith("Mozilla/4"))
        return LevenshteinDistanceMatcher.Match(userAgent.StartsWith("Mozilla/5") ? (IEnumerable<string>) CatchAllMatcher.GetMozillaData((IEnumerable<string>) this.UserAgents, "Mozilla/5") : (IEnumerable<string>) CatchAllMatcher.GetMozillaData((IEnumerable<string>) this.UserAgents, "Mozilla"), userAgent, 5);
      return LevenshteinDistanceMatcher.Match((IEnumerable<string>) CatchAllMatcher.GetMozillaData((IEnumerable<string>) this.UserAgents, "Mozilla/4"), userAgent, 5);
    }

    private static ICollection<string> GetMozillaData(
      IEnumerable<string> userAgentsSet,
      string starting)
    {
      return Efx.Web.BrowserCaps.Collections.Select<string>(userAgentsSet, Predicates.StartsWith(starting));
    }
  }
}
