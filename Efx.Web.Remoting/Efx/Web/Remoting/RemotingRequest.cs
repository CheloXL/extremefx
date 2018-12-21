// Decompiled with JetBrains decompiler
// Type: Efx.Web.Remoting.RemotingRequest
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using System;

namespace Efx.Web.Remoting
{
  [Serializable]
  public sealed class RemotingRequest
  {
    public string Id;
    public string Method;
    public object[] Params;
  }
}
