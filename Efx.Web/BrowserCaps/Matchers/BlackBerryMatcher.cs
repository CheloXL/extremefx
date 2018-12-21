// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.BlackBerryMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class BlackBerryMatcher : MatcherBase
  {
    private static readonly IDictionary<string, string> _blackBerries = BlackBerryMatcher.GenericBlackBerries();
    private static readonly Regex _blackBerryOsVersionRegex = new Regex("Black[Bb]erry[^/\\s]+/(\\d.\\d)", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public BlackBerryMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
    }

    public override bool CanMatch(string request)
    {
      if (!request.Contains("BlackBerry"))
        return request.Contains("Blackberry");
      return true;
    }

    protected override string ApplyRecoveryMatch(string normalizedRequest)
    {
      string input = BlackBerryMatcher.BlackBerryOsVersion(normalizedRequest);
      if (input == null)
        return "generic_mobile";
      string index = Efx.Web.BrowserCaps.Collections.Find<string>((IEnumerable<string>) BlackBerryMatcher._blackBerries.Keys, Predicates.PrefixOf(input));
      if (index == null)
        return "generic_mobile";
      return BlackBerryMatcher._blackBerries[index];
    }

    private static string BlackBerryOsVersion(string userAgent)
    {
      Match match = BlackBerryMatcher._blackBerryOsVersionRegex.Match(userAgent);
      if (!match.Success)
        return (string) null;
      return match.Groups[1].Value;
    }

    private static IDictionary<string, string> GenericBlackBerries()
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("2.", "blackberry_generic_ver2");
      dictionary.Add("3.2", "blackberry_generic_ver3_sub2");
      dictionary.Add("3.3", "blackberry_generic_ver3_sub30");
      dictionary.Add("3.5", "blackberry_generic_ver3_sub50");
      dictionary.Add("3.6", "blackberry_generic_ver3_sub60");
      dictionary.Add("3.7", "blackberry_generic_ver3_sub70");
      dictionary.Add("4.1", "blackberry_generic_ver4_sub10");
      dictionary.Add("4.2", "blackberry_generic_ver4_sub20");
      dictionary.Add("4.3", "blackberry_generic_ver4_sub30");
      dictionary.Add("4.5", "blackberry_generic_ver4_sub50");
      dictionary.Add("4.6", "blackberry_generic_ver4_sub60");
      dictionary.Add("4.7", "blackberry_generic_ver4_sub70");
      dictionary.Add("4.", "blackberry_generic_ver4");
      dictionary.Add("5.", "blackberry_generic_ver5");
      dictionary.Add("6.", "blackberry_generic_ver6");
      return dictionary;
    }
  }
}
