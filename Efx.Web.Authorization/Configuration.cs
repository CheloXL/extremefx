// Decompiled with JetBrains decompiler
// Type: Efx.Web.Authorization.Configuration
// Assembly: Efx.Web.Authorization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 06C571D6-DC37-4657-A156-F8EB982998FF
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Authorization.dll

using Efx.Web.Authorization.Repository;

namespace Efx.Web.Authorization
{
  public static class Configuration
  {
    public static RepositorySettings Data
    {
      get
      {
        return WebConfiguration<RepositorySettings>.Instance("AuthenticationSettings");
      }
    }
  }
}
