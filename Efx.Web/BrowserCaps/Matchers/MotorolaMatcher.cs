// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.MotorolaMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class MotorolaMatcher : MatcherBase
  {
    public MotorolaMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
    }

    public override bool CanMatch(string request)
    {
      if (!request.StartsWith("Mot-") && !request.Contains("MOT-"))
        return request.Contains("Motorola");
      return true;
    }

    protected override string ApplyRecoveryMatch(string request)
    {
      return !request.Contains("MIB/2.2") && !request.Contains("MIB/BER2.2") ? "generic" : "mot_mib22_generic";
    }

    protected override string LookForMatchingUserAgent(string userAgent)
    {
      if (!userAgent.StartsWith("Mot-") && !userAgent.StartsWith("MOT-") && !userAgent.StartsWith("Motorola"))
        return LevenshteinDistanceMatcher.Match((IEnumerable<string>) this.UserAgents, userAgent, 5);
      return base.LookForMatchingUserAgent(userAgent);
    }
  }
}
