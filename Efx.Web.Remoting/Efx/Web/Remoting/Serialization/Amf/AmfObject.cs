// Decompiled with JetBrains decompiler
// Type: Efx.Web.Remoting.Serialization.Amf.AmfObject
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using System;
using System.Collections;

namespace Efx.Web.Remoting.Serialization.Amf
{
  [Serializable]
  public sealed class AmfObject : Hashtable
  {
    public AmfObject()
    {
    }

    public AmfObject(string pTypeName)
    {
      this.method_0(pTypeName);
    }

    public string TypeName { get; }

    private void method_0(string string_0)
    {
      // ISSUE: reference to a compiler-generated field
      this.\u003CTypeName\u003Ek__BackingField = string_0;
    }

    public bool IsTypedObject
    {
      get
      {
        return !string.IsNullOrEmpty(this.TypeName);
      }
    }
  }
}
