// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Request.Normalizers.Specific.SafariNormalizer
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System.Text.RegularExpressions;

namespace Efx.Web.BrowserCaps.Request.Normalizers.Specific
{
  internal sealed class SafariNormalizer : IUserAgentNormalizer
  {
    private static readonly Regex _safariRegex = new Regex("(Mozilla\\/5\\.0.*)(?:;\\s*U;.*)(Safari\\/\\d{0,3})", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public string Normalize(string userAgent)
    {
      Match match = SafariNormalizer._safariRegex.Match(userAgent);
      if (!match.Success)
        return userAgent;
      return match.Groups[1].Value + " " + match.Groups[2].Value;
    }
  }
}
