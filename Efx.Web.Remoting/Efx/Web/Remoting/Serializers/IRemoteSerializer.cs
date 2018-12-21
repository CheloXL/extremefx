// Decompiled with JetBrains decompiler
// Type: Efx.Web.Remoting.Serializers.IRemoteSerializer
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using System;

namespace Efx.Web.Remoting.Serializers
{
  public interface IRemoteSerializer
  {
    string EncodingType { get; }

    string Name { get; }

    byte[] ToByteArray(object pObject);

    object FromByteArray(byte[] pArray, Type pType);

    object TryCastTo(object pObject, Type pType);
  }
}
