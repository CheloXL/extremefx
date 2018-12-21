// Decompiled with JetBrains decompiler
// Type: Efx.Web.Caching.ContextCache
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
  public sealed class ContextCache : ICache<string, object>
  {
    public void Set(string key, object value)
    {
      HttpContext current = HttpContext.Current;
      if (current == null)
        return;
      IDictionary items = current.Items;
      lock (items.SyncRoot)
        items[(object) key] = value;
    }

    public object Get(string key)
    {
      return HttpContext.Current?.Items[(object) key];
    }

    public bool Contains(string key)
    {
      return !this.Get(key).IsNullOrDefault<object>();
    }

    public object GetOrAdd(string key, Func<string, object> valueCreator)
    {
      HttpContext current = HttpContext.Current;
      if (current == null)
        return (object) null;
      IDictionary items = current.Items;
      object obj = items[(object) key];
      if (obj != null)
        return obj;
      lock (items.SyncRoot)
        return items[(object) key] ?? (items[(object) key] = valueCreator(key));
    }

    public T Get<T>(string pKey)
    {
      HttpContext current = HttpContext.Current;
      if (current == null)
        return default (T);
      object obj = current.Items[(object) pKey];
      if (obj != null)
        return (T) obj;
      return default (T);
    }

    public void Remove(string pKey)
    {
      HttpContext current = HttpContext.Current;
      if (current == null)
        return;
      IDictionary items = current.Items;
      lock (items.SyncRoot)
      {
        if (items[(object) pKey] == null)
          return;
        items.Remove((object) pKey);
      }
    }

    public void Clear()
    {
      HttpContext current = HttpContext.Current;
      if (current == null)
        return;
      IDictionary items = current.Items;
      lock (items.SyncRoot)
      {
        foreach (DictionaryEntry dictionaryEntry in items)
          items.Remove(dictionaryEntry.Key);
      }
    }
  }
}
