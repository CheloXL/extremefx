// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Request.Normalizers.UserAgentNormalizerChain
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request.Normalizers.Generic;
using Efx.Web.BrowserCaps.Request.Normalizers.Specific;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Request.Normalizers
{
  internal sealed class UserAgentNormalizerChain : IUserAgentNormalizer, IUserAgentNormalizerChain
  {
    private readonly ICollection<IUserAgentNormalizer> _userAgentNormalizers;

    public UserAgentNormalizerChain()
    {
      this._userAgentNormalizers = (ICollection<IUserAgentNormalizer>) new List<IUserAgentNormalizer>()
      {
        (IUserAgentNormalizer) new SerialNumberRemover(),
        (IUserAgentNormalizer) new BlackBerryNormalizer(),
        (IUserAgentNormalizer) new YesWAPRemover(),
        (IUserAgentNormalizer) new UpLinkRemover(),
        (IUserAgentNormalizer) new BabelFishRemover(),
        (IUserAgentNormalizer) new LocaleRemover()
      };
    }

    private UserAgentNormalizerChain(
      IEnumerable<IUserAgentNormalizer> userAgentNormalizers)
    {
      this._userAgentNormalizers = (ICollection<IUserAgentNormalizer>) new List<IUserAgentNormalizer>(userAgentNormalizers);
    }

    public IUserAgentNormalizerChain Add(
      IUserAgentNormalizer userAgentNormalizer)
    {
      return (IUserAgentNormalizerChain) new UserAgentNormalizerChain((IEnumerable<IUserAgentNormalizer>) new List<IUserAgentNormalizer>((IEnumerable<IUserAgentNormalizer>) this._userAgentNormalizers)
      {
        userAgentNormalizer
      });
    }

    public string Normalize(string userAgent)
    {
      string userAgent1 = userAgent;
      foreach (IUserAgentNormalizer userAgentNormalizer in (IEnumerable<IUserAgentNormalizer>) this._userAgentNormalizers)
        userAgent1 = userAgentNormalizer.Normalize(userAgent1);
      return userAgent1;
    }
  }
}
