// Decompiled with JetBrains decompiler
// Type: Class120
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

internal sealed class Class120
{
  private readonly string string_0;
  private readonly bool bool_0;
  private readonly bool bool_1;
  private readonly string[] string_1;

  public Class120(string string_2, string[] string_3, bool bool_2, bool bool_3)
  {
    this.string_0 = string_2;
    this.string_1 = string_3;
    this.bool_1 = bool_2;
    this.bool_0 = bool_3;
  }

  public string method_0()
  {
    return this.string_0;
  }

  public int method_1()
  {
    if (this.string_1 != null)
      return this.string_1.Length;
    return 0;
  }

  public string[] method_2()
  {
    return this.string_1;
  }

  public bool method_3()
  {
    return this.bool_1;
  }

  public bool method_4()
  {
    return this.bool_0;
  }
}
