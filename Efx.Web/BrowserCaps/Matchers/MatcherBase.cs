// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.MatcherBase
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;
using System;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal abstract class MatcherBase
  {
    private readonly IUserAgentNormalizer _userAgentNormalizer;
    private readonly IDictionary<string, string> _userAgentsDeviceIdMap;

    protected MatcherBase(IUserAgentNormalizer userAgentNormalizer)
    {
      this._userAgentsDeviceIdMap = (IDictionary<string, string>) new SortedDictionary<string, string>((IComparer<string>) StringComparer.Ordinal);
      this._userAgentNormalizer = userAgentNormalizer;
    }

    protected ICollection<string> UserAgents
    {
      get
      {
        return this._userAgentsDeviceIdMap.Keys;
      }
    }

    public void Add(string userAgent, string deviceId)
    {
      this._userAgentsDeviceIdMap[this.NormalizeUserAgent(userAgent)] = deviceId;
    }

    public abstract bool CanMatch(string request);

    public string Match(string wurflRequest)
    {
      string str1 = this._userAgentNormalizer.Normalize(wurflRequest);
      string key = str1;
      if (string.IsNullOrEmpty(key))
        return "generic";
      string deviceId1;
      this._userAgentsDeviceIdMap.TryGetValue(key, out deviceId1);
      if (!MatcherBase.IsBlankOrGeneric(deviceId1))
        return deviceId1;
      string deviceId2 = this.ApplyConclusiveMatch(str1);
      if (!MatcherBase.IsBlankOrGeneric(deviceId2))
        return deviceId2;
      string deviceId3 = this.ApplyRecoveryMatch(str1);
      if (!MatcherBase.IsBlankOrGeneric(deviceId3))
        return deviceId3;
      string str2 = MatcherBase.ApplyCatchAllRecoveryMatch(str1);
      if (!string.IsNullOrEmpty(str2))
        return str2;
      return "generic";
    }

    internal static int OrdinalIndexOfOrLength(
      string target,
      string needle,
      int ordinal,
      int startIndex = 0)
    {
      if (startIndex >= target.Length)
        return target.Length;
      int num1 = 0;
      int num2 = startIndex;
      do
      {
        num2 = target.IndexOf(needle, num2 + 1, StringComparison.Ordinal);
        if (num2 >= 0)
          ++num1;
        else
          break;
      }
      while (num1 < ordinal);
      if (num2 == -1)
        return target.Length;
      return num2;
    }

    protected static bool ContainsAllOf(string haystack, params string[] needles)
    {
      foreach (string needle in needles)
      {
        if (!haystack.Contains(needle))
          return false;
      }
      return true;
    }

    protected static bool ContainsAnyOf(string haystack, params string[] needles)
    {
      foreach (string needle in needles)
      {
        if (haystack.Contains(needle))
          return true;
      }
      return false;
    }

    protected static int FirstSemiColon(string target)
    {
      return MatcherBase.OrdinalIndexOfOrLength(target, ";", 1, 0);
    }

    protected static int FirstSlash(string target)
    {
      return MatcherBase.IndexOfOrLength(target, "/", 0);
    }

    protected static int FirstSpace(string target)
    {
      return MatcherBase.IndexOfOrLength(target, " ", 0);
    }

    protected static int IndexOfOrLength(string target, string needle, int startIndex = 0)
    {
      return MatcherBase.OrdinalIndexOfOrLength(target, needle, 1, startIndex);
    }

    protected static int SecondSlash(string target)
    {
      return MatcherBase.OrdinalIndexOfOrLength(target, "/", 2, 0);
    }

    protected static bool StartsWithAnyOf(string haystack, params string[] needles)
    {
      foreach (string needle in needles)
      {
        if (haystack.StartsWith(needle))
          return true;
      }
      return false;
    }

    protected virtual string ApplyConclusiveMatch(string request)
    {
      string index = this.LookForMatchingUserAgent(request);
      if (index == null)
        return (string) null;
      return this._userAgentsDeviceIdMap[index];
    }

    protected virtual string ApplyRecoveryMatch(string request)
    {
      return "generic";
    }

    protected bool DeviceExist(string deviceId)
    {
      return this._userAgentsDeviceIdMap.Values.Contains(deviceId);
    }

    protected virtual string LookForMatchingUserAgent(string userAgent)
    {
      return LongestCommonPrefixMatcher.Match((IEnumerable<string>) this.UserAgents, userAgent, MatcherBase.FirstSlash(userAgent));
    }

    private static string ApplyCatchAllRecoveryMatch(string normalizedRequest)
    {
      return MatcherBase.CatchAllRecoveryMap.DeviceIdOf(normalizedRequest);
    }

    private static bool IsBlankOrGeneric(string deviceId)
    {
      if (!string.IsNullOrEmpty(deviceId))
        return "generic_mobile".Equals(deviceId);
      return true;
    }

    private string NormalizeUserAgent(string userAgent)
    {
      return this._userAgentNormalizer.Normalize(userAgent);
    }

    private static class CatchAllRecoveryMap
    {
      private static readonly IDictionary<string, string> _cathAllRecoveryMap = (IDictionary<string, string>) new Dictionary<string, string>();

      static CatchAllRecoveryMap()
      {
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("UP.Browser/7.2", "opwv_v72_generic");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("UP.Browser/7", "opwv_v7_generic");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("UP.Browser/6.2", "opwv_v62_generic");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("UP.Browser/6", "opwv_v6_generic");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("UP.Browser/5", "upgui_generic");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("UP.Browser/4", "uptext_generic");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("UP.Browser/3", "uptext_generic");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("Series60", "nokia_generic_series60");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("NetFront/3.0", "generic_netfront_ver3");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("ACS-NF/3.0", "generic_netfront_ver3");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("NetFront/3.1", "generic_netfront_ver3_1");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("ACS-NF/3.1", "generic_netfront_ver3_1");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("NetFront/3.2", "generic_netfront_ver3_2");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("ACS-NF/3.2", "generic_netfront_ver3_2");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("NetFront/3.3", "generic_netfront_ver3_3");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("ACS-NF/3.3", "generic_netfront_ver3_3");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("NetFront/3.4", "generic_netfront_ver3_4");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("NetFront/3.5", "generic_netfront_ver3_5");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("NetFront/4.0", "generic_netfront_ver4");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("NetFront/4.1", "generic_netfront_ver4_1");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("Windows CE", "generic_ms_mobile_browser_ver1");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("Mozilla/4.0", "generic_web_browser");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("Mozilla/5.0", "generic_web_browser");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("Mozilla/", "generic_xhtml");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("ObigoInternetBrowser/Q03C", "generic_xhtml");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("AU-MIC/2", "generic_xhtml");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("AU-MIC-", "generic_xhtml");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("AU-OBIGO/", "generic_xhtml");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("Obigo/Q03", "generic_xhtml");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("Obigo/Q04", "generic_xhtml");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("ObigoInternetBrowser/2", "generic_xhtml");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("Teleca Q03B1", "generic_xhtml");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("Opera Mini/1", "browser_opera_mini_release1");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("Opera Mini/2", "browser_opera_mini_release2");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("Opera Mini/3", "browser_opera_mini_release3");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("Opera Mini/4", "browser_opera_mini_release4");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("Opera Mini/5", "browser_opera_mini_release5");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("DoCoMo", "docomo_generic_jap_ver1");
        MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Add("KDDI", "docomo_generic_jap_ver1");
      }

      public static string DeviceIdOf(string userAgent)
      {
        string index = Efx.Web.BrowserCaps.Collections.Find<string>((IEnumerable<string>) MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap.Keys, Predicates.ContainedIn(userAgent));
        if (index == null)
          return "generic_mobile";
        return MatcherBase.CatchAllRecoveryMap._cathAllRecoveryMap[index];
      }
    }
  }
}
