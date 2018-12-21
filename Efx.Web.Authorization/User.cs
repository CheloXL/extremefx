// Decompiled with JetBrains decompiler
// Type: Efx.Web.Authorization.User
// Assembly: Efx.Web.Authorization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 06C571D6-DC37-4657-A156-F8EB982998FF
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Authorization.dll

using System;
using System.Xml.Serialization;

namespace Efx.Web.Authorization
{
  [XmlRoot]
  [Serializable]
  public class User
  {
    public User()
      : this(string.Empty)
    {
    }

    public User(string pName)
    {
      this.Name = pName;
      this.FullName = this.Email = this.Password = string.Empty;
    }

    [XmlAttribute]
    public string Name { get; set; }

    [XmlAttribute]
    public string FullName { get; set; }

    [XmlAttribute]
    public string Email { get; set; }

    [XmlAttribute]
    public string Password { get; set; }
  }
}
