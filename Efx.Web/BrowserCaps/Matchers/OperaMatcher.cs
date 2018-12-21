// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.OperaMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class OperaMatcher : MatcherBase
  {
    private static readonly Regex _operaVersion = new Regex(".*Opera[\\s/](\\d+).*", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private readonly IDictionary<string, string> _operaVersions = (IDictionary<string, string>) new Dictionary<string, string>();
    private const int OPERA_LD_TOLERANCE = 1;

    public OperaMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
      this.CreateCatchAllIds();
    }

    public override bool CanMatch(string request)
    {
      if (!MatchersUtil.IsMobileBrowser(request))
        return request.Contains("Opera");
      return false;
    }

    protected override string ApplyRecoveryMatch(string request)
    {
      string deviceId = this.OperaId(OperaMatcher.OperaVersionNumber(request));
      if (!this.DeviceExist(deviceId))
        return "generic";
      return deviceId;
    }

    protected override string LookForMatchingUserAgent(string userAgent)
    {
      return LevenshteinDistanceMatcher.Match((IEnumerable<string>) this.UserAgents, userAgent, 1);
    }

    private void CreateCatchAllIds()
    {
      this._operaVersions.Add("", "opera");
      this._operaVersions.Add("7", "opera_7");
      this._operaVersions.Add("8", "opera_8");
      this._operaVersions.Add("9", "opera_9");
      this._operaVersions.Add("10", "opera_10");
    }

    private string OperaId(string operaVersionNumber)
    {
      if (!this._operaVersions.ContainsKey(operaVersionNumber))
        return "opera";
      return this._operaVersions[operaVersionNumber];
    }

    private static string OperaVersionNumber(string userAgent)
    {
      Match match = OperaMatcher._operaVersion.Match(userAgent);
      if (!match.Success)
        return string.Empty;
      return match.Groups[1].Value;
    }
  }
}
