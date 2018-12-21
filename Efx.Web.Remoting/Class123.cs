// Decompiled with JetBrains decompiler
// Type: Class123
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using Efx.Web.Remoting.Serialization.Amf;

internal sealed class Class123 : IDataOutput
{
  private readonly Class119 class119_0;

  public Class123(Class119 class119_1)
  {
    this.class119_0 = class119_1;
  }

  public void WriteBoolean(bool pValue)
  {
    this.class119_0.method_11(pValue);
  }

  public void WriteByte(byte pValue)
  {
    this.class119_0.method_0(pValue);
  }

  public void WriteBytes(byte[] pBytes, int pOffset, int pLength)
  {
    for (int index = pOffset; index < pOffset + pLength; ++index)
      this.class119_0.method_0(pBytes[index]);
  }

  public void WriteDouble(double pValue)
  {
    this.class119_0.method_8(pValue);
  }

  public void WriteFloat(float pValue)
  {
    this.class119_0.method_9(pValue);
  }

  public void WriteInt(int pValue)
  {
    this.class119_0.method_10(pValue);
  }

  public void WriteObject(object pValue)
  {
    this.class119_0.method_14(pValue);
  }

  public void WriteShort(short pValue)
  {
    this.class119_0.method_4((int) pValue);
  }

  public void WriteUnsignedInt(uint pValue)
  {
    this.class119_0.method_10((int) pValue);
  }

  public void WriteUTF(string pValue)
  {
    this.class119_0.method_5(pValue);
  }

  public void WriteUTFBytes(string pValue)
  {
    this.class119_0.method_6(pValue);
  }
}
