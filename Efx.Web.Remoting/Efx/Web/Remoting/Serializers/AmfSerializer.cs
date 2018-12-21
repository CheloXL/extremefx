// Decompiled with JetBrains decompiler
// Type: Efx.Web.Remoting.Serializers.AmfSerializer
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using Efx.Web.Remoting.Serialization.Amf;
using System;

namespace Efx.Web.Remoting.Serializers
{
  public sealed class AmfSerializer : IRemoteSerializer
  {
    public string EncodingType
    {
      get
      {
        return "application/x-amf";
      }
    }

    public byte[] ToByteArray(object pObject)
    {
      return AmfMapper.ToAmf(pObject);
    }

    public object FromByteArray(byte[] pArray, Type pType)
    {
      return AmfMapper.ToObject(pArray, pType);
    }

    public object TryCastTo(object pObject, Type pType)
    {
      return pObject;
    }

    public string Name
    {
      get
      {
        return "amf";
      }
    }
  }
}
