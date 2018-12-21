// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.ToleranceCalculatorAfterString
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class ToleranceCalculatorAfterString
  {
    private readonly string _needle;
    private readonly int _ordinalIndex;
    private readonly string _startingNeedle;

    public ToleranceCalculatorAfterString(string needle, int ordinalIndex, string startingNeedle)
    {
      this._needle = needle;
      this._ordinalIndex = ordinalIndex;
      this._startingNeedle = startingNeedle;
    }

    public int Tolerance(string userAgent)
    {
      return MatcherBase.OrdinalIndexOfOrLength(userAgent, this._needle, this._ordinalIndex, userAgent.IndexOf(this._startingNeedle, StringComparison.Ordinal));
    }
  }
}
