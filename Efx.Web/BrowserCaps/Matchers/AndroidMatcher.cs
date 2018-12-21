// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.AndroidMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class AndroidMatcher : MatcherBase
  {
    private static readonly IDictionary<string, string> _andoroids = AndroidMatcher.GenericAndroids();
    private static readonly Regex _androidOsVersionRegex = new Regex("Android[\\s/](\\d).(\\d)", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private const string GENERIC_ANDROID = "generic_android";

    public AndroidMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
    }

    public override bool CanMatch(string request)
    {
      if (request.StartsWith("Mozilla"))
        return request.Contains("Android");
      return false;
    }

    protected override string ApplyRecoveryMatch(string request)
    {
      if (AndroidMatcher.IsFroyo(request))
        return "generic_android_ver2_2";
      string key = AndroidMatcher.AndroidOsVersion(request);
      if (!AndroidMatcher._andoroids.ContainsKey(key))
        return "generic_android";
      return AndroidMatcher._andoroids[key];
    }

    protected override string LookForMatchingUserAgent(string userAgent)
    {
      int tolerance = AndroidMatcher.FirsSpaceAfterAndroidString(userAgent);
      return LongestCommonPrefixMatcher.Match((IEnumerable<string>) this.UserAgents, userAgent, tolerance);
    }

    private static string AndroidOsVersion(string userAgent)
    {
      Match match = AndroidMatcher._androidOsVersionRegex.Match(userAgent);
      if (!match.Success)
        return string.Empty;
      return match.Groups[1].Value + "_" + (object) match.Groups[2];
    }

    private static int FirsSpaceAfterAndroidString(string userAgent)
    {
      return MatcherBase.IndexOfOrLength(userAgent, " ", MatcherBase.IndexOfOrLength(userAgent, "Android", 0));
    }

    private static IDictionary<string, string> GenericAndroids()
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("", "generic_android");
      dictionary.Add("1_5", "generic_android_ver1_5");
      dictionary.Add("1_6", "generic_android_ver1_6");
      dictionary.Add("2_0", "generic_android_ver2");
      dictionary.Add("2_1", "generic_android_ver2_1");
      dictionary.Add("2_2", "generic_android_ver2_2");
      dictionary.Add("2_3", "generic_android_ver2_3");
      dictionary.Add("3_0", "generic_android_ver3_0");
      dictionary.Add("3_1", "generic_android_ver3_1");
      dictionary.Add("3_2", "generic_android_ver3_2");
      dictionary.Add("3_3", "generic_android_ver3_3");
      return dictionary;
    }

    private static bool IsFroyo(string userAgent)
    {
      return userAgent.Contains("Froyo");
    }
  }
}
