// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.DesktopBrowserMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal abstract class DesktopBrowserMatcher : MatcherBase
  {
    protected DesktopBrowserMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
    }

    public override bool CanMatch(string request)
    {
      if (!MatchersUtil.IsMobileBrowser(request))
        return this.CanMatchDesktopBrowser(request);
      return false;
    }

    protected abstract bool CanMatchDesktopBrowser(string request);
  }
}
