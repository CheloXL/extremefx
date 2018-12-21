// Decompiled with JetBrains decompiler
// Type: Efx.Web.AsynchRequestOperation
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;
using System.Threading;
using System.Web;

namespace Efx.Web
{
  public abstract class AsynchRequestOperation : IAsyncResult
  {
    private readonly AsyncCallback _callback;

    protected AsynchRequestOperation(AsyncCallback callback, HttpContext context, object extraData)
    {
      this._callback = callback;
      this.Context = context;
      this.AsyncState = extraData;
      this.IsCompleted = false;
      ThreadPool.QueueUserWorkItem(new WaitCallback(this.StartTask), (object) null);
    }

    private void StartTask(object extraData)
    {
      this.StartRequest(extraData);
      this.IsCompleted = true;
      if (this._callback == null)
        return;
      this._callback((IAsyncResult) this);
    }

    public abstract void StartRequest(object extraData);

    public abstract void EndRequest();

    protected HttpContext Context { get; private set; }

    public object AsyncState { get; private set; }

    public bool IsCompleted { get; private set; }

    public bool CompletedSynchronously
    {
      get
      {
        return false;
      }
    }

    public WaitHandle AsyncWaitHandle
    {
      get
      {
        return (WaitHandle) null;
      }
    }
  }
}
