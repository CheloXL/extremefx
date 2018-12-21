// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Matchers.IndexedString
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

namespace Efx.Web.BrowserCaps.Matchers
{
  internal sealed class IndexedString
  {
    private const int FIRST = 1;
    private readonly string _needle;

    public IndexedString(string needle)
    {
      this._needle = needle;
    }

    public ToleranceCalculatorAfterString After(string startingNeedle)
    {
      return new ToleranceCalculatorAfterString(this._needle, 1, startingNeedle);
    }
  }
}
