// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.WindowsCeMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class WindowsCeMatcher : MatcherBase
  {
    private const int WINDOWS_CE_TOLERANCE = 3;

    public WindowsCeMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
    }

    public override bool CanMatch(string request)
    {
      if (!request.Contains("Mozilla/"))
        return false;
      if (!request.Contains("WindowsCE"))
        return request.Contains("Windows CE");
      return true;
    }

    protected override string ApplyRecoveryMatch(string normalizedRequest)
    {
      return "generic_ms_mobile_browser_ver1";
    }

    protected override string LookForMatchingUserAgent(string userAgent)
    {
      return LevenshteinDistanceMatcher.Match((IEnumerable<string>) this.UserAgents, userAgent, 3);
    }
  }
}
