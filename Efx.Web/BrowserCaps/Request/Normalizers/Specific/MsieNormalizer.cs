// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Request.Normalizers.Specific.MsieNormalizer
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System.Text.RegularExpressions;

namespace Efx.Web.BrowserCaps.Request.Normalizers.Specific
{
  internal sealed class MsieNormalizer : IUserAgentNormalizer
  {
    private static readonly Regex _msieMajorAndMinorRegex = new Regex("MSIE\\s\\d\\.\\d", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public string Normalize(string userAgent)
    {
      Match match = MsieNormalizer._msieMajorAndMinorRegex.Match(userAgent);
      if (!match.Success)
        return userAgent;
      return match.Groups[0].Value;
    }
  }
}
