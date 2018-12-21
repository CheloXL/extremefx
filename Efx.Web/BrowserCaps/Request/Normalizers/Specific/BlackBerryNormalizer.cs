// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Request.Normalizers.Specific.BlackBerryNormalizer
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;

namespace Efx.Web.BrowserCaps.Request.Normalizers.Specific
{
  internal sealed class BlackBerryNormalizer : IUserAgentNormalizer
  {
    public string Normalize(string userAgent)
    {
      string str = userAgent.Replace("Blackberry", "BlackBerry");
      int startIndex = userAgent.IndexOf("BlackBerry", StringComparison.Ordinal);
      if (startIndex > 0 && !userAgent.Contains("AppleWebKit"))
        str = userAgent.Substring(startIndex);
      return str;
    }
  }
}
