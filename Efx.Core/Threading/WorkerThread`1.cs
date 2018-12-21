// Decompiled with JetBrains decompiler
// Type: Efx.Core.Threading.WorkerThread`1
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Debug;
using Efx.Core.Events;
using Efx.Core.ExtensionMethods;
using System;
using System.ComponentModel;
using System.Threading;

namespace Efx.Core.Threading
{
  public abstract class WorkerThread<TArgument> : IDisposable
  {
    private readonly object _threadLock = new object();
    private readonly SendOrPostCallback _progressReporter;
    private TArgument _argument;
    private AsyncOperation _asyncOperation;
    private float _oldProgress;
    private Thread _thread;

    protected WorkerThread()
    {
      this._progressReporter = new SendOrPostCallback(this.ProgressReporter);
    }

    protected virtual string ThreadName
    {
      get
      {
        return string.Format("Thread Worker ({0})", (object) this.GetHashCode());
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
    }

    public event EventHandler<ChangingData<float>> ProgressChanged;

    protected virtual void ThreadStarted(TArgument pArgument)
    {
    }

    protected virtual void ThreadFinished()
    {
    }

    protected virtual void ThreadException(Exception pException)
    {
    }

    ~WorkerThread()
    {
      this.Dispose(false);
    }

    private void Dispose(bool pExplicitCall)
    {
      this.DisposeThread();
      GC.SuppressFinalize((object) this);
    }

    private void DisposeThread()
    {
      lock (this._threadLock)
      {
        try
        {
          if (this._thread == null)
            return;
          this._thread.Abort();
        }
        catch (ThreadAbortException ex)
        {
        }
        finally
        {
          this._thread = (Thread) null;
        }
      }
    }

    public void RunWorkerAsync(TArgument pArgument)
    {
      lock (this._threadLock)
      {
        if (this._thread != null)
          return;
        this._argument = pArgument;
        this._thread = new Thread(new ThreadStart(this.ThreadEntryPoint))
        {
          IsBackground = true,
          Name = this.ThreadName
        };
        this._thread.Start();
      }
    }

    private void ProgressReporter(object pState)
    {
      this.ProgressChanged.Raise<ChangingData<float>>((object) this, (ChangingData<float>) pState);
    }

    protected void ReportProgress(int pTotal, int pCurrent)
    {
      this.ReportProgress((float) pTotal / (float) pCurrent);
    }

    private void ReportProgress(float pProgress)
    {
      ChangingData<float> changingData = new ChangingData<float>(this._oldProgress, pProgress);
      if (this._asyncOperation != null)
        this._asyncOperation.Post(this._progressReporter, (object) changingData);
      else
        this._progressReporter((object) changingData);
      this._oldProgress = pProgress;
    }

    private void ThreadEntryPoint()
    {
      this._asyncOperation = AsyncOperationManager.CreateOperation((object) this._argument);
      this.ThreadStarted(this._argument);
      try
      {
        this.DoWork(this._argument);
      }
      catch (ThreadAbortException ex)
      {
      }
      catch (Exception ex)
      {
        Trace.LogError("An unhandled error occurred when processing thread.", ex);
        this.ThreadException(ex);
      }
      finally
      {
        this.ThreadFinished();
        this.DisposeThread();
      }
    }

    protected abstract void DoWork(TArgument pArgument);
  }
}
