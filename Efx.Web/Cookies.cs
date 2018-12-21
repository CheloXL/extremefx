// Decompiled with JetBrains decompiler
// Type: Efx.Web.Cookies
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;
using System.Web;

namespace Efx.Web
{
  public static class Cookies
  {
    public static string GetValue(string pKey)
    {
      return Cookies.GetValue(HttpContext.Current, pKey);
    }

    public static string GetValue(HttpContext pContext, string pKey)
    {
      string str = (string) null;
      if (pContext == null)
        return (string) null;
      HttpCookie cookie = pContext.Request.Cookies[pKey];
      if (cookie == null)
      {
        if (pContext.Session[pKey] != null && pContext.Session[pKey].ToString() != "0")
          str = pContext.Session[pKey].ToString();
      }
      else
        str = cookie.Value;
      return str;
    }

    public static bool HasValue(string pKey)
    {
      return !string.IsNullOrEmpty(Cookies.GetValue(HttpContext.Current, pKey));
    }

    public static void SetValue(string pKey, string pValue)
    {
      Cookies.SetValue(HttpContext.Current, pKey, pValue, DateTime.UtcNow.AddMonths(1));
    }

    public static void SetValue(string pKey, string pValue, DateTime pExpires)
    {
      Cookies.SetValue(HttpContext.Current, pKey, pValue, pExpires);
    }

    public static void SetValue(
      HttpContext pContext,
      string pKey,
      string pValue,
      DateTime pExpires)
    {
      if (pContext == null)
        return;
      HttpCookie cookie = pContext.Request.Cookies[pKey] ?? new HttpCookie(pKey);
      cookie.Value = pValue;
      cookie.Expires = pExpires;
      pContext.Response.Cookies.Remove(pKey);
      pContext.Response.Cookies.Add(cookie);
    }
  }
}
