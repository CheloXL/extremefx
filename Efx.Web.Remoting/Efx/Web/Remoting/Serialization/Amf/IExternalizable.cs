// Decompiled with JetBrains decompiler
// Type: Efx.Web.Remoting.Serialization.Amf.IExternalizable
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

namespace Efx.Web.Remoting.Serialization.Amf
{
  public interface IExternalizable
  {
    void ReadExternal(IDataInput pInput);

    void WriteExternal(IDataOutput pOutput);
  }
}
