// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.DoCoMoMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class DoCoMoMatcher : MatcherBase
  {
    public DoCoMoMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
    }

    public override bool CanMatch(string request)
    {
      return request.StartsWith("DoCoMo");
    }

    protected override string ApplyRecoveryMatch(string request)
    {
      return !request.StartsWith("DoCoMo/2") ? "docomo_generic_jap_ver1" : "docomo_generic_jap_ver2";
    }

    protected override string LookForMatchingUserAgent(string userAgent)
    {
      return (string) null;
    }
  }
}
