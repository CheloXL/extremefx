// Decompiled with JetBrains decompiler
// Type: Efx.Web.Remoting.RemotingConfiguration
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Efx.Web.Remoting
{
  [XmlRoot(ElementName = "Configuration")]
  public sealed class RemotingConfiguration : WebConfiguration<RemotingConfiguration>
  {
    [XmlAttribute]
    public string DefaultSerializer = "efx";
    [XmlAttribute]
    public int KeyLength = 384;
    [XmlArray]
    public Serializer[] Serializers = new Serializer[3]
    {
      new Serializer() { Id = "json" },
      new Serializer() { Id = "efx" },
      new Serializer() { Id = "amf" }
    };
    private Dictionary<string, Dictionary<string, string>> _mappingsToDotnet = new Dictionary<string, Dictionary<string, string>>();
    private Dictionary<string, Dictionary<string, string>> _mappingsFromDotnet = new Dictionary<string, Dictionary<string, string>>();
    [XmlAttribute]
    public bool Encrypt;
    private bool _inited;

    public string ToDotnetType(string pSerializerName, string pRemoteType)
    {
      this.InitTypes();
      Dictionary<string, string> dictionary;
      string str;
      if (!this._mappingsToDotnet.TryGetValue(pSerializerName, out dictionary) || !dictionary.TryGetValue(pRemoteType, out str))
        return pRemoteType;
      return str;
    }

    public string ToRemoteType(string pSerializerName, string pDotnetType)
    {
      this.InitTypes();
      Dictionary<string, string> dictionary;
      string str;
      if (!this._mappingsFromDotnet.TryGetValue(pSerializerName, out dictionary) || !dictionary.TryGetValue(pDotnetType, out str))
        return pDotnetType;
      return str;
    }

    private void InitTypes()
    {
      if (this._inited || this.Serializers == null || this.Serializers.Length == 0)
        return;
      this._inited = true;
      foreach (Serializer serializer in this.Serializers)
      {
        string id = serializer.Id;
        this._mappingsToDotnet.Add(id, new Dictionary<string, string>());
        this._mappingsFromDotnet.Add(id, new Dictionary<string, string>());
        if (serializer.Mappings != null)
        {
          foreach (MapType mapping in serializer.Mappings)
          {
            this._mappingsToDotnet[id].Add(mapping.Remote, mapping.Local);
            this._mappingsFromDotnet[id].Add(mapping.Local, mapping.Remote);
          }
        }
      }
    }
  }
}
