// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.MatchersChain
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class MatchersChain
  {
    private readonly ICollection<MatcherBase> _matchers;

    public MatchersChain(IEnumerable<MatcherBase> matchers)
    {
      this._matchers = (ICollection<MatcherBase>) new List<MatcherBase>(matchers);
    }

    public string Match(string wurflRequest)
    {
      foreach (MatcherBase matcher in (IEnumerable<MatcherBase>) this._matchers)
      {
        if (matcher.CanMatch(wurflRequest))
          return matcher.Match(wurflRequest);
      }
      return (string) null;
    }
  }
}
