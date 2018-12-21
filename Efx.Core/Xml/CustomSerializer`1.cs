// Decompiled with JetBrains decompiler
// Type: Efx.Core.Xml.CustomSerializer`1
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Efx.Core.Xml
{
  public sealed class CustomSerializer<TItemType> : IXmlSerializable
  {
    public CustomSerializer()
    {
    }

    public CustomSerializer(TItemType pParameters)
    {
      this.Parameters = pParameters;
    }

    public TItemType Parameters { get; private set; }

    XmlSchema IXmlSerializable.GetSchema()
    {
      return (XmlSchema) null;
    }

    void IXmlSerializable.ReadXml(XmlReader pReader)
    {
      Type type = Type.GetType(pReader.GetAttribute("type"));
      pReader.ReadStartElement();
      this.Parameters = (TItemType) new XmlSerializer(type).Deserialize(pReader);
      pReader.ReadEndElement();
    }

    void IXmlSerializable.WriteXml(XmlWriter pWriter)
    {
      pWriter.WriteAttributeString("type", this.Parameters.GetType().ToString());
      new XmlSerializer(this.Parameters.GetType()).Serialize(pWriter, (object) this.Parameters);
    }

    public static implicit operator CustomSerializer<TItemType>(TItemType p)
    {
      if ((object) p != null)
        return new CustomSerializer<TItemType>(p);
      return (CustomSerializer<TItemType>) null;
    }

    public static implicit operator TItemType(CustomSerializer<TItemType> p)
    {
      if (!p.Equals((object) default (TItemType)))
        return p.Parameters;
      return default (TItemType);
    }
  }
}
