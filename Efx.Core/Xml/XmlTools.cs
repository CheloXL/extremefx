// Decompiled with JetBrains decompiler
// Type: Efx.Core.Xml.XmlTools
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Debug;
using Efx.Core.Plugins;
using Efx.Core.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Efx.Core.Xml
{
  public static class XmlTools
  {
    private static readonly XmlReaderSettings _settings = new XmlReaderSettings();
    private static readonly List<IXsltExtensions> _xsltExtensions = new List<IXsltExtensions>();

    public static void InitDllsFolder(string pPathToDllsFolder)
    {
      lock (XmlTools._xsltExtensions)
      {
        foreach (string typeName in TypeResolver.GetAssignableFromType<IXsltExtensions>(pPathToDllsFolder, "*.dll"))
        {
          try
          {
            XmlTools._xsltExtensions.Add(TypeHelper.GetTypeByName(typeName).CreateInstance<IXsltExtensions>());
          }
          catch (Exception ex)
          {
            Trace.LogError(string.Format("Error adding XSLT Extension: {0}", (object) typeName), ex);
          }
        }
      }
    }

    static XmlTools()
    {
      XmlTools._settings.IgnoreComments = true;
      XmlTools._settings.IgnoreWhitespace = true;
      XmlTools._settings.ProhibitDtd = false;
      XmlTools._settings.XmlResolver = (XmlResolver) null;
    }

    public static string GetXsltTransformResult(
      IXPathNavigable pMacroXml,
      XslCompiledTransform pXslt)
    {
      return XmlTools.GetXsltTransformResult(pMacroXml, pXslt, new XsltArgumentList(), (Dictionary<string, object>) null);
    }

    public static string GetXsltTransformResult(
      IXPathNavigable pMacroXml,
      XslCompiledTransform pXslt,
      Dictionary<string, object> pParameters)
    {
      return XmlTools.GetXsltTransformResult(pMacroXml, pXslt, new XsltArgumentList(), pParameters);
    }

    public static string GetXsltTransformResult(
      IXPathNavigable pMacroXml,
      XslCompiledTransform pXslt,
      XsltArgumentList pArgumentList,
      Dictionary<string, object> pParameters = null)
    {
      if (pMacroXml == null)
        throw new ArgumentNullException(nameof (pMacroXml));
      if (pXslt == null)
        throw new ArgumentNullException(nameof (pXslt));
      if (pArgumentList == null)
        throw new ArgumentNullException(nameof (pArgumentList));
      TextWriter results = (TextWriter) new StringWriter();
      if (pParameters != null)
      {
        foreach (KeyValuePair<string, object> pParameter in pParameters)
          pArgumentList.AddParam(pParameter.Key, string.Empty, pParameter.Value);
      }
      HashSet<string> stringSet = new HashSet<string>();
      foreach (IXsltExtensions xsltExtension in XmlTools._xsltExtensions)
      {
        string urn = xsltExtension.Urn;
        if (!stringSet.Contains(urn))
        {
          stringSet.Add(urn);
          if (pArgumentList.GetExtensionObject(urn) == null)
            pArgumentList.AddExtensionObject(urn, (object) xsltExtension);
        }
      }
      pXslt.Transform(pMacroXml, pArgumentList, results);
      return results.ToString();
    }

    public static string GetXsltTransformResult(XmlReader pXmlReader, XslCompiledTransform pXslt)
    {
      return XmlTools.GetXsltTransformResult(pXmlReader, pXslt, new XsltArgumentList(), (Dictionary<string, object>) null);
    }

    public static string GetXsltTransformResult(
      XmlReader pXmlReader,
      XslCompiledTransform pXslt,
      Dictionary<string, object> pParameters)
    {
      return XmlTools.GetXsltTransformResult(pXmlReader, pXslt, new XsltArgumentList(), pParameters);
    }

    public static string GetXsltTransformResult(
      XmlReader pMacroXml,
      XslCompiledTransform pXslt,
      XsltArgumentList pArgumentList,
      Dictionary<string, object> pParameters = null)
    {
      if (pMacroXml == null)
        throw new ArgumentNullException(nameof (pMacroXml));
      if (pXslt == null)
        throw new ArgumentNullException(nameof (pXslt));
      if (pArgumentList == null)
        throw new ArgumentNullException(nameof (pArgumentList));
      TextWriter results = (TextWriter) new StringWriter();
      if (pParameters != null)
      {
        foreach (KeyValuePair<string, object> pParameter in pParameters)
          pArgumentList.AddParam(pParameter.Key, string.Empty, pParameter.Value);
      }
      HashSet<string> stringSet = new HashSet<string>();
      foreach (IXsltExtensions xsltExtension in XmlTools._xsltExtensions)
      {
        string urn = xsltExtension.Urn;
        if (!stringSet.Contains(urn))
        {
          stringSet.Add(urn);
          if (pArgumentList.GetExtensionObject(urn) == null)
            pArgumentList.AddExtensionObject(urn, (object) xsltExtension);
        }
      }
      pXslt.Transform(pMacroXml, pArgumentList, results);
      return results.ToString();
    }

    public static string GetXsltTransformResult(string pXmlString, XslCompiledTransform pXslt)
    {
      return XmlTools.GetXsltTransformResult(pXmlString, pXslt, new XsltArgumentList(), (Dictionary<string, object>) null);
    }

    public static string GetXsltTransformResult(
      string pXmlString,
      XslCompiledTransform pXslt,
      Dictionary<string, object> pParameters)
    {
      return XmlTools.GetXsltTransformResult(pXmlString, pXslt, new XsltArgumentList(), pParameters);
    }

    public static string GetXsltTransformResult(
      string pXmlString,
      XslCompiledTransform pXslt,
      XsltArgumentList pArgumentList,
      Dictionary<string, object> pParameters = null)
    {
      using (StringReader stringReader = new StringReader(pXmlString))
      {
        using (XmlReader pMacroXml = (XmlReader) new XmlTextReader((TextReader) stringReader))
          return XmlTools.GetXsltTransformResult(pMacroXml, pXslt, pArgumentList, pParameters);
      }
    }

    public static XslCompiledTransform GetXslt(string pXsltFilename)
    {
      if (string.IsNullOrEmpty(pXsltFilename))
        throw new ArgumentNullException(nameof (pXsltFilename));
      return XmlTools.GetXslt((XmlReader) new XmlTextReader(pXsltFilename) { EntityHandling = EntityHandling.ExpandEntities });
    }

    public static XslCompiledTransform GetXslt(IXPathNavigable pXPathNavigable)
    {
      if (pXPathNavigable == null)
        throw new ArgumentNullException(nameof (pXPathNavigable));
      XslCompiledTransform compiledTransform = new XslCompiledTransform();
      XmlUrlResolver xmlUrlResolver1 = new XmlUrlResolver();
      xmlUrlResolver1.Credentials = CredentialCache.DefaultCredentials;
      XmlUrlResolver xmlUrlResolver2 = xmlUrlResolver1;
      compiledTransform.Load(pXPathNavigable, XsltSettings.TrustedXslt, (XmlResolver) xmlUrlResolver2);
      return compiledTransform;
    }

    public static XslCompiledTransform GetXslt(XmlReader pXmlReader)
    {
      if (pXmlReader == null)
        throw new ArgumentNullException(nameof (pXmlReader));
      XslCompiledTransform compiledTransform = new XslCompiledTransform();
      XmlUrlResolver xmlUrlResolver1 = new XmlUrlResolver();
      xmlUrlResolver1.Credentials = CredentialCache.DefaultCredentials;
      XmlUrlResolver xmlUrlResolver2 = xmlUrlResolver1;
      try
      {
        compiledTransform.Load(pXmlReader, XsltSettings.TrustedXslt, (XmlResolver) xmlUrlResolver2);
      }
      finally
      {
        pXmlReader.Close();
      }
      return compiledTransform;
    }
  }
}
