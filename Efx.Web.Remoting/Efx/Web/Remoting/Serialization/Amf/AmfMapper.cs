// Decompiled with JetBrains decompiler
// Type: Efx.Web.Remoting.Serialization.Amf.AmfMapper
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using Efx.Core.Conversion;
using System;
using System.IO;

namespace Efx.Web.Remoting.Serialization.Amf
{
  public static class AmfMapper
  {
    public static byte[] ToAmf(object pObj)
    {
      MemoryStream memoryStream = new MemoryStream();
      new Class119((Stream) memoryStream).method_14(pObj);
      memoryStream.Position = 0L;
      return memoryStream.ToArray();
    }

    public static object ToObject(byte[] pAmfData)
    {
      return new Class118((Stream) new MemoryStream(pAmfData)).method_2();
    }

    public static object ToObject(byte[] pAmfData, Type pType)
    {
      return AmfMapper.ToObject(pAmfData).Convert(pType);
    }
  }
}
