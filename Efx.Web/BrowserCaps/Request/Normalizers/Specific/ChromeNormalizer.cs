// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Request.Normalizers.Specific.ChromeNormalizer
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;
using System.Text.RegularExpressions;

namespace Efx.Web.BrowserCaps.Request.Normalizers.Specific
{
  internal sealed class ChromeNormalizer : IUserAgentNormalizer
  {
    private static readonly Regex _chromeWithMajorVersionRegex = new Regex("Chrome/\\d", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public string Normalize(string userAgent)
    {
      if (!ChromeNormalizer.ContainsChromeWithMajorVersion(userAgent))
        return userAgent;
      return userAgent.Substring(userAgent.IndexOf("Chrome", StringComparison.Ordinal), 8);
    }

    private static bool ContainsChromeWithMajorVersion(string userAgent)
    {
      return ChromeNormalizer._chromeWithMajorVersionRegex.IsMatch(userAgent);
    }
  }
}
