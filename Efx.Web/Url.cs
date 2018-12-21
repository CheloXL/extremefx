// Decompiled with JetBrains decompiler
// Type: Efx.Web.Url
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Efx.Web
{
  public sealed class Url
  {
    private static readonly char[] _question = new char[1]
    {
      '?'
    };
    private static readonly char[] _ampersand = new char[1]
    {
      '&'
    };
    private readonly List<string> _positions = new List<string>();
    private readonly Dictionary<string, string> _qs;
    private readonly string _baseUrl;

    public Url()
      : this(HttpContext.Current.Request.Url)
    {
    }

    public Url(string pUrl)
      : this(new Uri(pUrl))
    {
    }

    public Url(Uri pUri)
    {
      this._baseUrl = pUri.AbsolutePath;
      this._qs = new Dictionary<string, string>();
      foreach (string str in pUri.Query.TrimStart(Url._question).Split(Url._ampersand, StringSplitOptions.RemoveEmptyEntries))
      {
        char[] chArray = new char[1]{ '=' };
        string[] strArray = str.Split(chArray);
        string pHtml1 = strArray[0];
        string pHtml2 = strArray.Length == 2 ? HttpUtility.UrlDecode(strArray[1]).Replace('+', ' ') : pHtml1;
        if (pHtml1.Length > 4 && pHtml1.Substring(0, 4).Equals("amp;", StringComparison.OrdinalIgnoreCase))
          pHtml1 = pHtml1.Substring(4);
        if (!string.IsNullOrEmpty(pHtml1))
          this._qs.Add(pHtml1.XssClean(), pHtml2.XssClean());
      }
      foreach (KeyValuePair<string, string> keyValuePair in this._qs.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (pKeyValuePair => !string.IsNullOrEmpty(pKeyValuePair.Key))))
        this._positions.Add(keyValuePair.Key);
    }

    public string this[string pKey]
    {
      get
      {
        if (string.IsNullOrEmpty(pKey))
          return (string) null;
        string str;
        if (!this._qs.TryGetValue(pKey, out str))
          return (string) null;
        return str;
      }
      set
      {
        this.AddPair(pKey, value);
      }
    }

    public Url AddPair(string pKey, string pValue)
    {
      if (!string.IsNullOrEmpty(pKey))
      {
        if (this._qs.ContainsKey(pKey))
        {
          this._qs[pKey] = pValue;
        }
        else
        {
          this._positions.Add(pKey);
          this._qs.Add(pKey, pValue);
        }
      }
      return this;
    }

    public Url RemovePair(string pKey)
    {
      if (this._qs.ContainsKey(pKey))
      {
        this._qs.Remove(pKey);
        this._positions.Remove(pKey);
      }
      return this;
    }

    public override string ToString()
    {
      return this.ToString(false);
    }

    public string ToString(bool pUseHtmlAttribudeEncode)
    {
      StringBuilder stringBuilder = new StringBuilder(this._baseUrl);
      List<string> list = this._positions.Select<string, string>((Func<string, string>) (pKeyName => string.Format("{0}={1}", (object) HttpUtility.UrlPathEncode(pKeyName), (object) HttpUtility.UrlPathEncode(this._qs[pKeyName])))).ToList<string>();
      if (list.Count != 0)
      {
        stringBuilder.Append("?");
        stringBuilder.Append(string.Join("&", list.ToArray()));
      }
      if (!pUseHtmlAttribudeEncode)
        return stringBuilder.ToString();
      return HttpUtility.HtmlAttributeEncode(stringBuilder.ToString());
    }
  }
}
