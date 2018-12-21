// Decompiled with JetBrains decompiler
// Type: Efx.Web.Remoting.Serializers.JsonSerializer
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using Efx.Core.ExtensionMethods;
using Efx.Web.Remoting.Serialization.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Efx.Web.Remoting.Serializers
{
  public sealed class JsonSerializer : IRemoteSerializer
  {
    public string EncodingType
    {
      get
      {
        return "application/json";
      }
    }

    public string Name
    {
      get
      {
        return "json";
      }
    }

    public byte[] ToByteArray(object pObject)
    {
      if (pObject != null)
        return JsonMapper.ToJson(pObject, false).ToByteArray();
      return (byte[]) null;
    }

    public object FromByteArray(byte[] pArray, Type pType)
    {
      if (pArray != null && pArray.Length > 0)
        return JsonMapper.ToObject(pArray.ToUtf8String(), pType);
      return (object) null;
    }

    public object TryCastTo(object pObject, Type pType)
    {
      try
      {
        Dictionary<string, object> dictionary_0 = pObject as Dictionary<string, object>;
        if (dictionary_0 != null)
          return JsonMapper.smethod_0(dictionary_0, pType);
        IEnumerable enumerable = pObject as IEnumerable;
        if (enumerable == null || !pType.IsGenericType)
          return pObject;
        ArrayList arrayList = new ArrayList();
        Type genericArgument = pType.GetGenericArguments()[0];
        foreach (object pObject1 in enumerable)
          arrayList.Add(this.TryCastTo(pObject1, genericArgument));
        return (object) arrayList;
      }
      catch (Exception ex)
      {
        return pObject;
      }
    }
  }
}
