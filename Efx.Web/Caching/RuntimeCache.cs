// Decompiled with JetBrains decompiler
// Type: Efx.Web.Caching.RuntimeCache
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Core.Caching;
using Efx.Core.ExtensionMethods;
using System;
using System.Collections;
using System.Web;

namespace Efx.Web.Caching
{
  public sealed class RuntimeCache : ICache<string, object>
  {
    private static readonly object _syncLock = new object();

    public void Set(string key, object value)
    {
      lock (RuntimeCache._syncLock)
        HttpRuntime.Cache[key] = value;
    }

    public object Get(string key)
    {
      return HttpRuntime.Cache[key];
    }

    public bool Contains(string key)
    {
      return !this.Get(key).IsNullOrDefault<object>();
    }

    public object GetOrAdd(string key, Func<string, object> valueCreator)
    {
      object obj = HttpRuntime.Cache[key];
      if (obj != null)
        return obj;
      lock (RuntimeCache._syncLock)
        return HttpRuntime.Cache[key] ?? (HttpRuntime.Cache[key] = valueCreator(key));
    }

    public T Get<T>(string pKey)
    {
      object obj = HttpRuntime.Cache[pKey];
      if (obj != null)
        return (T) obj;
      return default (T);
    }

    public void Remove(string pKey)
    {
      lock (RuntimeCache._syncLock)
      {
        if (HttpRuntime.Cache[pKey] == null)
          return;
        HttpRuntime.Cache.Remove(pKey);
      }
    }

    public void Clear()
    {
      lock (RuntimeCache._syncLock)
      {
        foreach (DictionaryEntry dictionaryEntry in HttpRuntime.Cache)
          HttpRuntime.Cache.Remove(dictionaryEntry.Key.ToString());
      }
    }
  }
}
