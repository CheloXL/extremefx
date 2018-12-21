// Decompiled with JetBrains decompiler
// Type: Class114
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using System;
using System.Reflection;

internal sealed class Class114
{
  public readonly MethodInfo methodInfo_0;
  public readonly string string_0;
  public readonly string string_1;
  public readonly bool bool_0;
  public readonly Type type_0;
  public readonly ParameterInfo[] parameterInfo_0;

  public Class114(
    Type type_1,
    string string_2,
    bool bool_1,
    MethodInfo methodInfo_1,
    ParameterInfo[] parameterInfo_1,
    string string_3)
  {
    this.type_0 = type_1;
    this.string_1 = string_3;
    this.parameterInfo_0 = parameterInfo_1;
    this.string_0 = string_2;
    this.bool_0 = bool_1;
    this.methodInfo_0 = methodInfo_1;
  }
}
