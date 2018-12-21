// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.BotMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class BotMatcher : MatcherBase
  {
    private static readonly string[] _currentBotKeywords = new string[10]
    {
      "bot",
      "crawler",
      "spider",
      "novarra",
      "transcoder",
      "yahoo! searchmonkey",
      "yahoo! slurp",
      "feedfetcher-google",
      "toolbar",
      "mowser"
    };
    private readonly List<string> _botKeywords = new List<string>();
    private const int BOTS_LD_TOLERANCE = 4;

    public BotMatcher(IUserAgentNormalizer userAgentNormalizer)
      : this(userAgentNormalizer, (IEnumerable<string>) new List<string>())
    {
    }

    private BotMatcher(IUserAgentNormalizer userAgentNormalizer, IEnumerable<string> keywords)
      : base(userAgentNormalizer)
    {
      this._botKeywords = new List<string>(keywords);
      this._botKeywords.AddRange((IEnumerable<string>) BotMatcher._currentBotKeywords);
    }

    public override bool CanMatch(string request)
    {
      return Efx.Web.BrowserCaps.Collections.Exist<string>((IEnumerable<string>) this._botKeywords, Predicates.ContainedInCaseInsensitive(request));
    }

    protected override string ApplyRecoveryMatch(string normalizedRequest)
    {
      return "generic_web_crawler";
    }

    protected override string LookForMatchingUserAgent(string userAgent)
    {
      return LevenshteinDistanceMatcher.Match((IEnumerable<string>) this.UserAgents, userAgent, 4);
    }
  }
}
