// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.AppleMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class AppleMatcher : MatcherBase
  {
    private static readonly string[] _appleKeyWords = new string[3]
    {
      "iPhone",
      "iPod",
      "iPad"
    };
    private const string IPAD = "iPad";
    private const string IPHONE = "iPhone";
    private const string IPOD = "iPod";

    public AppleMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
    }

    public override bool CanMatch(string request)
    {
      return Efx.Web.BrowserCaps.Collections.Exist<string>((IEnumerable<string>) AppleMatcher._appleKeyWords, Predicates.ContainedIn(request));
    }

    protected override string ApplyRecoveryMatch(string request)
    {
      if (request.Contains("iPad"))
        return "apple_ipad_ver1";
      return !request.Contains("iPod") ? "apple_iphone_ver1" : "apple_ipod_touch_ver1";
    }

    protected override string LookForMatchingUserAgent(string userAgent)
    {
      int tolerance = userAgent.StartsWith("Apple") ? AppleMatcher.ThirdSpaceOrLength(userAgent) : MatcherBase.FirstSemiColon(userAgent);
      return LongestCommonPrefixMatcher.Match((IEnumerable<string>) this.UserAgents, userAgent, tolerance);
    }

    private static int ThirdSpaceOrLength(string userAgent)
    {
      return MatcherBase.OrdinalIndexOfOrLength(userAgent, " ", 3, 0);
    }
  }
}
