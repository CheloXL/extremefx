// Decompiled with JetBrains decompiler
// Type: Efx.Web.StandardWebRequest
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Core.ExtensionMethods;
using System;

namespace Efx.Web
{
  public sealed class StandardWebRequest : WebResourceRequest
  {
    private byte[] _content;

    public void FetchData(string pUrl)
    {
      this._content = this.fetchData(pUrl);
    }

    public void FetchData(Uri uri)
    {
      this._content = this.fetchData(uri.ToString());
    }

    public string Content
    {
      get
      {
        return this._content.ToUtf8String();
      }
    }
  }
}
