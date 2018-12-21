// Decompiled with JetBrains decompiler
// Type: Efx.Web.Authorization.Repository.FileUserRepositorySettings
// Assembly: Efx.Web.Authorization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 06C571D6-DC37-4657-A156-F8EB982998FF
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Authorization.dll

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Efx.Web.Authorization.Repository
{
  [XmlRoot(ElementName = "Configuration")]
  public sealed class FileUserRepositorySettings : WebConfiguration<FileUserRepositorySettings>
  {
    [XmlArrayItem("User", typeof (User))]
    [XmlArray("Users")]
    public List<User> UsersList;
  }
}
