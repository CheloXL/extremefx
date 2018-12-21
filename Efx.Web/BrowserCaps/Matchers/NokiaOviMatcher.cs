﻿// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.NokiaOviMatcher
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.StringMatcher;
using System;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class NokiaOviMatcher : MatcherBase
  {
    public NokiaOviMatcher(IUserAgentNormalizer userAgentNormalizer)
      : base(userAgentNormalizer)
    {
    }

    public override bool CanMatch(string request)
    {
      if (!request.Contains("Nokia"))
        return request.Contains("S40OviBrowser");
      return true;
    }

    protected override string LookForMatchingUserAgent(string userAgent)
    {
      int tolerance = NokiaOviMatcher.IndexOfAnyOrLengthAfterNokia(userAgent);
      return LongestCommonPrefixMatcher.Match((IEnumerable<string>) this.UserAgents, userAgent, tolerance);
    }

    private static int IndexOfAnyOrLengthAfterNokia(string userAgent)
    {
      int num = userAgent.IndexOfAny(new char[2]{ '/', ' ' }, userAgent.IndexOf("Nokia", StringComparison.Ordinal));
      if (num > -1)
        return num;
      return userAgent.Length;
    }

    protected override string ApplyRecoveryMatch(string request)
    {
      return "nokia_generic_series40_ovibrosr";
    }
  }
}