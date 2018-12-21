// Decompiled with JetBrains decompiler
// Type: Efx.Web.HttpAsyncHandler
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;
using System.Web;

namespace Efx.Web
{
  public abstract class HttpAsyncHandler : IHttpAsyncHandler, IHttpHandler
  {
    public bool IsReusable
    {
      get
      {
        return true;
      }
    }

    protected abstract AsynchRequestOperation ProcessAsyncRequest(
      AsyncCallback callback,
      HttpContext context,
      object extraData);

    public IAsyncResult BeginProcessRequest(
      HttpContext context,
      AsyncCallback cb,
      object extraData)
    {
      return (IAsyncResult) this.ProcessAsyncRequest(cb, context, extraData);
    }

    public void EndProcessRequest(IAsyncResult result)
    {
      ((AsynchRequestOperation) result)?.EndRequest();
    }

    public void ProcessRequest(HttpContext context)
    {
      throw new NotImplementedException();
    }
  }
}
