// Decompiled with JetBrains decompiler
// Type: Efx.Web.Authorization.Repository.RepositorySettings
// Assembly: Efx.Web.Authorization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 06C571D6-DC37-4657-A156-F8EB982998FF
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Authorization.dll

using System.Xml.Serialization;

namespace Efx.Web.Authorization.Repository
{
  [XmlRoot(ElementName = "Configuration")]
  public sealed class RepositorySettings : WebConfiguration<RepositorySettings>
  {
    [XmlAttribute]
    public string Repository;
    [XmlAttribute]
    public string Realm;
    [XmlElement]
    public Discriminators Discriminators;
  }
}
