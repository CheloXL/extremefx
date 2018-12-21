// Decompiled with JetBrains decompiler
// Type: Efx.Web.Authorization.Repository.Discriminator
// Assembly: Efx.Web.Authorization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 06C571D6-DC37-4657-A156-F8EB982998FF
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Authorization.dll

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Xml.Serialization;

namespace Efx.Web.Authorization.Repository
{
  public sealed class Discriminator : Interface2
  {
    private Regex regex_0;
    private string string_0;
    private string string_1;

    [XmlAttribute]
    public string InputExpression
    {
      get
      {
        return this.string_0;
      }
      set
      {
        this.string_0 = value;
      }
    }

    [XmlAttribute]
    public string Pattern
    {
      get
      {
        return this.string_1;
      }
      set
      {
        this.string_1 = value;
      }
    }

    private Regex method_0()
    {
      if (this.regex_0 == null && !string.IsNullOrEmpty(this.Pattern))
        this.regex_0 = new Regex(this.Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
      return this.regex_0;
    }

    public bool Qualifies(object pContext)
    {
      if (pContext == null)
        throw new ArgumentNullException(nameof (pContext));
      if (this.method_0() != null)
        return this.method_0().Match(this.method_1(pContext)).Success;
      return false;
    }

    private string method_1(object object_0)
    {
      if (object_0 == null)
        throw new ArgumentNullException("pContext");
      return Convert.ToString(DataBinder.Eval(object_0, this.InputExpression), (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }
}
