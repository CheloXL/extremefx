// Decompiled with JetBrains decompiler
// Type: Efx.Web.Scheduler
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Caching;

namespace Efx.Web
{
  public static class Scheduler
  {
    private static string _baseUrl;
    private static bool _isRunning;

    private static void AddCacheObject(Scheduler.CacheItem pCache)
    {
      Scheduler.SetupRefreshJob();
      Cache cache = HttpRuntime.Cache;
      if (cache[pCache.Name] != null)
        return;
      cache.Add(pCache.Name, (object) pCache, (CacheDependency) null, DateTime.Now.AddMinutes((double) pCache.RefreshEvery), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, new CacheItemRemovedCallback(Scheduler.CacheCallback));
    }

    private static void SetupRefreshJob()
    {
      if (HttpContext.Current == null)
        return;
      if (string.IsNullOrEmpty(Scheduler._baseUrl))
        Scheduler._baseUrl = WebUtilities.GetServerBaseUri(HttpContext.Current.Request);
      if (Scheduler._isRunning)
        return;
      ThreadPool.QueueUserWorkItem(new WaitCallback(Scheduler.AsyncRequest));
    }

    private static void AsyncRequest(object pState)
    {
      Scheduler._isRunning = true;
      while (true)
      {
        Thread.Sleep(300000);
        WebClient webClient = new WebClient();
        try
        {
          webClient.DownloadData(Scheduler._baseUrl);
        }
        catch (Exception ex)
        {
          Scheduler._isRunning = false;
        }
        finally
        {
          webClient.Dispose();
        }
      }
    }

    private static void CacheCallback(string pKey, object pValue, CacheItemRemovedReason pReason)
    {
      Scheduler.CacheItem pCache = (Scheduler.CacheItem) pValue;
      DateTime now = DateTime.Now;
      if (pCache.LastRun < DateTime.Now)
      {
        if (pCache.Callback != null)
          pCache.Callback();
        pCache.LastRun = now;
      }
      Scheduler.AddCacheObject(pCache);
    }

    public static void Run(int pMinutes, Action pCallbackMethod)
    {
      if (pMinutes < 5)
        throw new ArgumentOutOfRangeException(nameof (pMinutes));
      if (pCallbackMethod == null)
        throw new ArgumentNullException(nameof (pCallbackMethod));
      Scheduler.AddCacheObject(new Scheduler.CacheItem()
      {
        RefreshEvery = pMinutes,
        Name = Guid.NewGuid().ToString(),
        Callback = pCallbackMethod,
        LastRun = DateTime.Now
      });
    }

    private sealed class CacheItem
    {
      public Action Callback;
      public DateTime LastRun;
      public string Name;
      public int RefreshEvery;
    }
  }
}
