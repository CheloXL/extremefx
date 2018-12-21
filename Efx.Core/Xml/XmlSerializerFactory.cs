// Decompiled with JetBrains decompiler
// Type: Efx.Core.Xml.XmlSerializerFactory
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Efx.Core.Xml
{
  public static class XmlSerializerFactory
  {
    private static readonly XmlReaderSettings _settings = new XmlReaderSettings();
    private static readonly Dictionary<Type, XmlSerializer> _serializers = new Dictionary<Type, XmlSerializer>();

    static XmlSerializerFactory()
    {
      XmlSerializerFactory._settings.IgnoreComments = true;
      XmlSerializerFactory._settings.IgnoreWhitespace = true;
      XmlSerializerFactory._settings.ProhibitDtd = false;
      XmlSerializerFactory._settings.XmlResolver = (XmlResolver) null;
    }

    private static void AddToCache(Type type)
    {
      lock (XmlSerializerFactory._serializers)
      {
        if (XmlSerializerFactory._serializers.ContainsKey(type) || XmlSerializerFactory._serializers.ContainsKey(type))
          return;
        XmlSerializerFactory._serializers.Add(type, new XmlSerializer(type));
      }
    }

    public static T New<T>(string pFile)
    {
      if (string.IsNullOrEmpty(pFile))
        throw new ArgumentNullException(nameof (pFile));
      Type type = typeof (T);
      XmlSerializerFactory.AddToCache(type);
      XmlTextReader xmlTextReader = new XmlTextReader(pFile);
      object obj = XmlSerializerFactory._serializers[type].Deserialize((XmlReader) xmlTextReader);
      xmlTextReader.Close();
      return (T) obj;
    }

    public static T New<T>(Stream pXml)
    {
      Type type = typeof (T);
      XmlSerializerFactory.AddToCache(type);
      XmlReader xmlReader = XmlReader.Create(pXml, XmlSerializerFactory._settings);
      object obj = XmlSerializerFactory._serializers[type].Deserialize(xmlReader);
      xmlReader.Close();
      return (T) obj;
    }

    public static T New<T>(TextReader pXml)
    {
      Type type = typeof (T);
      XmlSerializerFactory.AddToCache(type);
      XmlReader xmlReader = XmlReader.Create(pXml, XmlSerializerFactory._settings);
      object obj = XmlSerializerFactory._serializers[type].Deserialize(xmlReader);
      xmlReader.Close();
      return (T) obj;
    }

    public static void Save<T>(string pFile, T pObject)
    {
      if (string.IsNullOrEmpty(pFile))
        throw new ArgumentNullException(nameof (pFile));
      DirectoryInfo directory = new FileInfo(pFile).Directory;
      if (!directory.Exists)
        Directory.CreateDirectory(directory.FullName);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (T));
      XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
      namespaces.Add(string.Empty, string.Empty);
      XmlWriterSettings settings = new XmlWriterSettings() { OmitXmlDeclaration = true, Indent = true, IndentChars = "\t" };
      using (XmlWriter xmlWriter = XmlWriter.Create(pFile, settings))
        xmlSerializer.Serialize(xmlWriter, (object) pObject, namespaces);
    }

    public static string GetAsXml<T>(T pObject)
    {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (T));
      XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
      namespaces.Add(string.Empty, string.Empty);
      XmlWriterSettings settings = new XmlWriterSettings() { OmitXmlDeclaration = true, Indent = true, IndentChars = "\t" };
      StringBuilder output = new StringBuilder();
      using (XmlWriter xmlWriter = XmlWriter.Create(output, settings))
        xmlSerializer.Serialize(xmlWriter, (object) pObject, namespaces);
      return output.ToString();
    }
  }
}
