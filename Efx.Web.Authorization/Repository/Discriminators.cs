// Decompiled with JetBrains decompiler
// Type: Efx.Web.Authorization.Repository.Discriminators
// Assembly: Efx.Web.Authorization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 06C571D6-DC37-4657-A156-F8EB982998FF
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Authorization.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Efx.Web.Authorization.Repository
{
  public sealed class Discriminators : Interface2
  {
    [XmlElement("Discriminators", typeof (Discriminators))]
    public Discriminators Children;
    [XmlElement("Discriminator")]
    public Discriminator[] Rules;
    private List<Interface2> list_0;
    private bool bool_0;
    private static Func<Interface2, bool> func_0;

    [XmlAttribute]
    public bool All
    {
      get
      {
        return this.bool_0;
      }
      set
      {
        this.bool_0 = value;
      }
    }

    public bool Qualifies(object pContext)
    {
      if (pContext == null)
        throw new ArgumentNullException(nameof (pContext));
      if (!this.method_0())
        return false;
      bool flag = false;
      List<Interface2> list0 = this.list_0;
      if (Discriminators.func_0 == null)
        Discriminators.func_0 = new Func<Interface2, bool>(Discriminators.smethod_0);
      Func<Interface2, bool> func0 = Discriminators.func_0;
      foreach (Interface2 nterface2 in list0.Where<Interface2>(func0))
      {
        if (nterface2.Qualifies(pContext))
        {
          if (!this.All)
            return true;
          flag = true;
        }
        else if (this.All)
          return false;
      }
      return flag;
    }

    private bool method_0()
    {
      if (this.list_0 == null)
      {
        this.list_0 = new List<Interface2>((IEnumerable<Interface2>) this.Rules);
        if (this.Children != null)
          this.list_0.Add((Interface2) this.Children);
      }
      return this.list_0.Count != 0;
    }

    private static bool smethod_0(Interface2 interface2_0)
    {
      return interface2_0 != null;
    }
  }
}
