// Decompiled with JetBrains decompiler
// Type: Efx.Web.Remoting.Serializers.DotNetSerializer
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Efx.Web.Remoting.Serializers
{
  public sealed class DotNetSerializer : IRemoteSerializer
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
        return "dotnet";
      }
    }

    public byte[] ToByteArray(object pObject)
    {
      if (pObject == null)
        return (byte[]) null;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) memoryStream, pObject);
        memoryStream.Close();
        return memoryStream.ToArray();
      }
    }

    public object FromByteArray(byte[] pArray, Type pType)
    {
      if (pArray == null || pArray.Length < 1)
        return (object) null;
      using (MemoryStream memoryStream = new MemoryStream(pArray))
        return new BinaryFormatter().Deserialize((Stream) memoryStream);
    }

    public object TryCastTo(object pObject, Type pType)
    {
      return pObject;
    }
  }
}
