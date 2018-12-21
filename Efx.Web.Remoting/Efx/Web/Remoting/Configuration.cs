// Decompiled with JetBrains decompiler
// Type: Efx.Web.Remoting.Configuration
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

namespace Efx.Web.Remoting
{
  public static class Configuration
  {
    public static RemotingConfiguration Data
    {
      get
      {
        return WebConfiguration<RemotingConfiguration>.Instance("RemotingConfiguration");
      }
    }
  }
}
