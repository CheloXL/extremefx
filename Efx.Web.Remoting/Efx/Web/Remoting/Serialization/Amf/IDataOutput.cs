// Decompiled with JetBrains decompiler
// Type: Efx.Web.Remoting.Serialization.Amf.IDataOutput
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

namespace Efx.Web.Remoting.Serialization.Amf
{
  public interface IDataOutput
  {
    void WriteBoolean(bool pValue);

    void WriteByte(byte pValue);

    void WriteBytes(byte[] pBytes, int pOffset, int pLength);

    void WriteDouble(double pValue);

    void WriteFloat(float pValue);

    void WriteInt(int pValue);

    void WriteObject(object pValue);

    void WriteShort(short pValue);

    void WriteUnsignedInt(uint pValue);

    void WriteUTF(string pValue);

    void WriteUTFBytes(string pValue);
  }
}
