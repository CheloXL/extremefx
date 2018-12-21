// Decompiled with JetBrains decompiler
// Type: Efx.Web.Xml.XsltWebExtensions
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Core.ExtensionMethods;
using Efx.Core.Plugins;
using Efx.Core.Xml;
using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Efx.Web.Xml
{
  public class XsltWebExtensions : IXsltExtensions, IPlugin
  {
    private static readonly Regex _nls = new Regex("[\n\r]+");
    private static readonly Regex _amputator = new Regex("&(?!#?[xX]?(?:[0-9a-fA-F]+|\\w{1,8});)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
    private static readonly Regex _absolutize = new Regex("<(.*?)(src|href)=\"(?!http)(.*?)\"(.*?)>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

    public static string XmlEscape(string pText)
    {
      return XsltWebExtensions._amputator.Replace(pText, "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
    }

    public static string Amputator(string pText)
    {
      return XsltWebExtensions._amputator.Replace(pText, "&amp;");
    }

    public static string AbsoluteUrls(string pText, string pBaseUrl)
    {
      if (string.IsNullOrEmpty(pText) || string.IsNullOrEmpty(pBaseUrl))
        return pText;
      return XsltWebExtensions._absolutize.Replace(pText, string.Format("<$1$2=\"{0}/$3\"$4>", (object) pBaseUrl)).Replace(string.Format("{0}//", (object) pBaseUrl), string.Format("{0}/", (object) pBaseUrl));
    }

    public static void ChangeContentType(string pMimeType)
    {
      HttpContext current = HttpContext.Current;
      if (string.IsNullOrEmpty(pMimeType) || current == null)
        return;
      current.Response.ContentType = pMimeType;
    }

    public static void AddResponseHeader(string headerName, string headerValue)
    {
      HttpContext current = HttpContext.Current;
      if (string.IsNullOrEmpty(headerName) || current == null)
        return;
      current.Response.AppendHeader(headerName, headerValue);
    }

    public static string CreateSummary(string pHtmlText, int pChars)
    {
      return XsltWebExtensions._nls.Replace(pHtmlText.StripHtml(), " ").SmartRightTrim(pChars, " …");
    }

    public static string QueryString(string pName)
    {
      try
      {
        return new Url()[pName];
      }
      catch (Exception ex)
      {
        return string.Empty;
      }
    }

    public static string CurrentUrl(string pName)
    {
      try
      {
        Url url = new Url();
        if (!string.IsNullOrEmpty(pName))
          url.RemovePair(pName);
        return url.ToString();
      }
      catch (Exception ex)
      {
        return string.Empty;
      }
    }

    public static string CurrentUrl()
    {
      return new Url().ToString();
    }

    public string PluginName
    {
      get
      {
        return "urn:EfxWebExtensions";
      }
    }

    public PluginInfo PluginInfo
    {
      get
      {
        return (PluginInfo) new XsltWebExtensions.Info();
      }
    }

    public void Start(IApplication pApplication)
    {
    }

    public string Urn
    {
      get
      {
        return "urn:EfxWebExtensions";
      }
    }

    private sealed class Info : PluginInfo
    {
      public override string Name
      {
        get
        {
          return "Extreme|FX web XSLT extensions";
        }
      }

      public override string Description
      {
        get
        {
          return string.Empty;
        }
      }

      public override string Homepage
      {
        get
        {
          return "http://www.extremefx.com.ar";
        }
      }

      public override string Copyright
      {
        get
        {
          return "2011 © Extreme|FX";
        }
      }
    }
  }
}
