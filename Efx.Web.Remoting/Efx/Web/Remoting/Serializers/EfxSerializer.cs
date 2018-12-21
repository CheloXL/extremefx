// Decompiled with JetBrains decompiler
// Type: Efx.Web.Remoting.Serializers.EfxSerializer
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using Efx.Core.Serialization;
using System;
using System.IO;

namespace Efx.Web.Remoting.Serializers
{
  public sealed class EfxSerializer : IRemoteSerializer
  {
    public string EncodingType
    {
      get
      {
        return "binary/octet-stream";
      }
    }

    public string Name
    {
      get
      {
        return "efx";
      }
    }

    public byte[] ToByteArray(object pObject)
    {
      if (pObject == null)
        return (byte[]) null;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        FastBinaryFormatter.Serialize((Stream) memoryStream, pObject);
        memoryStream.Close();
        return memoryStream.ToArray();
      }
    }

    public object FromByteArray(byte[] pArray, Type pType)
    {
      if (pArray == null || pArray.Length < 1)
        return (object) null;
      using (MemoryStream memoryStream = new MemoryStream(pArray))
        return FastBinaryFormatter.Deserialize((Stream) memoryStream);
    }

    public object TryCastTo(object pObject, Type pType)
    {
      return pObject;
    }
  }
}
