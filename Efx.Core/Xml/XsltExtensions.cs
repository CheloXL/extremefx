// Decompiled with JetBrains decompiler
// Type: Efx.Core.Xml.XsltExtensions
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Plugins;
using System;

namespace Efx.Core.Xml
{
  public class XsltExtensions : IPlugin, IXsltExtensions
  {
    private static readonly string[] _sizeUnits1 = new string[9]{ "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
    private static readonly string[] _sizeUnits2 = new string[9]{ "Bytes", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB", "ZiB", "YiB" };

    public static string ToHumanSize(string pSize)
    {
      long result;
      if (!long.TryParse(pSize, out result))
        return XsltExtensions.ToHumanSize(0L, false);
      return XsltExtensions.ToHumanSize(result, false);
    }

    public static string ToHumanSize(long pSize, bool pUseI)
    {
      int index = 0;
      int num1 = pUseI ? 1024 : 1000;
      double num2 = (double) pSize;
      while (num2 >= (double) num1)
      {
        num2 /= (double) num1;
        ++index;
      }
      return string.Format("{0:F1} {1}", (object) num2, (object) (pUseI ? XsltExtensions._sizeUnits2 : XsltExtensions._sizeUnits1)[index]);
    }

    public static string DateFormat(string pDate, string pFormat)
    {
      DateTime result;
      if (!DateTime.TryParse(pDate, out result))
        return string.Empty;
      return result.ToString(pFormat);
    }

    public static string UrlEncode(string pUrl)
    {
      return pUrl.Replace(" ", "%20");
    }

    public static string ToRfc822Date(string pDate)
    {
      DateTime result;
      if (!DateTime.TryParse(pDate, out result))
        return W3CDateTime.ToString(DateTime.UtcNow, "R");
      return W3CDateTime.ToString(result, "R");
    }

    public string PluginName
    {
      get
      {
        return "urn:EfxExtensions";
      }
    }

    public PluginInfo PluginInfo
    {
      get
      {
        return (PluginInfo) new XsltExtensions.Info();
      }
    }

    public void Start(IApplication pApplication)
    {
    }

    public string Urn
    {
      get
      {
        return "urn:EfxExtensions";
      }
    }

    private sealed class Info : PluginInfo
    {
      public override string Name
      {
        get
        {
          return "Extreme|FX core XSLT extensions";
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
