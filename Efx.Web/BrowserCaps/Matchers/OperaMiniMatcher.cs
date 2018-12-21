// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.OperaMiniMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class OperaMiniMatcher : MatcherBase
  {
    private readonly IDictionary<string, string> _operaMini = (IDictionary<string, string>) new Dictionary<string, string>();

    public OperaMiniMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
      this.CreateCatchAllIds();
    }

    public override bool CanMatch(string request)
    {
      return request.Contains("Opera Mini");
    }

    protected override string ApplyRecoveryMatch(string request)
    {
      string index = Efx.Web.BrowserCaps.Collections.Find<string>((IEnumerable<string>) this._operaMini.Keys, Predicates.ContainedIn(request));
      if (index == null)
        return "generic";
      return this._operaMini[index];
    }

    protected override string LookForMatchingUserAgent(string userAgent)
    {
      return LongestCommonPrefixMatcher.Match((IEnumerable<string>) this.UserAgents, userAgent, ToleranceCalculators.First("/").After("Opera Mini").Tolerance(userAgent));
    }

    private void CreateCatchAllIds()
    {
      this._operaMini.Add("Opera Mini/1", "browser_opera_mini_release1");
      this._operaMini.Add("Opera Mini/2", "browser_opera_mini_release2");
      this._operaMini.Add("Opera Mini/3", "browser_opera_mini_release3");
      this._operaMini.Add("Opera Mini/4", "browser_opera_mini_release4");
      this._operaMini.Add("Opera Mini/5", "browser_opera_mini_release5");
    }
  }
}
