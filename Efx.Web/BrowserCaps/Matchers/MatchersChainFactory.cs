// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.MatchersChainFactory
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.Request.Normalizers.Specific;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class MatchersChainFactory
  {
    private readonly IUserAgentNormalizerChain _genericUserAgentNormalizerChain;
    private CatchAllMatcher _catchAllMatcher;

    public MatchersChainFactory(
      IUserAgentNormalizerChain genericUserAgentNormalizerChain)
    {
      this._genericUserAgentNormalizerChain = genericUserAgentNormalizerChain;
    }

    private IUserAgentNormalizer GenericNormalizer
    {
      get
      {
        return (IUserAgentNormalizer) this._genericUserAgentNormalizerChain;
      }
    }

    public MatchersChain Create(IEnumerable<DeviceInfo> devices)
    {
      IEnumerable<MatcherBase> matchers = this.GetMatchers();
      foreach (DeviceInfo device in devices)
      {
        foreach (MatcherBase matcherBase in matchers)
        {
          if (!string.IsNullOrEmpty(device.UserAgent))
          {
            if (matcherBase.CanMatch(device.UserAgent))
            {
              matcherBase.Add(device.UserAgent, device.Id);
              break;
            }
          }
          else
          {
            device.UserAgent = "MatchNone_" + device.Id;
            this._catchAllMatcher.Add(device.UserAgent, device.Id);
            break;
          }
        }
      }
      return new MatchersChain(matchers);
    }

    private IUserAgentNormalizerChain ComposeNormalizer(
      IUserAgentNormalizer userAgentNormalizer)
    {
      return this._genericUserAgentNormalizerChain.Add(userAgentNormalizer);
    }

    private IEnumerable<MatcherBase> GetMatchers()
    {
      ICollection<MatcherBase> matcherBases = (ICollection<MatcherBase>) new List<MatcherBase>();
      matcherBases.Add((MatcherBase) new NokiaMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new NokiaOviMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new LguplusMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new AndroidMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new SonyEricssonMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new MotorolaMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new BlackBerryMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new SiemensMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new SagemMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new SamsungMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new PanasonicMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new NecMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new QtekMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new MitsubishiMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new PhilipsMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new LgMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new AppleMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new KyoceraMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new AlcatelMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new SharpMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new SanyoMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new BenQMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new PantechMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new ToshibaMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new GrundigMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new HtcMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new BotMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new SpvMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new WindowsCeMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new PortalmmmMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new DoCoMoMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new KddiMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new VodafoneMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new OperaMiniMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new MaemoMatcher((IUserAgentNormalizer) this.ComposeNormalizer((IUserAgentNormalizer) new MaemoNormalizer())));
      matcherBases.Add((MatcherBase) new ChromeMatcher((IUserAgentNormalizer) this.ComposeNormalizer((IUserAgentNormalizer) new ChromeNormalizer())));
      matcherBases.Add((MatcherBase) new AolMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new OperaMatcher(this.GenericNormalizer));
      matcherBases.Add((MatcherBase) new SafariMatcher((IUserAgentNormalizer) this.ComposeNormalizer((IUserAgentNormalizer) new SafariNormalizer())));
      matcherBases.Add((MatcherBase) new FirefoxMatcher((IUserAgentNormalizer) this.ComposeNormalizer((IUserAgentNormalizer) new FirefoxNormalizer())));
      matcherBases.Add((MatcherBase) new MsieMatcher((IUserAgentNormalizer) this.ComposeNormalizer((IUserAgentNormalizer) new MsieNormalizer())));
      this._catchAllMatcher = new CatchAllMatcher(this.GenericNormalizer);
      matcherBases.Add((MatcherBase) this._catchAllMatcher);
      return (IEnumerable<MatcherBase>) matcherBases;
    }
  }
}
